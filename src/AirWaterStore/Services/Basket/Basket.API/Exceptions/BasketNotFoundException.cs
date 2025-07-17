namespace Basket.API.Exceptions;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(int userId) : base("Basket", userId)
    {
    }
}
