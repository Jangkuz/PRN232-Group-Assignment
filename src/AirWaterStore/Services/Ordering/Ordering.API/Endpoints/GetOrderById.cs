using Ordering.Application.Orders.Queries.GetOrderById;

namespace Ordering.API.Endpoints;

//- Accepts a name parameter.
//- Constructs a GetOrdersByIdQuery.
//- Retrieves and returns matching orders.

//public record GetOrdersByIdRequest(string Id);
public record GetOrdersByIdResponse(OrderDto Order);

public class GetOrdersById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderId}", async (string orderId, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderByIdQuery(orderId));

            var response = result.Adapt<GetOrdersByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrdersById")
        .Produces<GetOrdersByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders By Id")
        .WithDescription("Get Orders By Id");
    }
}

