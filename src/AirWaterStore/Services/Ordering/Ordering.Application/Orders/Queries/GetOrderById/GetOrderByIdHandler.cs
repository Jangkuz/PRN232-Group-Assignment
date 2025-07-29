namespace Ordering.Application.Orders.Queries.GetOrderById;
public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrderByIdQuery, GetOrdersByIdResult>
{
    public async Task<GetOrdersByIdResult> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        // get orders by name using dbContext
        // return result

        var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Customer)
                .AsNoTracking()
                .Where(o => o.Id.Value.Equals(query.OrderId))
                .OrderBy(o => o.Id.Value)
                .ToListAsync(cancellationToken);

        return new GetOrdersByIdResult(orders.ToOrderDtoList());
    }
}
