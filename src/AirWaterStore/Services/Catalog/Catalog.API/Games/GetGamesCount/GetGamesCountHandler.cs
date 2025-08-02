namespace Catalog.API.Games.GetGamesCount;


public record GetGamesCountQuery : IQuery<GetGamesCountResult>;

public record GetGamesCountResult(int Count);

internal  class GetGamesCountHandler(IDocumentSession session)
    : IQueryHandler<GetGamesCountQuery, GetGamesCountResult>
{
    public async Task<GetGamesCountResult> Handle(GetGamesCountQuery query, CancellationToken cancellationToken)
    {
        // Count all games in the database
        var count = await session.Query<Game>().CountAsync();
        return new GetGamesCountResult(count);
    }
}
