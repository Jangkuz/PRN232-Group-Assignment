
namespace AirWaterStore.API.Messages.GetMessagesByChatRoomId;

public record GetMessagesByChatRoomIdQuery(
    int ChatRoomId
    ) : IQuery<GetMessagesByChatRoomIdResult>;

public record GetMessagesByChatRoomIdResult(IEnumerable<MessageDto> Messages);

internal class GetMessagesByChatRoomIdHandler (
    ApplicationDbContext dbContext
    ) : IQueryHandler<GetMessagesByChatRoomIdQuery, GetMessagesByChatRoomIdResult>
{
    public Task<GetMessagesByChatRoomIdResult> Handle(GetMessagesByChatRoomIdQuery query, CancellationToken cancellationToken)
    {
        var messages = dbContext.Messages
            .Where(m => m.ChatRoomId == query.ChatRoomId)
            .OrderBy(m => m.SentAt)
            .ToList();

        return Task.FromResult(new GetMessagesByChatRoomIdResult(
            Messages: messages.Select(m => m.Adapt<MessageDto>())
            ));
    }
}
