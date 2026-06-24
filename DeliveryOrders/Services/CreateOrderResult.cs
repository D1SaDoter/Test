using DeliveryOrders.Models;

namespace DeliveryOrders.Services;

public class CreateOrderResult
{
    private CreateOrderResult(Order? order, string? errorMessage)
    {
        Order = order;
        ErrorMessage = errorMessage;
    }

    public Order? Order { get; }

    public string? ErrorMessage { get; }

    public bool IsSuccess => Order is not null;

    public static CreateOrderResult Success(Order order)
    {
        return new CreateOrderResult(order, null);
    }

    public static CreateOrderResult Failure(string errorMessage)
    {
        return new CreateOrderResult(null, errorMessage);
    }
}
