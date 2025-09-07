namespace Ordering.Application.Extensions;
public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(order => order.ToOrderDto()).ToList();
    }

    public static OrderDto ToOrderDto(this Order order)
    {
        return DtoFromOrder(order);
    }

    private static OrderDto DtoFromOrder(Order order)
    {
        return new OrderDto(
                    Id: order.Id.Value,
                    CustomerId: order.CustomerId.Value,
                    CustomerName: order.Customer?.Name ?? "N/A",
                    OrderName: order.OrderName.Value,
                    //Payment: new PaymentDto(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
                    TotalPrice: order.TotalPrice,
                    Status: order.Status,
                    OrderItems: order.OrderItems.Select(oi => new OrderItemDto(
                        oi.OrderId.Value, 
                        oi.GameId.Value, 
                        oi.Game.Title,
                        oi.Quantity, 
                        oi.Price
                        )).ToList()
                );
    }
}

