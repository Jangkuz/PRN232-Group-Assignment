namespace Basket.API.Models;

public class ShoppingCart
{
    public int UserId { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    public ShoppingCart(int userName)
    {
        UserId = userName;
    }

    //Required for Mapping
    public ShoppingCart()
    {
    }
}
