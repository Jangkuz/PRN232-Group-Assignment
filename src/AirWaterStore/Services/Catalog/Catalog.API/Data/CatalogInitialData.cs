using System.Text.Json;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if (!await session.Query<Game>().AnyAsync())
        {
            session.Store<Game>(await GetPreconfigureGameAsync());
            await session.SaveChangesAsync();
        }

        if (!await session.Query<Review>().AnyAsync())
        {
            session.Store<Review>(await GetPreconfigureReviewAsync());
            await session.SaveChangesAsync();
        }
    }

    private static async Task<IEnumerable<Game>> GetPreconfigureGameAsync(){
        var json = await File.ReadAllTextAsync("Data/steam_games.json");
        var rawList = JsonSerializer.Deserialize<List<GameJsonDto>>(json);

        return rawList?
            .Select(ReadGameData.ConvertToGame)
            .ToList() ?? new();
    }

    private static async Task<IEnumerable<Review>> GetPreconfigureReviewAsync()
    {
        var json = await File.ReadAllTextAsync("Data/steam_reviews.json");
        var rawList = JsonSerializer.Deserialize<List<ReviewJsonDto>>(json);

        return rawList?
            .Select(ReadReviewData.ConvertToReview)
            .ToList() ?? new();
    }
}
