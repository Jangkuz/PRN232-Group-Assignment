namespace AirWaterStore.API.Users.GetUserTotalCount;

public record GetUserTotalCountResponse(int UserCount);

public class GetUserTotalCountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/count", async (ISender sender) =>
        {
            var result = await sender.Send(new GetUserTotalCountQuery());

            var response = result.Adapt<GetUserTotalCountResult>();

            return Results.Ok(response);
        })
        .WithName("GetUserTotalCount")
        .Produces<GetUserTotalCountResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get User Total Count")
        .WithDescription("Get User Total Count");
    }
}
