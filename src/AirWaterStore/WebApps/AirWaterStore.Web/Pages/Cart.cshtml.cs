using AirWaterStore.Web.Models.Basket;

namespace AirWaterStore.Web.Pages;

public class CartModel : PageModel
{
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    public decimal TotalPrice => CartItems.Sum(item => item.Price * item.Quantity);

    public IActionResult OnGet()
    {
        if (!this.IsAuthenticated())
        {
            return RedirectToPage("/Login");
        }

        CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        return Page();
    }

    //public IActionResult OnPostUpdateQuantity(int gameId, int quantity)
    //{
    //    var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
    //    var item = cart.FirstOrDefault(c => c.GameId == gameId);

    //    if (item != null && quantity > 0)
    //    {
    //        item.Quantity = quantity;
    //        HttpContext.Session.SetObjectAsJson("Cart", cart);
    //    }

    //    return RedirectToPage();
    //}

    //public IActionResult OnPostRemoveItem(int gameId)
    //{
    //    var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
    //    cart.RemoveAll(c => c.GameId == gameId);
    //    HttpContext.Session.SetObjectAsJson("Cart", cart);

    //    return RedirectToPage();
    //}

    //public IActionResult OnPostClearCart()
    //{
    //    HttpContext.Session.Remove("Cart");
    //    return RedirectToPage();
    //}
}