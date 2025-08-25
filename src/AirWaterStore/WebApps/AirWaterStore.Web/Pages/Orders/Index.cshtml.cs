using AirWaterStore.Web.Models.Ordering;

namespace AirWaterStore.Web.Pages.Orders;

public class IndexModel (
    IOrderService orderService,
    ILogger<IndexModel> logger
    ) : PageModel
{
    private const int PageSize = 10;


    public List<Order> Orders { get; set; } = new List<Order>();
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }

    public async Task<IActionResult> OnGetAsync(int currentPage = 1)
    {
        if (!this.IsAuthenticated())
        {
            return RedirectToPage(AppRouting.Login);
        }

        CurrentPage = currentPage;

        if (this.IsStaff()) // Staff sees all orders
        {
            var ordersResult = await orderService.GetOrders(currentPage, PageSize);

            Orders = ordersResult.Orders.Data.Select(o => o.ToOrder()).ToList();

            var totalCount = orderService.GetTotalCountAsync().GetAwaiter().GetResult().TotalOrder;
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
        }
        else // Customer sees only their orders
        {
            var ordersResult = await orderService.GetOrdersByCustomerId(this.GetCurrentUserId());

            Orders = ordersResult.Orders.Select(o => o.ToOrder()).ToList();

            var totalCount = 10;
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
        }

        return Page();
    }
}