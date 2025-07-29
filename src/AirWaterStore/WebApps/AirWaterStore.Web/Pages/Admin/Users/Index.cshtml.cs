
namespace AirWaterStore.Web.Pages.Admin.Users;

public class IndexModel(
IAirWaterStoreService airWaterStoreService,
ILogger<IndexModel> logger
) : PageModel
{
    private const int PageSize = 10;

    public List<User> Users { get; set; } = new List<User>();
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
    public string? SuccessMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(int currentPage = 1)
    {
        // Check if user is staff
        if (!this.IsStaff())
        {
            return RedirectToPage(AppRouting.Login);
        }

        CurrentPage = currentPage;

        var successMessage = TempData["SuccessMessage"];
        if (successMessage != null)
        {
            SuccessMessage = successMessage.ToString();
        }

        try
        {

            var result = await airWaterStoreService.GetUsersPaging(this.GetCurrentUserId(), currentPage, PageSize);

            Users = result.Users.Data.Select(u =>
            {
                return new User
                {
                    Id = u.Id,
                    Email = u.Email,
                    Role = AppRole.GetRoleValue(u.Roles[0]),
                    IsBan = u.IsBan
                };
            }).ToList();


            var totalCount = result.Users.Count;
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

        }
        catch (ApiException ex)
        {

            logger.LogWarning("Get users list failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostBanAsync(int userId)
    {
        if (!this.IsStaff())
        {
            return Forbid();
        }

        try
        {
            var userResult = await airWaterStoreService.GetUserById(userId);
            if (userResult.User.Id != this.GetCurrentUserId())
            {
                var request = new UpdateUserStatusDto(
                    Id: userId,
                    IsBan: true,
                    Role: userResult.User.Roles[0]
                    );
                await airWaterStoreService.PutUserStatus(request);
                TempData["SuccessMessage"] = $"User {userResult.User.UserName} has been banned.";
            }

        }
        catch (ApiException ex)
        {
            logger.LogWarning("Banning user status failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUnbanAsync(int userId)
    {
        if (!this.IsStaff())
        {
            return Forbid();
        }

        try
        {
            var userResult = await airWaterStoreService.GetUserById(userId);
            if (userResult.User.Id != this.GetCurrentUserId())
            {
                var request = new UpdateUserStatusDto(
                    Id: userId,
                    IsBan: false,
                    Role: userResult.User.Roles[0]
                    );
                await airWaterStoreService.PutUserStatus(request);
                TempData["SuccessMessage"] = $"User {userResult.User.UserName} has been unbanned.";
            }

        }
        catch (ApiException ex)
        {
            logger.LogWarning("Unbanning user status failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostMakeStaffAsync(int userId)
    {
        if (!this.IsStaff())
        {
            return Forbid();
        }

        try
        {

            var userResult = await airWaterStoreService.GetUserById(userId);
            if (userResult != null && userResult.User.Roles[0] == AppConst.User)
            {
                var request = new UpdateUserStatusDto(
                    Id: userId,
                    IsBan: userResult.User.IsBan,
                    Role: AppConst.Staff
                    );
                await airWaterStoreService.PutUserStatus(request);
                TempData["SuccessMessage"] = $"User {userResult.User.UserName} has been promoted to Staff.";
            }

        }
        catch (ApiException ex)
        {
            logger.LogWarning("Staffing user status failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
        }

        return RedirectToPage();
    }
}