using AirWaterStore.Web.Models.Ordering;

namespace AirWaterStore.Web.Pages.Admin;

public class DashboardModel(
    ICatalogService catalogService,
    IAirWaterStoreService airWaterStoreService,
    ILogger<DashboardModel> logger
    ) : PageModel
{

    public int TotalGames { get; set; }
    public int TotalOrders { get; set; }
    public int TotalUsers { get; set; }
    public int PendingChats { get; set; }
    public List<Order> RecentOrders { get; set; } = new List<Order>();
    public Dictionary<int, string> UserNames { get; set; } = new Dictionary<int, string>();

    public async Task<IActionResult> OnGetAsync()
    {
        // Check if user is staff
        if (!this.IsStaff())
        {
            return RedirectToPage(AppRouting.Login);
        }

        try
        {

        // Get statistics
        //TotalGames = await _gameService.GetTotalCountAsync();
        //TotalOrders = await _orderService.GetTotalCountAsync();
        var userResult = await airWaterStoreService.GetUserCount();

        TotalUsers = userResult.UserCount;

        //// Get pending chats (chats without assigned staff)
        //// var userId = HttpContext.Session.GetInt32(SessionParams.UserId);
        //var userId = this.GetCurrentUserId();
        //var allChats = await _chatRoomService.GetChatRoomsByUserIdAsync(userId);
        //PendingChats = allChats.Count(c => c.StaffId == null);

        //// Get recent orders
        //RecentOrders = await _orderService.GetAllAsync(1, 5);

        //// Load usernames for orders
        //foreach (var order in RecentOrders)
        //{
        //    if (!UserNames.ContainsKey(order.UserId))
        //    {
        //        var user = await _userService.GetByIdAsync(order.UserId);
        //        UserNames[order.UserId] = user?.Username ?? "Unknown User";
        //    }
        //}

        } catch (ApiException ex)
        {
            logger.LogWarning("Dashboard load failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
        }

        return Page();
    }

    public string GetUsername(int userId)
    {
        return UserNames.TryGetValue(userId, out var username) ? username : "Unknown User";
    }
}