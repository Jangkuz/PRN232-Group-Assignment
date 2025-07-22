namespace Catalog.API.Games.GetGames;

//public record GetGamesQuery(
//    int? PageNumber = 1,
//    int? PageSize = 10
//    ) : IQuery<GetGamesResult>;

public record GetGamesQuery(
    PaginationRequest PaginationRequest
    ) : IQuery<GetGamesResult>;

public record GetGamesResult(IEnumerable<Game> Games);

internal class GetGamesHandler(IDocumentSession session)
    : IQueryHandler<GetGamesQuery, GetGamesResult>
{
    public async Task<GetGamesResult> Handle(GetGamesQuery query, CancellationToken cancellationToken)
    {
        var games = await session.Query<Game>()
           .ToPagedListAsync(query.PaginationRequest.PageIndex,
           query.PaginationRequest.PageSize,
           cancellationToken);

        return new GetGamesResult(games);
    }
}
