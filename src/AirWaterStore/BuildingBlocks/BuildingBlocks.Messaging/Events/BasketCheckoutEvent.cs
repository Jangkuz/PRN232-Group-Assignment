namespace BuildingBlocks.Messaging.Events;
public record BasketCheckoutEvent : IntegrationEvent
{
    public int CustomerId { get; set; } = default!;
    //public string CustomerName { get; set; } = default!;
    public decimal TotalPrice { get; set; } = default!;

    // Payment
    public List<BasketItem> Items { get; set; } = [];
    //public int PaymentMethod { get; set; } = default!;
}

public record BasketItem (
    int GameId,
    int Quantity,
    decimal Price
    );
