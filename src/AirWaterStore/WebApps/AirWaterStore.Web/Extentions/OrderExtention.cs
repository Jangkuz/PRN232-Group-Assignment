
namespace AirWaterStore.Web.Extentions;

public static class OrderExtention
{

    public static Order ToOrder(this OrderDto orderDto)
    {
        return new Order
        {
            OrderId = orderDto.Id,
            UserId = orderDto.CustomerId,
            UserName = orderDto.CustomerName,
            TotalPrice = orderDto.TotalPrice,
            Status = orderDto.status.ToString(),
            OrderDetails = new List<OrderDetail>() // Initialize with an empty list
        };
    }
}
