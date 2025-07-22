
namespace AirWaterStore.API.Messages.CreateMessage;
public record CreateMessageCommand(
    int ChatRoomId,
    int UserId,
    string Content,
    DateTime? SentAt
    ) : ICommand<CreateMessageResult>;

public record CreateMessageResult(int MessageId);

internal class CreateMessageHandler (
    ApplicationDbContext dbContext
    ) : ICommandHandler<CreateMessageCommand, CreateMessageResult>
{
    public async Task<CreateMessageResult> Handle(CreateMessageCommand command, CancellationToken cancellationToken)
    {
        var message = command.Adapt<Message>();
        
        await dbContext.Messages.AddAsync(message);

        await dbContext.SaveChangesAsync();

        return new CreateMessageResult(message.MessageId);

    }
}
