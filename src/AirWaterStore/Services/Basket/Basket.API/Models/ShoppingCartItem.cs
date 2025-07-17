namespace Basket.API.Models;

public class ShoppingCartItem
{
    public int Quantity { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public int GameId { get; set; }
    public string GameTitle { get; set; } = default!;
}
