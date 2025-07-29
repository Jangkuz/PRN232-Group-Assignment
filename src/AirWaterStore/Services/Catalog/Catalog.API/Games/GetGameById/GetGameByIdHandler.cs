namespace Catalog.API.Games.GetGameById;
public record GetGameByIdQuery(int Id) : IQuery<GetGameByIdResult>;
public record GetGameByIdResult(Game Game);
internal class GetReviewByGameIdHandler
    (IDocumentSession session)
    : IQueryHandler<GetGameByIdQuery, GetGameByIdResult>
{
    public async Task<GetGameByIdResult> Handle(GetGameByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Game>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new GameNotFoundException(query.Id);
        }

        return new GetGameByIdResult(product);
    }
}
