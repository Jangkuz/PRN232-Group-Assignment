namespace Ordering.Domain.ValueObjects;
public record CustomerId
{
    public int Value { get; }
    private CustomerId(int value) => Value = value;
    public static CustomerId Of(int value)
    {
        if (value <= 0)
        {
            throw new DomainException("CustomerId must be a positive non-zero integer.");
        }
        return new CustomerId(value);
    }
}
