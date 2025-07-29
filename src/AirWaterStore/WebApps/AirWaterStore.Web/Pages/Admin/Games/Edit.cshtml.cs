using AirWaterStore.Web.Models.Catalog;

namespace AirWaterStore.Web.Pages.Admin.Games;

public class EditModel(
    ICatalogService catalogService,
    ILogger<EditModel> logger
    ) : PageModel
{

    [BindProperty]
    public Game Game { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        // Check if user is staff
        if (!this.IsStaff())
        {
            return RedirectToPage("/Login");
        }

        var game = await catalogService.GetGame(id);

        if (game == null)
        {
            return NotFound();
        }

        Game = game.Game;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!this.IsStaff())
        {
            return RedirectToPage("/Login");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        logger.LogInformation("Update review visited");

        try
        {
            var gameDto = new UpdateGameDto(
                Id: Game.Id,
                ThumbnailUrl: Game.ThumbnailUrl!,
                Title: Game.Title,
                Description: Game.Description!,
                Genre: Game.Genres,
                Developer: Game.Developer!,
                Publisher: Game.Publisher!,
                ReleaseDate: (DateOnly)Game.ReleaseDate!,
                Price: Game.Price,
                Quantity: Game.Quantity
                );

            await catalogService.PutGame(gameDto);

            TempData["SuccessMessage"] = "Game updated successfully!";
            return RedirectToPage("/Games/Details", new { id = Game.Id });
        }
        catch (ApiException ex)
        {

            logger.LogWarning("Update review: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
            ModelState.AddModelError(string.Empty, "An error occurred while updating the game.");
        }
        return Page();
    }
}