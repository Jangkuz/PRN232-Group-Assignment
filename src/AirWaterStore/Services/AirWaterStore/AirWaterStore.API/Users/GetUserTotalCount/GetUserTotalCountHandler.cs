


using Microsoft.EntityFrameworkCore;

namespace AirWaterStore.API.Users.GetUserTotalCount;

public record GetUserTotalCountQuery() : IQuery<GetUserTotalCountResult>;

public record GetUserTotalCountResult(int UserCount);

internal class GetUserTotalCountHandler(
    ApplicationDbContext dbContext
    ) : IQueryHandler<GetUserTotalCountQuery, GetUserTotalCountResult>
{
    public async Task<GetUserTotalCountResult> Handle(GetUserTotalCountQuery query, CancellationToken cancellationToken)
    {
        var count = await dbContext.Users.CountAsync(cancellationToken);

        return new GetUserTotalCountResult(count);
    }
}
