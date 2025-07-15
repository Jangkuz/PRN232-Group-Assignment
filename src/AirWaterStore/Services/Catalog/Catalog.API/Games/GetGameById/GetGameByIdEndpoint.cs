namespace Catalog.API.Games.GetGameById;

//public record GetProductByIdRequest();
public record GetGameByIdResponse(Game Game);

public class GetReviewByGameIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/games/{id}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new GetGameByIdQuery(id));

            var response = result.Adapt<GetGameByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetGameById")
        .Produces<GetGameByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Game By Id")
        .WithDescription("Get Game By Id");
    }
}
