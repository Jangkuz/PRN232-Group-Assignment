namespace AirWaterStore.Web.Pages.Admin.Games;

public class CreateModel(
    ICatalogService catalogService,
    ILogger<CreateModel> logger
    ) : PageModel
{

    [BindProperty]
    public Game Game { get; set; } = default!;

    public IActionResult OnGet()
    {
        // Check if user is staff
        if (!this.IsStaff())
        {
            return RedirectToPage(AppRouting.Login);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!this.IsStaff())
        {
            return RedirectToPage(AppRouting.Login);
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {

            var gameDto = new CreateGameDto(
                ThumbnailUrl: Game.ThumbnailUrl!,
                Title: Game.Title!,
                Description: Game.Description!,
                Genre: Game.Genres,
                Developer: Game.Developer!,
                Publisher: Game.Publisher!,
                ReleaseDate: (DateOnly)Game.ReleaseDate!,
                Price: Game.Price,
                Quantity: Game.Quantity
                );

            await catalogService.PostGame(gameDto);

            TempData["SuccessMessage"] = "Game created successfully!";

        }
        catch (ApiException ex)
        {

            logger.LogWarning("Create game failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
        }
        return RedirectToPage(AppRouting.Home);
    }
}