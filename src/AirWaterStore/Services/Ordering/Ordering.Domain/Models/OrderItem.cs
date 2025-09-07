
namespace Ordering.Domain.Models;
public class OrderItem : Entity<OrderItemId>
{
    internal OrderItem(OrderId orderId, GameId gameId, int quantity, decimal price)
    {
        Id = OrderItemId.Of(Guid.NewGuid());
        OrderId = orderId;
        GameId = gameId;
        Quantity = quantity;
        Price = price;
    }

    public OrderId OrderId { get; private set; } = default!;
    public GameId GameId { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    public Game Game { get; private set; } = default!;
}
