using System.ComponentModel.DataAnnotations;

namespace AirWaterStore.Web.Pages.Games;

public class DetailsModel(
    ICatalogService catalogService,
    IBasketService basketService,
    ILogger<IndexModel> logger
    ) : PageModel
{

    public Game Game { get; set; } = default!;
    public List<Review> Reviews { get; set; } = [];
    public Dictionary<int, string> UserNames { get; set; } = new Dictionary<int, string>();

    [BindProperty]
    public ReviewInputModel NewReview { get; set; } = default!;

    public bool CanReview { get; set; }

    public class ReviewInputModel
    {
        public int GameId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [StringLength(1000)]
        public string Comment { get; set; } = string.Empty;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        logger.LogInformation("Game detail visited");

        try
        {
            var gameResult = await catalogService.GetGame(id);
            var game = gameResult.Game;
            if (game == null)
            {
                return NotFound();
            }

            Game = game;

            var reviewResult = await catalogService.GetReviewsByGameId(id);

            Reviews = [.. reviewResult.Reviews];

            // Load usernames for reviews
            foreach (var review in Reviews)
            {
                if (!UserNames.ContainsKey(review.UserId))
                {
                    UserNames[review.UserId] = review.UserName;
                }
            }

            // Check if current user can review (hasn't reviewed this game yet)
            if (this.IsCustomer() && this.IsAuthenticated())
            {
                CanReview = !Reviews.Any(r => r.UserId == this.GetCurrentUserId());
            }

            return Page();
        }
        catch (ApiException ex)
        {

            logger.LogWarning("Get games failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
            return RedirectToPage(AppRouting.Home);
        }

    }

    public async Task<IActionResult> OnPostAddToCartAsync(int gameId, int quantity = 1)
    {
        if (!this.IsAuthenticated() || !this.IsCustomer())
        {
            return RedirectToPage("/Login");
        }

        try
        {
            var gameResponse = await catalogService.GetGame(gameId);

            if (gameResponse.Game.Quantity < quantity)
            {
                return RedirectToPage();
            }

            // Get or create cart in session
            var cart = await basketService.LoadUserBasket(this.GetCurrentUserId());

            var item = cart.Items.FirstOrDefault(i => i.GameId == gameId);

            if(item == null)
            {
                cart.Items.Add(new CartItem
                {
                    GameId = gameResponse.Game.Id,
                    GameTitle = gameResponse.Game.Title,
                    Price = gameResponse.Game.Price,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity+=quantity;
            }

            await basketService.StoreBasket(new StoreBasketRequest(cart));
            TempData["SuccessMessage"] = "Game added to cart!";
        }
        catch (ApiException ex)
        {
            logger.LogWarning("Add to cart failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostAddReviewAsync()
    {
        if (!this.IsAuthenticated() || !this.IsCustomer())
        {
            return RedirectToPage(AppRouting.Login);
        }

        if (!ModelState.IsValid || !this.IsAuthenticated())
        {
            return await OnGetAsync(NewReview.GameId);
        }

        logger.LogInformation("Create review visited");

        try
        {
            var reviewDto = new CreateReviewDto
            (
                UserId: this.GetCurrentUserId(),
                UserName: this.GetCurrentUserName(),
                GameId: NewReview.GameId,
                Rating: NewReview.Rating,
                Comment: NewReview.Comment,
                ReviewDate: DateTime.UtcNow
            );

            await catalogService.PostReview(reviewDto);

        }
        catch (ApiException ex)
        {

            logger.LogWarning("Add review failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
        }
        return RedirectToPage(new { id = NewReview.GameId });
    }

    public async Task<IActionResult> OnPostUpdateReviewAsync(int reviewId, int gameId, int rating, string comment)
    {
        if (!this.IsAuthenticated() || !this.IsCustomer())
        {
            return RedirectToPage(AppRouting.Login);
        }

        logger.LogInformation("Update review visited");

        try
        {
            var reviewDto = new UpdateReviewDto
            (
                Id: reviewId,
                Rating: NewReview.Rating,
                Comment: NewReview.Comment
            );

            await catalogService.PutReview(reviewDto);

        }
        catch (ApiException ex)
        {

            logger.LogWarning("Update review failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
        }

        return RedirectToPage(new { id = gameId });
    }

    public async Task<IActionResult> OnPostDeleteReviewAsync(int reviewId, int gameId)
    {
        if (!this.IsAuthenticated() || !this.IsCustomer())
        {
            return RedirectToPage(AppRouting.Login);
        }

        logger.LogInformation("Delete review visited");

        try
        {

            await catalogService.DeleteReview(reviewId);

        }
        catch (ApiException ex)
        {

            logger.LogWarning("Delete review failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
        }
        return RedirectToPage(new { id = gameId });
    }

    public string GetUsername(int userId)
    {
        return UserNames.TryGetValue(userId, out var username) ? username : "Unknown User";
    }
}