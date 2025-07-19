namespace Ordering.Domain.ValueObjects;
public record GameId
{
    public int Value { get; }

    private GameId(int value) => Value = value;
    public static GameId Of(int value)
    {
        if (value <= 0)
        {
            throw new DomainException("GameId must be a positive non-zero integer.");
        }
        return new GameId(value);
    }
    public override string ToString() => Value.ToString();
}

