namespace BuildingBlocks.Messaging.Events;
public record OrderCreatedEvent : IntegrationEvent
{
    public int CustomerId;
    public string OrderName = default!;
    public List<OrderItem> OrderItem = default!;
}


public record OrderItem(
    Guid OrderId,
    int GameId,
    int Quantity,
    decimal Price);
