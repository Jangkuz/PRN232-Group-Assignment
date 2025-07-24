
namespace AirWaterStore.Web.Services;

public interface ICatalogService
{
    [Get("/catalog-service/games?pageIndex={pageIndex}&pageSize={pageSize}")]
    Task<GetGamesResponse> GetGames(int? pageIndex = 1, int? pageSize = 10);

    [Get("/catalog-service/games/{id}")]
    Task<GetGameByIdResponse> GetGame(int id);

    [Get("/catalog-service/reviews/{gameId}")]
    Task<GetReviewsByGameIdResponse> GetReviewsByGameId(int gameId);
}
