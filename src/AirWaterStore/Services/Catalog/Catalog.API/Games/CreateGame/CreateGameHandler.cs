using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Catalog.API.Games.CreateGame;
public record CreateGameCommand(
    string ThumbnailUrl,
    string Title,
    string Description,
    List<string> Genre,
    string Developer,
    string Publisher,
    DateOnly ReleaseDate,
    decimal Price,
    int Quantity
    ) : ICommand<CreateGameResult>;

public record CreateGameResult(int Id);
public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
    }
}

internal class CreateGameCommandHandler(
    IDocumentSession session,
    IPublishEndpoint publishEndpoint
    ) : ICommandHandler<CreateGameCommand, CreateGameResult>
{
    public async Task<CreateGameResult> Handle(CreateGameCommand command, CancellationToken cancellationToken)
    {
        //create Game entity from command object
        //save to database
        //return CreateGameResult result

        var game = new Game
        {
            Title = command.Title,
            ThumbnailUrl = command.ThumbnailUrl,
            Description = command.Description,
            Genre = command.Genre,
            Developer = command.Developer,
            Publisher = command.Publisher,
            ReleaseDate = command.ReleaseDate,
            Price = command.Price,
            Quantity = command.Quantity,
        };

        //save to database
        session.Store(game);
        await session.SaveChangesAsync(cancellationToken);

        var eventMessage = new GameCreatedEvent
        {
            GameId = game.Id,
            Title = game.Title,
            Price = game.Price
        };

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        //return result
        return new CreateGameResult(game.Id);
    }
}
