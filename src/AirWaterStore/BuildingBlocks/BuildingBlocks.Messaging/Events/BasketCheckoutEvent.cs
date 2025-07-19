namespace BuildingBlocks.Messaging.Events;
public record BasketCheckoutEvent : IntegrationEvent
{
    public string UserName { get; set; } = default!;
    public int CustomerId { get; set; } = default!;
    public decimal TotalPrice { get; set; } = default!;

    // Payment
    public int PaymentMethod { get; set; } = default!;
}
