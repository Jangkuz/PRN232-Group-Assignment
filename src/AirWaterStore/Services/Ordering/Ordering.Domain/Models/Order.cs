namespace Ordering.Domain.Models;
public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    //public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }

    public static Order Create(
        OrderId id, 
        CustomerId customerId, 
        OrderName orderName
        //Payment payment
        )
    {
        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            //Payment = payment,
            Status = OrderStatus.Pending
        };

        order.AddDomainEvent(new OrderUpdatedEvent(order));

        return order;
    }

    public void Update(
        OrderName orderName, 
        //Payment payment, 
        OrderStatus status
        )
    {
        OrderName = orderName;
        //Payment = payment;
        Status = status;

        AddDomainEvent(new OrderCreatedEvent(this));
    }

    public void Add(GameId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var orderItem = new OrderItem(Id, productId, quantity, price);
        _orderItems.Add(orderItem);
    }

    public void Remove(GameId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(x => x.GameId == productId);
        if (orderItem is not null)
        {
            _orderItems.Remove(orderItem);
        }
    }
}

