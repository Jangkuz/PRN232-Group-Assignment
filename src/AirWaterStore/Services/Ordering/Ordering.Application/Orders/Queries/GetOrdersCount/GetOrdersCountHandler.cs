namespace Ordering.Application.Orders.Queries.GetOrdersCount;

public class GetOrdersCountHandler (IApplicationDbContext dbContext) : IQueryHandler<GetOrdersCountQuery, GetOrdersCountResult>
{

    public async Task<GetOrdersCountResult> Handle(GetOrdersCountQuery query, CancellationToken cancellationToken)
    {
        var totalOrder = await dbContext.Orders.CountAsync(cancellationToken);
        var pendingOrder = await dbContext.Orders.CountAsync(o => o.Status == Domain.Enums.OrderStatus.Pending, cancellationToken);
        var completedOrder = await dbContext.Orders.CountAsync(o => o.Status == Domain.Enums.OrderStatus.Completed, cancellationToken);
        return new GetOrdersCountResult(
            TotalOrder: totalOrder,
            PendingOrder: pendingOrder,
            CompletedOrder: completedOrder
        );
    }
}
