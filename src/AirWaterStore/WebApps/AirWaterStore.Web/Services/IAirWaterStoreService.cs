using AirWaterStore.Web.Models.AirWaterStore;

namespace AirWaterStore.Web.Services;

public interface IAirWaterStoreService
{
    [Post("/airwater-service/users/login")]
    Task<LoginTokenResponse> PostLogin(LoginDto loginRequest);

    [Post("/airwater-service/users")]
    Task<RegisterResponse> PostRegister(RegisterDto registerDto);

    [Get("/airwater-service/users/count")]
    Task<GetUserCountResponse> GetUserCount();

    [Get("/airwater-service/users/{Id}")]
    Task<GetUserByIdResponse> GetUserById(int Id);

    [Get("/airwater-service/users?ExceptUserId={exceptUserId}&PageIndex={pageIndex}&PageSize={pageSize}")]
    Task<AdminUserApiResponse> GetUsersPaging(int exceptUserId, int pageIndex, int pageSize);

    [Put("/airwater-service/users")]
    Task<UpdateUserStatusResponse> PutUserStatus(UpdateUserStatusDto updateUserDto);
}
