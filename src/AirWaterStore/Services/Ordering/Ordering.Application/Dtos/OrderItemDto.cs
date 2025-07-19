namespace Ordering.Application.Dtos;
public record OrderItemDto(
    Guid OrderId, 
    int GameId, 
    int Quantity, 
    decimal Price);

