namespace AirWaterStore.Web.Pages.Orders;

public class DetailsModel(
    IOrderService orderService,
    ILogger<DetailsModel> logger
    ) : PageModel
{
    //private readonly IOrderService _orderService;
    //private readonly IOrderDetailService _orderDetailService;
    //private readonly IUserService _userService;
    //private readonly IVnPayService _paymentService;
    //private readonly VnPayConfig _vpnPayConfig;

    //[BindProperty(SupportsGet = true)]
    //public string Vnp_TxnRef { get; set; }
    //[BindProperty(SupportsGet = true)]
    //public string Vnp_TransactionStatus { get; set; }
    //[BindProperty(SupportsGet = true)]
    //public string Vnp_ResponseCode { get; set; }
    //[BindProperty(SupportsGet = true)]
    //public string Vnp_SecureHash { get; set; }

    public Order Order { get; set; } = default!;
    public List<OrderDetail> OrderDetails { get; set; } = new();
    public string CustomerName { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        //if (!this.IsAuthenticated())
        //{
        //    return RedirectToPage("/Login");
        //}

        //Order? order = null;

        //order = await orderService.GetOrderById(id);

        //if (order == null)
        //{
        //    return NotFound();
        //}

        //Order = order;

        //// Check authorization - customers can only see their own orders
        //if (!this.IsStaff() && Order.UserId != this.GetCurrentUserId())
        //{
        //    return Forbid();
        //}

        //OrderDetails = await _orderDetailService.GetAllByOrderIdAsync(Order.OrderId);

        //var customer = await _userService.GetByIdAsync(Order.UserId);
        //CustomerName = customer?.Username ?? "Unknown Customer";

        return Page();
    }

    //public async Task<IActionResult> OnPostUpdateStatusAsync(int orderId, string status)
    //{
    //    if (!this.IsStaff())
    //    {
    //        return Forbid();
    //    }

    //    var order = await _orderService.GetByIdAsync(orderId);
    //    if (order != null)
    //    {
    //        order.Status = status;
    //        await _orderService.UpdateAsync(order);
    //    }

    //    return RedirectToPage(new { id = orderId });
    //}

    //public async Task<IActionResult> OnPostCheckOutAsync(int orderId)
    //{
    //    if (this.IsStaff())
    //    {
    //        return Unauthorized();
    //    }

    //    var order = await _orderService.GetByIdAsync(orderId);
    //    if (order == null)
    //    {
    //        return NotFound();
    //    }

    //    string paymentLink = _paymentService.CreatePayment(order);

    //    return Redirect(paymentLink);
    //}

}