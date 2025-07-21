using AirWaterStore.API.Helper;
using Microsoft.AspNetCore.Identity;

namespace AirWaterStore.API.Users.CreateUser;

public record CreateUserCommand(
    string UserName,
    string Email,
    string Password
    ) : ICommand<CreateUserResult>;

public record CreateUserResult(int Id);

//TODO: add validator

public class CreateUserHandler(
    UserManager<User> userManager
    ) : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = command.UserName,
            Email = command.Email,
            //Password = command.Password,
            IsBan = false
        };

        var result = await userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new BadRequestException(errors);
        }

        await userManager.AddToRoleAsync(user, AppConst.User);

        return new CreateUserResult(user.Id);
    }
}
