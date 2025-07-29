namespace AirWaterStore.Web.Helper;

public static class AppRole
{
    public const int Customer = 1;
    public const int Staff = 2;
    public const int Admin = 3;

    //public static string GetRoleName(int roleId)
    //{
    //    return roleId switch
    //    {
    //        Customer => AppConst.User,
    //        Staff => AppConst.Staff,
    //        Admin => AppConst.Admin,
    //        _ => AppConst.User
    //    };
    //}

    public static int GetRoleValue(string role)
    {
        return role switch
        {
            AppConst.User => Customer,
            AppConst.Staff => Staff,
            AppConst.Admin => Admin,
            _ => Customer
        };
    }
}
