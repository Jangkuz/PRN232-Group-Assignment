
namespace Catalog.API.Reviews.UpdateReview;
public record UpdateReviewCommand(
    int Id,
    int Rating,
    string Comment
    ) : ICommand<UpdateReviewResult>;

public record UpdateReviewResult(bool IsSuccess);

public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Review Id is required");
        RuleFor(x => x.Rating).NotEmpty().WithMessage("Rating is required");
        RuleFor(x => x.Comment).NotEmpty().WithMessage("Comment is required");
    }
}


internal class UpdateReviewHandler(IDocumentSession session) : ICommandHandler<UpdateReviewCommand, UpdateReviewResult>
{
    public async Task<UpdateReviewResult> Handle(UpdateReviewCommand command, CancellationToken cancellationToken)
    {
        //update Review entity from command object
        //save to database
        //return UpdateReviewResult result               

        var review = await session.LoadAsync<Review>(command.Id);

        if(review is null)
        {
            throw new ReviewNotFoundException(command.Id);
        }


        review.Rating = command.Rating;
        review.Comment = command.Comment;

        //save to database
        session.Update(review);
        await session.SaveChangesAsync(cancellationToken);

        //return result
        return new UpdateReviewResult(true);
    }
}
