namespace Ordering.Domain.Models;
public class Game : Entity<GameId>
{
    public string Title { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    public static Game Create(GameId id, string name, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var product = new Game
        {
            Id = id,
            Title = name,
            Price = price
        };

        return product;
    }
}

