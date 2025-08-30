namespace AirWaterStore.Web.Models.Ordering;

public partial class Order
{
    public string OrderId { get; set; } = string.Empty;

    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; }

    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

}

public record GetOrderByIdResponse(OrderDto Order);
public record GetOrdersByCustomerIdResponse(IEnumerable<OrderDto> Orders);

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
    decimal TotalPrice,
    int status
    );

public record GetOrdersCountResponse(
    int TotalOrder,
    int PendingOrder,
    int CompletedOrder
    );

