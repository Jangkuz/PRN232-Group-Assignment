namespace AirWaterStore.API.Dtos;

public record UserDto(
    int Id,
    string Email,
    bool? IsBan,
    IList<string> Roles
);
