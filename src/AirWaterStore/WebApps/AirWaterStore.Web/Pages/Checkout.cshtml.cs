namespace AirWaterStore.Web.Pages;

public class CheckoutModel(
    ICatalogService catalogService,
    IBasketService basketService,
    ILogger<CheckoutModel> logger
    ) : PageModel
{

    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    public decimal TotalPrice => CartItems.Sum(item => item.Price * item.Quantity);
    public string ErrorMessage { get; set; } = string.Empty;

    public async Task<IActionResult> OnGet()
    {
        if (this.IsAuthenticated())
        {
            return RedirectToPage("/Login");
        }

        var cart = await basketService.LoadUserBasket(this.GetCurrentUserId());

        CartItems = cart.Items;

        if (!CartItems.Any())
        {
            return RedirectToPage("/Cart");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (this.IsAuthenticated())
        {
            return RedirectToPage("/Login");
        }

        var cart = await basketService.LoadUserBasket(this.GetCurrentUserId());

        CartItems = cart.Items;

        if (!CartItems.Any())
        {
            return RedirectToPage("/Cart");
        }

        try
        {
            // Validate stock availability
            foreach (var item in CartItems)
            {
                var gameResult = await catalogService.GetGame(item.GameId);
                var game = gameResult.Game;
                if (game == null || game.Quantity < item.Quantity)
                {
                    ErrorMessage = $"'{item.GameTitle}' is out of stock or insufficient quantity available.";
                    return Page();
                }
            }


            var request = new CheckoutBasketRequest(UserId: this.GetCurrentUserId());

            var checkoutResponse = await basketService.CheckoutBasket(request);


            // Create order details and update stock
            //foreach (var item in CartItems)
            //{
            //    var orderDetail = new OrderDetail
            //    {
            //        OrderId = order.OrderId,
            //        GameId = item.GameId,
            //        Quantity = item.Quantity,
            //        Price = item.Price
            //    };

            //    await _orderDetailService.AddAsync(orderDetail);

            //    // Update game stock
            //    var game = await _gameService.GetByIdAsync(item.GameId);
            //    if (game != null)
            //    {
            //        game.Quantity -= item.Quantity;
            //        await _gameService.UpdateAsync(game);
            //    }
            //}

            // Clear cart
            //HttpContext.Session.Remove("Cart");

            // Redirect to order confirmation
            return RedirectToPage(AppRouting.Order);

            //string paymentLink = _paymentService.CreatePayment(order);

            //return Redirect(paymentLink);
        }
        catch (ApiException ex)
        {
            logger.LogWarning("Checkout failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
            ErrorMessage = "An error occurred while processing your order. Please try again.";
            return Page();
        }
    }
}