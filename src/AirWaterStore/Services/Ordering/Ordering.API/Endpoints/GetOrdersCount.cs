using Ordering.Application.Orders.Queries.GetOrdersCount;

namespace Ordering.API.Endpoints;

public record GetOrdersCountResponse(
    int TotalOrder, 
    int PendingOrder, 
    int CompletedOrder
    );

public class GetOrdersCount : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/count", async (ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersCountQuery());
            var response = new GetOrdersCountResponse(
                result.TotalOrder,
                result.PendingOrder,
                result.CompletedOrder);
            return Results.Ok(response);
        })
        .WithName("GetOrdersCount")
        .Produces<GetOrdersCountResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders Count")
        .WithDescription("Get Orders Count");
    }
}
