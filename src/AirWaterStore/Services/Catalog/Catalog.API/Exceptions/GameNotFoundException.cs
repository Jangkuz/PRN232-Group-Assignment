using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions;

public class GameNotFoundException : NotFoundException
{
    public GameNotFoundException(int Id) : base("Game", Id)
    {
    }
}
