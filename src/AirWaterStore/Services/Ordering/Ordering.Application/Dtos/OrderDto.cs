namespace Ordering.Application.Dtos;
public record OrderDto(
    Guid Id,
    int CustomerId,
    string CustomerName,
    string OrderName,
    OrderStatus Status,
    List<OrderItemDto> OrderItems);
