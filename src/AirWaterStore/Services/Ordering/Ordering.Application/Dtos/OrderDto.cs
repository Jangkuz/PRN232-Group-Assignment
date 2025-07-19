namespace Ordering.Application.Dtos;
public record OrderDto(
    Guid Id,
    int CustomerId,
    string OrderName,
    OrderStatus Status,
    List<OrderItemDto> OrderItems);
