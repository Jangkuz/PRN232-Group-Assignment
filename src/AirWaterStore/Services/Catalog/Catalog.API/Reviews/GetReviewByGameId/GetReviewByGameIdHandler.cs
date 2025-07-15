namespace Catalog.API.Reviews.GetReviewByGameId;
public record GetReviewByGameIdQuery(int GameId) : IQuery<GetReviewByGameIdResult>;
public record GetReviewByGameIdResult(IEnumerable<Review> Review);
internal class GetReviewByGameIdHandler
    (IDocumentSession session)
    : IQueryHandler<GetReviewByGameIdQuery, GetReviewByGameIdResult>
{
    public async Task<GetReviewByGameIdResult> Handle(GetReviewByGameIdQuery query, CancellationToken cancellationToken)
    {
        var review = await session.Query<Review>()
            .Where(r => r.GameId == query.GameId)
            .ToListAsync(cancellationToken);

        if (review is null)
        {
            throw new ReviewNotFoundException(query.GameId);
        }

        return new GetReviewByGameIdResult(review);
    }
}
