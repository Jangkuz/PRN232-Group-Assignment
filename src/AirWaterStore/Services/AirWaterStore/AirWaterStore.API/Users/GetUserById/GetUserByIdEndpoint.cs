using AirWaterStore.API.Users.GetUsers;

namespace AirWaterStore.API.Users.GetUserById;
//public record GetUserByIdRequest(int Id);

public record GetUserByIdResponse(UserDto User);
public class GetUserByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{Id}", async (int Id, ISender sender) =>
        {
            var result = await sender.Send(new GetUserByIdQuery(Id));

            var response = result.Adapt<GetUserByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetUser")
        .Produces<GetUsersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get User")
        .WithDescription("Get User");
    }
}
