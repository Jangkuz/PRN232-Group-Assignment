
using AirWaterStore.API.Users.Login;

namespace AirWaterStore.API.Users.UpdateUserStatus;
public record UpdateUserStatusRequest(
    int Id,
    bool IsBan,
    string Role
    );
public record UpdateUserStatusResponse(bool IsSuccess);

public class UpdateUserStatusEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapPut("/users", async (UpdateUserStatusRequest request, ISender sender) =>
        {
            var query = request.Adapt<UpdateUserStatusCommand>();

            var result = await sender.Send(query);

            var response = result.Adapt<UpdateUserStatusResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateUserStatus")
        .Produces<UpdateUserStatusResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update User Status")
        .WithDescription("Update User Status");
    }
}
