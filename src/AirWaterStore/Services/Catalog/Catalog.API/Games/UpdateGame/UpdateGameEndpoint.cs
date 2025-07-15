namespace Catalog.API.Games.UpdateGame;

public record UpdateGameRequest(
    int Id,
    string ThumbnailUrl,
    string Title,
    string Description,
    List<string> Genre,
    string Developer,
    string Publisher,
    DateOnly ReleaseDate,
    decimal Price,
    int Quantity
    );
public record UpdateGameResponse(bool IsSuccess);

public class UpdateGameEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/games",
            async (UpdateGameRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateGameCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateGameResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateGame")
            .Produces<UpdateGameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Game")
            .WithDescription("Update Game");
    }
}
