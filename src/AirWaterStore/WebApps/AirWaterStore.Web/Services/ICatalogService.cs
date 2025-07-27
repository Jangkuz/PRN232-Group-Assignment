
namespace AirWaterStore.Web.Services;

public interface ICatalogService
{
    [Get("/catalog-service/games?pageIndex={pageIndex}&pageSize={pageSize}")]
    Task<GetGamesResponse> GetGames(int? pageIndex = 1, int? pageSize = 10);

    [Get("/catalog-service/games/{id}")]
    Task<GetGameByIdResponse> GetGame(int id);

    [Put("/catalog-service/games")]
    Task<UpdateGameResponse> PutGame(UpdateGameDto gameDto);

    [Get("/catalog-service/reviews/{gameId}")]
    Task<GetReviewsByGameIdResponse> GetReviewsByGameId(int gameId);

    [Post("/catalog-service/reviews")]
    Task<PostReviewResponse> PostReview(CreateReviewDto reviewDto);

    [Put("/catalog-service/reviews")]
    Task<PutReviewResponse> PutReview(UpdateReviewDto reviewDto);

    [Delete("/catalog-service/reviews/{reviewId}")]
    Task<DeleteReviewResponse> DeleteReview(int reviewId);
}
