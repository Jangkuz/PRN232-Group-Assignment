using MediatR;

namespace Basket.API.Basket.DeleteBasket;

//public record DeleteBasketRequest(int UserId);
public record DeleteBasketResponse(bool IsSuccess);
public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userId}", async (int userId, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(userId));

            var response = result.Adapt<DeleteBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteGame")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Game")
        .WithDescription("Delete Game");
    }
}
