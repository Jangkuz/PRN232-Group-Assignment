namespace AirWaterStore.Web.Pages.Orders;

public class DetailsModel(
    IOrderService orderService,
    IAirWaterStoreService airWaterStoreService,
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
    public List<OrderItem> OrderDetails { get; set; } = new();
    public string CustomerName { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (!this.IsAuthenticated())
        {
            return RedirectToPage("/Login");
        }

        try
        {

            var orderResult = await orderService.GetOrderById(id);

            Order = orderResult.Order.ToOrder();

            // Check authorization - customers can only see their own orders
            if (!this.IsStaff() && Order.UserId != this.GetCurrentUserId())
            {
                return Forbid();
            }

            OrderDetails = Order.OrderItems.ToList();
            var customer = await airWaterStoreService.GetUserById(Order.UserId);
            CustomerName = customer.User.UserName;

            return Page();


        }
        catch (ApiException ex)
        {
            logger.LogWarning("Get order failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
            return RedirectToPage(AppRouting.Home);

        }
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

    public async Task<IActionResult> OnPostCheckOutAsync(string orderId)
    {
        if (this.IsStaff())
        {
            return Unauthorized();
        }

        try
        {
            var order = orderService.GetOrderById(orderId).GetAwaiter().GetResult().Order.ToOrder();
            var orderDto = new OrderDto(
                Id: order.OrderId,
                CustomerId: order.UserId,
                CustomerName: order.UserName,
                OrderName: order.OrderName,
                TotalPrice: order.TotalPrice,
                Status: (int)OrderDtoStatus.Completed,
                OrderItems: []
                );
            var updateRequest = new UpdateOrderStatusRequest(orderDto);
            var response = await orderService.UpdateOrderStatus(updateRequest);

            // Implement 3rd party payment

            return RedirectToPage(new {id = orderId});
        }
        catch (ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            logger.LogWarning("Order not found: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
            return NotFound();
        }
        catch (ApiException ex)
        {
            logger.LogWarning("Update order status failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
            return RedirectToPage(AppRouting.Home);
        }
    }

}