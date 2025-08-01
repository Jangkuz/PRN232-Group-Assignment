using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Ordering.Application.Orders.EventHandlers.Intergration;
public class GameCreatedEventHandler (
    IApplicationDbContext dbContext,
    ILogger<GameCreatedEventHandler> logger
    ) : IConsumer<GameCreatedEvent>
{
    public async Task Consume(ConsumeContext<GameCreatedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        await AddGame(context.Message, context.CancellationToken);
    }

    private async Task AddGame(GameCreatedEvent message, CancellationToken cancellation)
    {
        var game = Game.Create(GameId.Of(message.GameId), message.Title, message.Price, message.Quantity);
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync(cancellation);
    }
}
