namespace Catalog.API.Reviews.CreateReview;
public record CreateReviewCommand(
    int UserId,
    string UserName,
    int GameId,
    int Rating,
    string Comment,
    DateTime ReviewDate
    ) : ICommand<CreateReviewResult>;

public record CreateReviewResult(int Id);
public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
        RuleFor(x => x.GameId).NotEmpty().WithMessage("GameId is required");
        RuleFor(x => x.Rating).NotEmpty().WithMessage("Rating is required");
        RuleFor(x => x.Comment).NotEmpty().WithMessage("Comment is required");
        RuleFor(x => x.ReviewDate).NotEmpty().WithMessage("ReviewDate is required");
    }
}

internal class CreateReviewCommandHandler(IDocumentSession session) : ICommandHandler<CreateReviewCommand, CreateReviewResult>
{
    public async Task<CreateReviewResult> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
    {
        //create Review entity from command object
        //save to database
        //return CreateReviewResult result               

        var review = new Review
        {
            UserId = command.UserId,
            UserName = command.UserName,
            GameId = command.GameId,
            Rating = command.Rating,
            Comment = command.Comment,
            ReviewDate = command.ReviewDate
        };

        //save to database
        session.Store(review);
        await session.SaveChangesAsync(cancellationToken);

        //return result
        return new CreateReviewResult(review.Id);
    }
}
