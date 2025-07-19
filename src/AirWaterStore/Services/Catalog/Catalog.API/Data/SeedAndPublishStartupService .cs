using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Catalog.API.Data;

public class SeedAndPublishStartupService : IHostedService
{
    private readonly IServiceProvider _provider;

    public SeedAndPublishStartupService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _provider.CreateScope();
        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
        var documentStore = scope.ServiceProvider.GetRequiredService<IDocumentStore>();

        // Do your seeding logic manually
        using var session = documentStore.LightweightSession();

        if (!await session.Query<Game>().AnyAsync())
        {
            var games = await CatalogInitialData.GetPreconfigureGameAsync();

            foreach (var game in games)
            {
                session.Store<Game>(game);
                var evenMessage = new GameCreatedEvent
                {
                    GameId = game.Id,
                    Title = game.Title,
                    Price = game.Price
                };


                await session.SaveChangesAsync();
                // Publish event
                await publishEndpoint.Publish(evenMessage, cancellationToken);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;


}

