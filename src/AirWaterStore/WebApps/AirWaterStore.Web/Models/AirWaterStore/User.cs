using AirWaterStore.Web.Models.Catalog;
using AirWaterStore.Web.Models.Ordering;

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

public record RegisterDto(
    string Username,
    string Email,
    string Password
    );

public record RegisterResponse(int Id);
