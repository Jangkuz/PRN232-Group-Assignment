using Basket.API.Dtos;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(int UserId)
    : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator
    : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
    }
}

public class CheckoutBasketCommandHandler
    (IBasketRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        // get existing basket with total price
        // Set totalprice on basketcheckout event message
        // send basket checkout event to rabbitmq using masstransit
        // delete the basket

        var basket = await repository.GetBasket(command.UserId, cancellationToken);
        if (basket == null)
        {
            return new CheckoutBasketResult(false);
        }

        var eventMessage = basket.ToCheckoutEvent();
        //eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteBasket(command.UserId, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}

internal static class ShoppingCartExtention
{
    public static BasketCheckoutEvent ToCheckoutEvent(this ShoppingCart cart)
    {
        List<BasketItem> basketItems = [];

        foreach (var item in cart.Items)
        {
            basketItems.Add(
                new BasketItem(
                    GameId: item.GameId,
                    Quantity: item.Quantity,
                    Price: item.Price
                    )
                );
        }

        return new BasketCheckoutEvent
        {
            CustomerId = cart.UserId,
            TotalPrice = cart.TotalPrice,
            Items = basketItems
        };
    }
}
