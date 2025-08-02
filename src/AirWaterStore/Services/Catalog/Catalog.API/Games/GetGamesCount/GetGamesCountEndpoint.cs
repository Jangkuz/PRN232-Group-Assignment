
namespace Catalog.API.Games.GetGamesCount;
public record GetGamesCountResponse(int Count);

public class GetGamesCountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/games/count", async (ISender sender) =>
        {
            var count = await sender.Send(new GetGamesCountQuery());
            var response = count.Adapt<GetGamesCountResponse>();
            return Results.Ok(response);
        })
           .WithName("GetTotalGameCount")
           .Produces<GetGamesCountResponse>(StatusCodes.Status200OK)
           .WithSummary("Get total game count")
           .WithDescription("Returns the total number of games in the catalog");
    }
}
