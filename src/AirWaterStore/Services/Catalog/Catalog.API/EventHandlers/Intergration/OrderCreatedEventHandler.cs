using BuildingBlocks.Messaging.Events;
using Catalog.API.Games.UpdateGame;
using MassTransit;

namespace Catalog.API.EventHandlers.Intergration;

public class OrderCreatedEventHandler(
    ISender sender,
    IDocumentSession session,
    ILogger<OrderCreatedEventHandler> logger
    ) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var commands = await MapToUpdateGameCommand(context.Message);

        foreach (var command in commands)
        {
            await sender.Send(command);
        }
    }

    private async Task<List<UpdateGameCommand>> MapToUpdateGameCommand(OrderCreatedEvent message)
    {
        var gameList = await session.LoadManyAsync<Game>(message.OrderItems.Select(i => i.GameId).ToList());
        var orderItemDictionary = message.OrderItems.ToDictionary(o => o.GameId, o => o.Quantity);

        List<UpdateGameCommand> result = [];

        foreach (var game in gameList)
        {
            var command = new UpdateGameCommand(

                Id: game.Id,
                ThumbnailUrl: game.ThumbnailUrl!,
                Title: game.Title,
                Description: game.Description!,
                Genre: game.Genres,
                Developer: game.Developer!,
                Publisher: game.Publisher!,
                ReleaseDate: game.ReleaseDate ?? DateOnly.MinValue,
                Price: game.Price,
                Quantity: game.Quantity - orderItemDictionary[game.Id]
                );

            result.Add(command);
        }

        return result;
    }
}
