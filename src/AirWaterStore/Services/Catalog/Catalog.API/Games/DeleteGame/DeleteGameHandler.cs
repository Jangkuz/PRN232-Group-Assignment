﻿namespace Catalog.API.Games.DeleteGame;

public record DeleteGameCommand(int Id) : ICommand<DeleteGameResult>;
public record DeleteGameResult(bool IsSuccess);

public class DeleteGameCommandValidator : AbstractValidator<DeleteGameCommand>
{
    public DeleteGameCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Game ID is required");
    }
}

public class DeleteGameHandler
    (IDocumentSession session)
    : ICommandHandler<DeleteGameCommand, DeleteGameResult>
{
    public async Task<DeleteGameResult> Handle(DeleteGameCommand command, CancellationToken cancellationToken)
    {
        session.Delete<Game>(command.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteGameResult(true);
    }
}
