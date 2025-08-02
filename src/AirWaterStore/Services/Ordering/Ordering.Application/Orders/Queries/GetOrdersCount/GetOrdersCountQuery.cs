namespace Ordering.Application.Orders.Queries.GetOrdersCount;

public record GetOrdersCountQuery : IQuery<GetOrdersCountResult>;

public record GetOrdersCountResult(
    int TotalOrder,
    int PendingOrder,
    int CompletedOrder
    );
