using Mapster;
using MassTransit;
using Microsoft.FeatureManagement;
using BuildingBlocks.Messaging.Events;
using System.Runtime.CompilerServices;
using OrderItem = BuildingBlocks.Messaging.Events.OrderItem;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler
    (
    IPublishEndpoint publishEndpoint,
    IFeatureManager featureManager,
    ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedDomainEvent>
{
    public async Task Handle(OrderCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {

        if (await featureManager.IsEnabledAsync("OrderFullfilment"))
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);
            OrderDto orderDto = domainEvent.order.ToOrderDto();
            var orderCreatedIntegrationEvent = new OrderCreatedEvent
            {
                CustomerId = orderDto.CustomerId,
                OrderName = orderDto.OrderName,
                OrderItems = orderDto.OrderItems.Select(oi => ToOrderItem(oi)).ToList()
            };
            await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }
    }

    private OrderItem ToOrderItem(OrderItemDto dto)
    {
        return new OrderItem
        (
            OrderId: dto.OrderId,
            GameId: dto.GameId,
            Quantity: dto.Quantity,
            Price: dto.Price
        );
    }
}
