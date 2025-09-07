namespace Ordering.Application.Dtos;
public record OrderItemDto(
    Guid OrderId, 
    int GameId, 
    string GameTitle,
    int Quantity, 
    decimal Price);

