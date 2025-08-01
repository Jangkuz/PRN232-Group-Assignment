
namespace Ordering.Application.Exceptions;

public class GameNotFoundException : NotFoundException
{
    public GameNotFoundException(int Id) : base("Game", Id)
    {
    }
}
