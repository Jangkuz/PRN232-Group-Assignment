using BuildingBlocks.Messaging.Events;
using Ordering.Application.Orders.Commands.CreateOrder;
using MassTransit;

namespace Ordering.Application.Orders.EventHandlers.Intergration;
public class BasketCheckoutEventHandler
    (ISender sender, ILogger<BasketCheckoutEventHandler> logger)
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
        //TODO: get order item from message
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            CustomerName: "",
            OrderName: message.UserName,
            //Payment: paymentDto,
            Status: Ordering.Domain.Enums.OrderStatus.Pending,
            OrderItems:
            [
                new OrderItemDto(orderId,292030 , 2, 500),
                new OrderItemDto(orderId, 1091500, 1, 400)
            ]);

        return new CreateOrderCommand(orderDto);
    }
}
