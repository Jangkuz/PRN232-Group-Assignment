namespace BuildingBlocks.Messaging.Events;
public record OrderCreatedEvent : IntegrationEvent
{
    public int CustomerId { get; set; }
    public string OrderName { get; set; } = default!;
    public List<OrderItem> OrderItems { get; set; } = default!;
}


public record OrderItem(
    Guid OrderId,
    int GameId,
    int Quantity,
    decimal Price);
