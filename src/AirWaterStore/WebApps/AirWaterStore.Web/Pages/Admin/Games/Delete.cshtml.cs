namespace AirWaterStore.Web.Pages.Admin.Games;

public class DeleteModel(
ICatalogService catalogService,
ILogger<DeleteModel> logger
) : PageModel
{


    [BindProperty]
    public Game Game { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        //Check if user is staff
        if (!this.IsStaff())
        {
            return RedirectToPage(AppRouting.Login);
        }

        try
        {
            logger.LogInformation("Delete game visited");

            var game = await catalogService.GetGame(id);

            Game = game.Game;
            return Page();
        }
        catch (ApiException ex)

        {

            logger.LogWarning("Delete game failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);

            return NotFound();
        }
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!this.IsStaff())
        {
            return RedirectToPage(AppRouting.Login);
        }

        try
        {
            await catalogService.DeleteGame(id);
            TempData["SuccessMessage"] = "Game deleted successfully!";
            return RedirectToPage(AppRouting.Home);
        }
        catch (ApiException ex)
        {
            logger.LogWarning("Delete game failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
            // If deletion fails (e.g., due to foreign key constraints)
            TempData["ErrorMessage"] = "Cannot delete this game because it has associated orders or reviews.";
            return RedirectToPage(AppRouting.GameDetail, new { id = id });
        }
    }
}