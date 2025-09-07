
namespace AirWaterStore.Web.Extentions;

public static class OrderExtention
{

    public static Order ToOrder(this OrderDto orderDto)
    {
        List<OrderItem> items = orderDto.OrderItems.Select(oi => oi.ToOrderItem()).ToList();
        return new Order
        {
            OrderId = orderDto.Id,
            OrderName = orderDto.OrderName,
            UserId = orderDto.CustomerId,
            UserName = orderDto.CustomerName,
            TotalPrice = orderDto.TotalPrice,
            Status = orderDto.Status.ToString(),
            OrderItems = items
        };
    }

    public static OrderItem ToOrderItem(this OrderItemDto itemDto)
    {
        return new OrderItem
        {
            OrderId = itemDto.OrderId,
            GameId = itemDto.GameId,
            GameTitle = itemDto.GameTitle,
            Price = itemDto.Price,
            Quantity = itemDto.Quantity,
        };
    }
}
