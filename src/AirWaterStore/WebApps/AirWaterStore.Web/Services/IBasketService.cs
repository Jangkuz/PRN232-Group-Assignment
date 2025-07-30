using System.Net;

namespace AirWaterStore.Web.Services;

public interface IBasketService
{
    [Get("/basket-service/basket/{userId}")]
    Task<GetBasketResponse> GetBasket(int userId);

    [Post("/basket-service/basket")]
    Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

    [Delete("/basket-service/basket/{userId}")]
    Task<DeleteBasketResponse> DeleteBasket(int userId);

    //[Post("/basket-service/basket/checkout")]
    //Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);

    public async Task<ShoppingCart> LoadUserBasket(int userId)
    {
        // Get Basket If Not Exist Create New Basket with Default Logged In User Name: swn
        //var userName = "swn";
        ShoppingCart basket;

        try
        {
            var getBasketResponse = await GetBasket(userId);
            basket = getBasketResponse.ShoppingCart;
        }
        catch (ApiException apiException) when (apiException.StatusCode == HttpStatusCode.NotFound)
        {
            basket = new ShoppingCart
            {
                UserId = userId,
                Items = []
            };
        }

        return basket;
    }
}
