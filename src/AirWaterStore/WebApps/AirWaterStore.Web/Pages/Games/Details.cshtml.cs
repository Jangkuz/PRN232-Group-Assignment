using AirWaterStore.Web.Models.Catalog;
using AirWaterStore.Web.Services;
using System.ComponentModel.DataAnnotations;

namespace AirWaterStore.Web.Pages.Games
{
    public class DetailsModel(
        ICatalogService catalogService,
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
            } catch(ApiException ex)
            {

            logger.LogWarning("Login failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
                return RedirectToPage(AppRouting.Home);
            }

        }

        //public async Task<IActionResult> OnPostAddToCartAsync(int gameId, int quantity = 1)
        //{
        //    if (!this.IsAuthenticated() || !this.IsCustomer())
        //    {
        //        return RedirectToPage("/Login");
        //    }

        //    var game = await _gameService.GetByIdAsync(gameId);
        //    if (game == null || game.Quantity < quantity)
        //    {
        //        return RedirectToPage();
        //    }

        //    var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

        //    var existingItem = cart.FirstOrDefault(c => c.GameId == gameId);
        //    if (existingItem != null)
        //    {
        //        existingItem.Quantity += quantity;
        //    }
        //    else
        //    {
        //        cart.Add(new CartItem
        //        {
        //            GameId = gameId,
        //            Title = game.Title,
        //            Price = game.Price,
        //            Quantity = quantity
        //        });
        //    }

        //    HttpContext.Session.SetObjectAsJson("Cart", cart);
        //    TempData["SuccessMessage"] = "Game added to cart!";

        //    return RedirectToPage();
        //}

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

                logger.LogWarning("Login failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
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

                logger.LogWarning("Login failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
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

                logger.LogWarning("Login failed: {StatusCode}, {Content}", ex.StatusCode, ex.Content);
            }
            return RedirectToPage(new { id = gameId});
        }

        public string GetUsername(int userId)
        {
            return UserNames.TryGetValue(userId, out var username) ? username : "Unknown User";
        }
    }
}