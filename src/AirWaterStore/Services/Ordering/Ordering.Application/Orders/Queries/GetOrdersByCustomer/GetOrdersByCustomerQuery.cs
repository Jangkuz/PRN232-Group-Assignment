namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;
public record GetOrdersByCustomerQuery(int CustomerId)
    : IQuery<GetOrdersByCustomerResult>;

public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);
