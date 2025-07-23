
using AirWaterStore.Web.Services;

namespace AirWaterStore.Web.Pages.Games
{
    public class IndexModel (
        ICatalogService catalogService,
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
            //// Get success message from TempData
            //var successMessgae = TempData["SuccessMessage"];
            //if (successMessgae != null)
            //{
            //    SuccessMessage = successMessgae.ToString();
            //}

            var result = await catalogService.GetGames(1, 1000); // Get all for filtering

            var allGames = result.Games;

            //// Filter by search string
            if (!string.IsNullOrEmpty(SearchString))
            {
                allGames = allGames.Where(g =>
                    g.Title.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                    (g.Genre.ToString()?.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ?? false) ||
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
            //if (!this.IsAuthenticated() || !this.IsCustomer())
            //{
            //    return RedirectToPage("/Login");
            //}

            //// Get or create cart in session
            //var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            //var existingItem = cart.FirstOrDefault(c => c.GameId == gameId);
            //if (existingItem != null)
            //{
            //    existingItem.Quantity++;
            //}
            //else
            //{
            //    var game = await _gameService.GetByIdAsync(gameId);
            //    if (game != null && game.Quantity > 0)
            //    {
            //        cart.Add(new CartItem
            //        {
            //            GameId = gameId,
            //            Title = game.Title,
            //            Price = game.Price,
            //            Quantity = 1
            //        });
            //    }
            //}

            //HttpContext.Session.SetObjectAsJson("Cart", cart);
            //TempData["SuccessMessage"] = "Game added to cart!";

            return RedirectToPage(null, new
            { CurrentPage, SearchString });
        }
    }


}