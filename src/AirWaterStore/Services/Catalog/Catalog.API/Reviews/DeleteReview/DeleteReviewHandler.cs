namespace Catalog.API.Reviews.DeleteReview;
public record DeleteReviewCommand(int Id) : ICommand<DeleteReviewResult>;
public record DeleteReviewResult(bool IsSuccess);

public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
{
    public DeleteReviewCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Review ID is required");
    }
}

public class DeleteReviewHandler
    (IDocumentSession session)
    : ICommandHandler<DeleteReviewCommand, DeleteReviewResult>
{
    public async Task<DeleteReviewResult> Handle(DeleteReviewCommand command, CancellationToken cancellationToken)
    {
        session.Delete<Review>(command.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteReviewResult(true);
    }
}
