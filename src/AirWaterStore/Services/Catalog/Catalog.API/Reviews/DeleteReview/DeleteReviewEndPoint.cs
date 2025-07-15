namespace Catalog.API.Reviews.DeleteReview;

//public record DeleteReviewRequest(int Id);
public record DeleteReviewResponse(bool IsSuccess);

public class DeleteReviewEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/reviews/{id}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteReviewCommand(id));

            var response = result.Adapt<DeleteReviewResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteReview")
        .Produces<DeleteReviewResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Review")
        .WithDescription("Delete Review");
    }
}
