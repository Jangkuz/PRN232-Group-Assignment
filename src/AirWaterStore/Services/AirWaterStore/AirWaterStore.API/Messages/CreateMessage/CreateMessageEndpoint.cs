
namespace AirWaterStore.API.Messages.CreateMessage;

public record CreateMessageRequest(
    int ChatRoomId,
    int UserId,
    string Content,
    DateTime? SentAt
    );

public record CreateMessageResponse(int MessageId);

public class CreateMessageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/messages", async (CreateMessageRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateMessageCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateMessageResponse>();

            return Results.Created($"/messages/{response.MessageId}", response);
        })
        .WithName("CreateMessage")
        .Produces<CreateMessageResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Message")
        .WithDescription("Create Message");

    }
}
