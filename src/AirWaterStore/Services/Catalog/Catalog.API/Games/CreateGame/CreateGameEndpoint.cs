namespace Catalog.API.Games.CreateGame;

public record CreateGameRequest(
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

public record CreateGameResponse(int Id);
public class CreateGameEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/games", async (CreateGameRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateGameCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateGameResponse>();

            return Results.Created($"/games/{response.Id}", response);
        })
        .WithName("CreateGame")
        .Produces<CreateGameResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Game")
        .WithDescription("Create Game");
    }
}
