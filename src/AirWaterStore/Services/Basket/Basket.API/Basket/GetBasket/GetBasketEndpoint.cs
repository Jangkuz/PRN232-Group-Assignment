
using MediatR;

namespace Basket.API.Basket.GetBasket;

//public record GetBasketRequest(int UserId);
public record GetBasketResponse(ShoppingCart ShoppingCart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userId}", async (int userId, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(userId));

            var respose = result.Adapt<GetBasketResponse>();

            return Results.Ok(respose);
        })
        .WithName("GetGameById")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Game By Id")
        .WithDescription("Get Game By Id");
    }
}
