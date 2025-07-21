
namespace AirWaterStore.API.Users.Login;

public record LoginRequest(
    string Email,
    string Password
    );

public record LoginResponse(string Token);

public class LoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/users/login", async (LoginRequest request, ISender sender) =>
        {
            var query = request.Adapt<LoginQuery>();

            var result = await sender.Send(query);

            var response = result.Adapt<LoginResponse>();

            return Results.Ok(response);
        })
        .WithName("Login")
        .Produces<LoginResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Login User")
        .WithDescription("Login User");
    }
}

