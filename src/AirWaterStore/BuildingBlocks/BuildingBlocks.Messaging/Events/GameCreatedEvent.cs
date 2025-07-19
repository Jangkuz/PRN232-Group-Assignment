namespace BuildingBlocks.Messaging.Events;
public record GameCreatedEvent : IntegrationEvent
{
    public int GameId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}
