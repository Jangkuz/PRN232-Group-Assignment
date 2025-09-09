namespace Ordering.Application.Orders.Commands.CreateOrder;
public class CreateOrderHandler(
    IApplicationDbContext dbContext
    )
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        //create Order entity from command object
        //save to database
        //return result 

        var order = CreateNewOrder(command.Order);

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }

    private Order CreateNewOrder(OrderDto orderDto)
    {

        var newOrder = Order.Create(
                id: OrderId.Of(Guid.NewGuid()),
                customerId: CustomerId.Of(orderDto.CustomerId),
                orderName: OrderName.Of(orderDto.OrderName)
                //payment: Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod)
                );

        foreach (var orderItemDto in orderDto.OrderItems)
        {
            var game = dbContext.Games.FirstOrDefault(g => g.Id == GameId.Of(orderItemDto.GameId));

            if (game == null)
            {
                throw new GameNotFoundException(orderItemDto.GameId);
            }

            if (game.Quantity < orderItemDto.Quantity)
            {
                throw new BadRequestException("Not enough game in stock");
            }

            newOrder.Add(
                GameId.Of(orderItemDto.GameId),
                orderItemDto.Quantity,
                orderItemDto.Price);
        }
        return newOrder;
    }
}
