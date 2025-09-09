namespace AirWaterStore.Web.Models.Ordering;

public partial class Order
{
    public string OrderId { get; set; } = string.Empty;

    public string OrderName { get; set; } = string.Empty;

    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; }

    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = string.Empty;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

}

public record GetOrderByIdResponse(OrderDto Order);
public record GetOrdersByCustomerIdResponse(IEnumerable<OrderDto> Orders);
public record UpdateOrderStatusResponse(
    bool IsSuccess
    );

public record UpdateOrderStatusRequest(OrderDto Order);

public record GetOrdersResponse(
    OrderPaging Orders
    );

public record OrderPaging(
    int PageIndex,
    int PageSize,
    int TotalCount,
    IEnumerable<OrderDto> Data
    );

public record OrderDto(
    string Id,
    int CustomerId,
    string CustomerName,
    string OrderName,
    int Status,
    decimal TotalPrice,
    IEnumerable<OrderItemDto> OrderItems
    );

public record GetOrdersCountResponse(
    int TotalOrder,
    int PendingOrder,
    int CompletedOrder
    );

