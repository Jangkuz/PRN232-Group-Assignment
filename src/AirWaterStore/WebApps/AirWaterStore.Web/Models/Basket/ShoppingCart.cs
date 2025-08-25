namespace AirWaterStore.Web.Models.Basket;

public class ShoppingCart
{
    public int UserId { get; set; }
    public List<CartItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
}

public class CartItem
{
    public int GameId { get; set; }
    public string GameTitle { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

// wrapper classes
public record GetBasketResponse(ShoppingCart ShoppingCart);

public record StoreBasketRequest(ShoppingCart ShoppingCart);

public record CheckoutBasketRequest(int UserId);

public record CheckoutBasketResponse(bool IsSuccess);

public record StoreBasketResponse(int UserId);

public record DeleteBasketResponse(bool IsSuccess);

