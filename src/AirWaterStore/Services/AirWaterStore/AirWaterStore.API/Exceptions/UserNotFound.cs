namespace AirWaterStore.API.Exceptions;

public class UserNotFound : NotFoundException
{
    public UserNotFound(int Id) : base("User", Id)
    {
    }
}
