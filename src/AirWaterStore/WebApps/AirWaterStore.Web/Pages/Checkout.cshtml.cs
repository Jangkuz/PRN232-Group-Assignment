using AirWaterStore.Web.Models.Basket;
using System.Threading.Tasks;

namespace AirWaterStore.Web.Pages;

public class CheckoutModel (
    ICatalogService catalogService,
    IBasketService basketService,
    ILogger<CheckoutModel> logger
    ) : PageModel
{
    //private readonly IOrderService _orderService;
    //private readonly IOrderDetailService _orderDetailService;
    //private readonly IGameService _gameService;
    //private readonly IVnPayService _paymentService;

    //public CheckoutModel(IOrderService orderService, IOrderDetailService orderDetailService, IGameService gameService, IVnPayService paymentService)
    //{
    //    _orderService = orderService;
    //    _orderDetailService = orderDetailService;
    //    _gameService = gameService;
    //    _paymentService = paymentService;
    //}

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

    //public async Task<IActionResult> OnPostAsync()
    //{
    //    if (this.IsAuthenticated())
    //    {
    //        return RedirectToPage("/Login");
    //    }

    //    var cart = await basketService.LoadUserBasket(this.GetCurrentUserId());

    //    CartItems = cart.Items;

    //    if (!CartItems.Any())
    //    {
    //        return RedirectToPage("/Cart");
    //    }

    //    try
    //    {
    //        // Validate stock availability
    //        foreach (var item in CartItems)
    //        {
    //            var game = await _gameService.GetByIdAsync(item.GameId);
    //            if (game == null || game.Quantity < item.Quantity)
    //            {
    //                ErrorMessage = $"'{item.Title}' is out of stock or insufficient quantity available.";
    //                return Page();
    //            }
    //        }

    //        // Create order
    //        var order = new Order
    //        {
    //            UserId = userId.Value,
    //            OrderDate = DateTime.Now,
    //            TotalPrice = TotalPrice,
    //            Status = OrderStatus.Pending
    //        };

    //        await _orderService.AddAsync(order);

    //        // Create order details and update stock
    //        foreach (var item in CartItems)
    //        {
    //            var orderDetail = new OrderDetail
    //            {
    //                OrderId = order.OrderId,
    //                GameId = item.GameId,
    //                Quantity = item.Quantity,
    //                Price = item.Price
    //            };

    //            await _orderDetailService.AddAsync(orderDetail);

    //            // Update game stock
    //            var game = await _gameService.GetByIdAsync(item.GameId);
    //            if (game != null)
    //            {
    //                game.Quantity -= item.Quantity;
    //                await _gameService.UpdateAsync(game);
    //            }
    //        }

    //        // Clear cart
    //        HttpContext.Session.Remove("Cart");

    //        // Redirect to order confirmation
    //        //return RedirectToPage("/Orders/Details", new { id = order.OrderId });

    //        string paymentLink = _paymentService.CreatePayment(order);

    //        return Redirect(paymentLink);
    //    }
    //    catch
    //    {
    //        ErrorMessage = "An error occurred while processing your order. Please try again.";
    //        return Page();
    //    }
    //}
}