
namespace Catalog.API.Games.GetGames;

public record GetGamesRequest(
    int? PageNumber = 1, 
    int? PageSize = 10);
public record GetGamesResponse(IEnumerable<Game> Games);

public class GetGamesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/games", async ([AsParameters] GetGamesRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetGamesQuery>();

            var result = await sender.Send(query);

            var response = result.Adapt<GetGamesResponse>();

            return Results.Ok(response);
        })
        .WithName("GetGames")
        .Produces<GetGamesResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Games")
        .WithDescription("Get Games");
    }
}
