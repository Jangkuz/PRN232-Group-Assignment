namespace Ordering.Application.Orders.Queries.GetOrderById;
public record GetOrderByIdQuery(string OrderId)
    : IQuery<GetOrdersByIdResult>;

public record GetOrdersByIdResult(IEnumerable<OrderDto> Orders);
