namespace AirWaterStore.Web.Pages;

public class CartModel(
    ICatalogService catalogService,
    IBasketService basketService,
    ILogger<CartModel> logger
    ) : PageModel
{
    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    public decimal TotalPrice => CartItems.Sum(item => item.Price * item.Quantity);

    public async Task<IActionResult> OnGet()
    {
        if (!this.IsAuthenticated())
        {
            return RedirectToPage("/Login");
        }
        try
        {
            var cart = await basketService.LoadUserBasket(this.GetCurrentUserId());
            CartItems = cart.Items;
        }
        catch (ApiException ex)
        {
            logger.LogWarning("Get cart failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
        }
        return Page();
    }

    public async Task<IActionResult> OnPostUpdateQuantity(int gameId, int quantity)
    {
        if (!this.IsAuthenticated())
        {
            return RedirectToPage("/Login");
        }

        try
        {
            var gameResponse = await catalogService.GetGame(gameId);

            if (gameResponse.Game.Quantity < quantity)
            {
                return RedirectToPage();
            }

            var cart = await basketService.LoadUserBasket(this.GetCurrentUserId());

            var item = cart.Items.FirstOrDefault(i => i.GameId == gameId);

            if (item != null && quantity > 0)
            {
                item.Quantity = quantity;
                await basketService.StoreBasket(new StoreBasketRequest(cart));
            }

            await basketService.StoreBasket(new StoreBasketRequest(cart));
        }
        catch (ApiException ex)
        {
            logger.LogWarning("Update quantity failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostRemoveItem(int gameId)
    {
        if (!this.IsAuthenticated())
        {
            return RedirectToPage("/Login");
        }

        try
        {
            var cart = await basketService.LoadUserBasket(this.GetCurrentUserId());

            cart.Items.RemoveAll(i => i.GameId == gameId);

            await basketService.StoreBasket(new StoreBasketRequest(cart));
        }
        catch (ApiException ex)
        {
            logger.LogWarning("Remove cart failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostClearCart()
    {
        if (!this.IsAuthenticated())
        {
            return RedirectToPage("/Login");
        }

        try
        {
            var cart = await basketService.LoadUserBasket(this.GetCurrentUserId());
            cart.Items = [];
            await basketService.StoreBasket(new StoreBasketRequest(cart));
        }
        catch (ApiException ex)
        {
            logger.LogWarning("Remove cart failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
        }
        return RedirectToPage();
    }
}