using AirWaterStore.API.Extentions;
using Microsoft.EntityFrameworkCore;

namespace AirWaterStore.API.Users.GetUsers;

public record GetUsersQuery(
    int ExceptUserId,
    PaginationRequest PaginationRequest
    ) : IQuery<GetUsersResult>;

public record GetUsersResult(PaginatedResult<UserDto> Users);

internal class GetUsersHandler(
    ApplicationDbContext dbContext,
    UserManager<User> userManager
    ) : IQueryHandler<GetUsersQuery, GetUsersResult>
{
    public async Task<GetUsersResult> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Users.CountAsync();

        var users = await dbContext.Users
            .Where(u => u.Id != query.ExceptUserId)
            .OrderBy(u => u.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var mapper = new UserMapper(userManager);

        return new GetUsersResult(
            new PaginatedResult<UserDto>(
                pageIndex,
                pageSize,
                totalCount,
                mapper.ToUserDtosList(users)
                )
            );
    }
}
