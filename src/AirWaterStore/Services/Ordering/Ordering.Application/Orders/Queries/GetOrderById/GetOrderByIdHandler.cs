namespace Ordering.Application.Orders.Queries.GetOrderById;
public class GetOrdersByNameHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOrderByIdQuery, GetOrdersByIdResult>
{
    public async Task<GetOrdersByIdResult> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        // get orders by name using dbContext
        // return result
        Guid.TryParse(query.OrderId, out var orderId);

        var order = await dbContext.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Game)
                .Include(o => o.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == OrderId.Of(orderId));

        if (order is null)
        {
            throw new OrderNotFoundException(orderId);
        }

        return new GetOrdersByIdResult(order.ToOrderDto());
    }
}
