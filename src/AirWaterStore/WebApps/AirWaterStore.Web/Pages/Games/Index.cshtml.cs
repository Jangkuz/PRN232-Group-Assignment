

namespace AirWaterStore.Web.Pages.Games;

public class IndexModel(
    ICatalogService catalogService,
    IBasketService basketService,
    ILogger<IndexModel> logger
    ) : PageModel
{
    private const int PageSize = 9;

    public List<Game> Games { get; set; } = new List<Game>();

    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;

    [BindProperty(SupportsGet = true)]
    public string SearchString { get; set; } = string.Empty;
    public int TotalPages { get; set; }
    public string? SuccessMessage { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        // Get success message from TempData
        var successMessgae = TempData["SuccessMessage"];
        if (successMessgae != null)
        {
            SuccessMessage = successMessgae.ToString();
        }

        logger.LogInformation("Game list visited");

        var result = await catalogService.GetGames(1, 1000); // Get all for filtering

        var allGames = result.Games;

        //// Filter by search string
        if (!string.IsNullOrEmpty(SearchString))
        {
            allGames = allGames.Where(g =>
                g.Title.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                (g.GenresString?.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (g.Developer?.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ?? false)
            ).ToList();
        }

        //// Calculate pagination
        var totalCount = allGames.Count();
        TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

        // Get paginated results
        Games = allGames
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(int gameId)
    {

        logger.LogInformation("Add to cart button clicked");

        if (!this.IsAuthenticated() || !this.IsCustomer())
        {
            return RedirectToPage("/Login");
        }

        try
        {
            var gameResponse = await catalogService.GetGame(gameId);

            if (gameResponse.Game.Quantity < 1)
            {
                return RedirectToPage(null, new
                { CurrentPage, SearchString });
            }

            // Get or create cart in session
            var cart = await basketService.LoadUserBasket(this.GetCurrentUserId());

            var item = cart.Items.FirstOrDefault(i => i.GameId == gameId);

            if (item == null)
            {
                cart.Items.Add(new CartItem
                {
                    GameId = gameResponse.Game.Id,
                    GameTitle = gameResponse.Game.Title,
                    Price = gameResponse.Game.Price,
                    Quantity = 1
                });
            }
            else
            {
                item.Quantity++;
            }


            await basketService.StoreBasket(new StoreBasketRequest(cart));
            TempData["SuccessMessage"] = "Game added to cart!";
        }
        catch (ApiException ex)
        {
            logger.LogWarning("Add to cart failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
        }
        return RedirectToPage(null, new
        { CurrentPage, SearchString });
    }
}