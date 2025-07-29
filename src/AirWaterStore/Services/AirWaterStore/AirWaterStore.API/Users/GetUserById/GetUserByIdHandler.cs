using AirWaterStore.API.Extentions;
using Microsoft.EntityFrameworkCore;

namespace AirWaterStore.API.Users.GetUserById;

public record GetUserByIdQuery(int Id) : IQuery<GetUserByIdResult>;

public record GetUserByIdResult(UserDto User);

public class GetUserByIdHandler(
    ApplicationDbContext dbContext,
    UserManager<User> userManager
    ) : IQueryHandler<GetUserByIdQuery, GetUserByIdResult>
{
    public async Task<GetUserByIdResult> Handle(GetUserByIdQuery command, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == command.Id);

        if (user == null)
        {
            throw new UserNotFound(command.Id);
        }

        var mapper = new UserMapper(userManager);

        return new GetUserByIdResult(mapper.ToUserDto(user));
    }
}
