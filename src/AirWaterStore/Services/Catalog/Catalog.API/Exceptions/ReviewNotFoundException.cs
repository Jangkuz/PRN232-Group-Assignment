using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions;

public class ReviewNotFoundException : NotFoundException
{
    public ReviewNotFoundException(int Id) : base("Review", Id)
    {
    }
}
