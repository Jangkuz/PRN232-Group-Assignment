namespace Ordering.Domain.Models;
public class Game : Entity<GameId>
{
    public string Title { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    public int Quantity  { get; private set; } = default!;

    public static Game Create(GameId id, string name, decimal price, int quantity)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        ArgumentOutOfRangeException.ThrowIfNegative(quantity);

        var product = new Game
        {
            Id = id,
            Title = name,
            Price = price,
            Quantity = quantity
        };

        return product;
    }

    public void Update(
        string title,
        decimal price,
        int quantity
        )
    {
        Title = title;
        Price = price;
        Quantity = quantity;
    }
}

