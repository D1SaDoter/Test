using System.Globalization;
using DeliveryOrders.Data;
using DeliveryOrders.Models;
using DeliveryOrders.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOrders.Services;

public class OrderService(ApplicationDbContext dbContext) : IOrderService
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<IReadOnlyCollection<Order>> GetAllAsync()
    {
        return await _dbContext.Orders
            .AsNoTracking()
            .OrderByDescending(order => order.CreatedAtUtc)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _dbContext.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(order => order.Id == id);
    }

    public async Task<CreateOrderResult> CreateAsync(CreateOrderViewModel model)
    {
        NormalizeOrder(model);

        if (!TryParseCargoWeight(model.CargoWeightKg, out var cargoWeightKg))
        {
            return CreateOrderResult.Failure("Введите корректный вес груза.");
        }

        var order = new Order
        {
            SenderCity = model.SenderCity,
            SenderAddress = model.SenderAddress,
            RecipientCity = model.RecipientCity,
            RecipientAddress = model.RecipientAddress,
            CargoWeightKg = cargoWeightKg,
            PickupDate = model.PickupDate,
            CreatedAtUtc = DateTime.UtcNow,
            OrderNumber = GenerateOrderNumber()
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        return CreateOrderResult.Success(order);
    }

    private static void NormalizeOrder(CreateOrderViewModel model)
    {
        model.SenderCity = model.SenderCity?.Trim() ?? string.Empty;
        model.SenderAddress = model.SenderAddress?.Trim() ?? string.Empty;
        model.RecipientCity = model.RecipientCity?.Trim() ?? string.Empty;
        model.RecipientAddress = model.RecipientAddress?.Trim() ?? string.Empty;
        model.CargoWeightKg = model.CargoWeightKg?.Trim() ?? string.Empty;
    }

    private static bool TryParseCargoWeight(string input, out decimal cargoWeightKg)
    {
        cargoWeightKg = 0;

        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        var normalized = input.Replace(" ", string.Empty).Replace(',', '.');

        return decimal.TryParse(
                normalized,
                NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture,
                out cargoWeightKg)
            && cargoWeightKg > 0
            && cargoWeightKg <= 100000;
    }

    private static string GenerateOrderNumber()
    {
        var suffix = Guid.NewGuid().ToString("N")[..6].ToUpperInvariant();

        return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{suffix}";
    }
}
