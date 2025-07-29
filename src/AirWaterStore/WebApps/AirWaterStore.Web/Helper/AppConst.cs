namespace AirWaterStore.Web.Helper;

public static class AppConst
{
    public const string Admin = "Admin";
    public const string Staff = "Staff";
    public const string User = "User";

    public static readonly string UserIdClaim = "UserId";
    public static readonly string RoleClaim = "Role";
    public static readonly string UserNameClaim = "UserName";
    public static readonly string IsBanClaim = "IsBan";

    public static readonly string Cookie = "access_token";
}
