
namespace AirWaterStore.Web.Models.AirWaterStore;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public bool? IsBan { get; set; }

}

public record LoginDto
(
    string Email,
    string Password
);

public record LoginTokenResponse(string Token);

public record GetUserCountResponse(int UserCount);

public record RegisterDto(
    string Username,
    string Email,
    string Password
    );

public record RegisterResponse(int Id);


public record AdminUserApiResponse(
    PagedUserResponse Users
    );

public record PagedUserResponse(
    int PageIndex,
    int PageSize,
    int Count,
    List<UserDto> Data
    );

public record UserDto(
    int Id,
    string UserName,
    string Email,
    bool IsBan,
    List<string> Roles
    );

public record GetUserByIdResponse(
    UserDto User
    );

public record UpdateUserStatusResponse(bool IsSuccess);

public record UpdateUserStatusDto(
    int Id,
    bool IsBan,
    string Role
    );