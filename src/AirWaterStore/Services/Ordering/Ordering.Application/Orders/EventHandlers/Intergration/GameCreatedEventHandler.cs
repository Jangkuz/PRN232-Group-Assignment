using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Data;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Intergration;
public class GameCreatedEventHandler (
    ISender sender,
    IApplicationDbContext dbContext,
    ILogger<GameCreatedEventHandler> logger
    ) : IConsumer<GameCreatedEvent>
{
    public async Task Consume(ConsumeContext<GameCreatedEvent> context)
    {
        // TODO: Create new order and start order fullfillment process
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        await AddGame(context.Message, context.CancellationToken);
        //await sender.Send(command);
    }

    private async Task AddGame(GameCreatedEvent message, CancellationToken cancellation)
    {
        // Create full game with incoming event data

        var game = Game.Create(GameId.Of(message.GameId), message.Title, message.Price);
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync(cancellation);
    }
}
