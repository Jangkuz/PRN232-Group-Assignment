
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Ordering.Application.Orders.EventHandlers.Intergration;
public class UserCreatedEventHandler (
    IApplicationDbContext dbContext,
    ILogger<UserCreatedEventHandler> logger
    ) : IConsumer<UserCreatedEvent>
{
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

        await AddCustomer(context.Message, context.CancellationToken);
    }

    private async Task AddCustomer(UserCreatedEvent message, CancellationToken cancellationToken)
    {
        var user = Customer.Create(CustomerId.Of(message.UserId), message.UserName, message.Email);
        dbContext.Customers.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
