namespace Catalog.API.Reviews.CreateReview;

public record CreateReviewRequest(
    int UserId,
    int GameId,
    int Rating,
    string Comment,
    DateTime ReviewDate
    );

public record CreateReviewResponse(int Id);
public class CreateReviewEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/reviews", async (CreateReviewRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateReviewCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateReviewResponse>();

            return Results.Created($"/reviews/{response.Id}", response);
        })
        .WithName("CreateReview")
        .Produces<CreateReviewResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Review")
        .WithDescription("Create Review");
    }
}
