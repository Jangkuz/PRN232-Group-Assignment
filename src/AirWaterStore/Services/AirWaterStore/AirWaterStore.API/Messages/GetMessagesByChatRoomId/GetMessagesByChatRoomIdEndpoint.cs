
namespace AirWaterStore.API.Messages.GetMessagesByChatRoomId;

public record GetMessagesByChatRoomIdResponse(IEnumerable<MessageDto> Messages);

public class GetMessagesByChatRoomIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/messages/{chatRoomId}", async (int chatRoomId, ISender sender) =>
        {
            var result = await sender.Send(new GetMessagesByChatRoomIdQuery(chatRoomId));

            var response = result.Adapt<GetMessagesByChatRoomIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetMessagesByChatRoomId")
        .Produces<GetMessagesByChatRoomIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Messages By ChatRoom Id")
        .WithDescription("Get Messages By ChatRoom Id");
    }

}
