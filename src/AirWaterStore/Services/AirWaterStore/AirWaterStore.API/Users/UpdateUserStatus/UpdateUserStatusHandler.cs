
namespace AirWaterStore.API.Users.UpdateUserStatus;

public record UpdateUserStatusCommand(
    int Id,
    bool IsBan,
    string Role
    ) : ICommand<UpdateUserStatusResult>;
public record UpdateUserStatusResult(bool IsSuccess);

//TODO: Add validator

internal class UpdateUserStatusHandler(
    UserManager<User> userManager
    ) : ICommandHandler<UpdateUserStatusCommand, UpdateUserStatusResult>
{
    public async Task<UpdateUserStatusResult> Handle(UpdateUserStatusCommand command, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(command.Id.ToString());

        if (user == null)
        {
            throw new UserNotFound(command.Id);
        }

        user.IsBan = command.IsBan;

        await userManager.UpdateAsync(user);

        // Get current roles
        var currentRoles = await userManager.GetRolesAsync(user);

        // Remove from all current roles
        var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeResult.Succeeded)
            throw new BadRequestException("Failed to remove existing roles");

        // Add new role
        var addResult = await userManager.AddToRoleAsync(user, command.Role);
        if (!addResult.Succeeded)
            throw new BadRequestException("Failed to assign new role");

        return new UpdateUserStatusResult(true);
    }
}
