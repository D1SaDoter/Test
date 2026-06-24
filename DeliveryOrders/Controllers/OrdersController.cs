using DeliveryOrders.Services;
using DeliveryOrders.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryOrders.Controllers;

public class OrdersController(IOrderService orderService) : Controller
{
    private readonly IOrderService _orderService = orderService;

    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetAllAsync();

        return View(orders);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateOrderViewModel { PickupDate = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateOrderViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _orderService.CreateAsync(model);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError(nameof(model.CargoWeightKg), result.ErrorMessage!);

            return View(model);
        }

        TempData["SuccessMessage"] = $"Заказ {result.Order!.OrderNumber} успешно создан.";

        return RedirectToAction(nameof(Details), new { id = result.Order.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var order = await _orderService.GetByIdAsync(id);

        return order is null ? NotFound() : View(order);
    }
}
