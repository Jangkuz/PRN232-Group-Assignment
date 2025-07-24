namespace Catalog.API.Games.UpdateGame;

public record UpdateGameCommand(
    int Id,
    string ThumbnailUrl,
    string Title,
    string Description,
    List<string> Genre,
    string Developer,
    string Publisher,
    DateOnly ReleaseDate,
    decimal Price,
    int Quantity
    ) : ICommand<UpdateGameResult>;
public record UpdateGameResult(bool IsSuccess);

public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
{
    public UpdateGameCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Game ID is required");

        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Title is required")
            .Length(2, 150).WithMessage("Title must be between 2 and 150 characters");

        RuleFor(command => command.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

internal class UpdateGameHandler
    (IDocumentSession session)
    : ICommandHandler<UpdateGameCommand, UpdateGameResult>
{
    public async Task<UpdateGameResult> Handle(UpdateGameCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Game>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new GameNotFoundException(command.Id);
        }

        product.Title = command.Title;
        product.ThumbnailUrl = command.ThumbnailUrl;
        product.Description = command.Description;
        product.Genres = command.Genre;
        product.Developer = command.Developer;
        product.Publisher = command.Publisher;
        product.ReleaseDate = command.ReleaseDate;
        product.Price = command.Price;
        product.Quantity = command.Quantity;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateGameResult(true);
    }
}

