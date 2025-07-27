using AirWaterStore.Web.Models.AirWaterStore;

namespace AirWaterStore.Web.Services;

public interface IAirWaterStoreService
{
    [Post("/airwater-service/users/login")]
    Task<LoginTokenResponse> PostLogin(LoginDto loginRequest);

    [Post("/airwater-service/users")]
    Task<RegisterResponse> PostRegister(RegisterDto registerDto);
}
