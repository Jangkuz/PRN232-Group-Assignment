using BuildingBlocks.Messaging.Events;
using Ordering.Application.Orders.Commands.CreateOrder;
using MassTransit;

namespace Ordering.Application.Orders.EventHandlers.Intergration;
public class BasketCheckoutEventHandler (
    IApplicationDbContext dbContext,
    ISender sender, 
    ILogger<BasketCheckoutEventHandler> logger
    )
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        // TODO: Create new order and start order fullfillment process
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        // Create full order with incoming event data
        var orderId = Guid.NewGuid();

        var customer = dbContext.Customers.FirstOrDefault(c => c.Id == CustomerId.Of(message.CustomerId));
        string customerName = customer is null ? "Unknown" : customer.Name;
        List<OrderItemDto> orderItems = [];

        foreach (var item in message.Items) {
            orderItems.Add(
                new OrderItemDto(
                    orderId,
                    item.GameId,
                    item.Quantity,
                    item.Price
                    )
                );
        }

        var orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            CustomerName: customerName,
            OrderName: $"ORD_{customerName}_{DateTime.UtcNow.Ticks}",
            //Payment: paymentDto,
            Status: Ordering.Domain.Enums.OrderStatus.Pending,
            OrderItems: orderItems
            );

        return new CreateOrderCommand(orderDto);
    }
}
