

using Basket.API.Exceptions;

namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session)
    : IBasketRepository
{

    public async Task<ShoppingCart> GetBasket(int userId, CancellationToken cancellationToken = default)
    {
        var basket = await session.LoadAsync<ShoppingCart>(userId, cancellationToken);

        return basket is null ? throw new BasketNotFoundException(userId)
            : basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        session.Store(basket);
        await session.SaveChangesAsync();
        return basket;
    }
    public async Task<bool> DeleteBasket(int userId, CancellationToken cancellationToken = default)
    {
        session.Delete<ShoppingCart>(userId);
        await session.SaveChangesAsync();
        return true;
    }
}
