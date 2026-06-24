using DeliveryOrders.Models;
using DeliveryOrders.ViewModels;

namespace DeliveryOrders.Services;

public interface IOrderService
{
    Task<IReadOnlyCollection<Order>> GetAllAsync();

    Task<Order?> GetByIdAsync(int id);

    Task<CreateOrderResult> CreateAsync(CreateOrderViewModel model);
}
