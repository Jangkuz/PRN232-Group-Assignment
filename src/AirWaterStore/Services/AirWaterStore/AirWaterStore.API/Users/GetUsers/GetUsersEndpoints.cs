namespace AirWaterStore.API.Users.GetUsers;

//Staff only

public record GetUsersResponse(PaginatedResult<UserDto> Users);

public class GetUsersEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/users", async (int ExceptUserId, [AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetUsersQuery(ExceptUserId, request));

            var response = result.Adapt<GetUsersResult>();

            return Results.Ok(response);
        })
        .WithName("GetUsers")
        .Produces<GetUsersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Users")
        .WithDescription("Get Users");
    }
}
