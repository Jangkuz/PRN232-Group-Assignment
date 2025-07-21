using BuildingBlocks.Data;

namespace Ordering.Infrastructure.Data.Extentions;
internal class InitialData
{
    public static async Task<IEnumerable<Customer>> CustomersAsync()
    {
        var rawList = await ReadUserData.ReadMockDataAsync();

        return rawList
            .Select(ConvertToCustomer)
            .ToList();
    }
    public static async Task<IEnumerable<Game>> GamesAsync()
    {
        var rawList = await ReadGameData.ReadMockDataAsync();

        return rawList
            .Select(ConvertToGame)
            .ToList();
    }
    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {

            var order1 = Order.Create(
                            OrderId.Of(Guid.NewGuid()),
                            CustomerId.Of(100),
                            OrderName.Of("ORD_1"));
            order1.Add(GameId.Of(413150), 2, 500);
            order1.Add(GameId.Of(1245620), 2, 500);

            var order2 = Order.Create(
                            OrderId.Of(Guid.NewGuid()),
                            CustomerId.Of(101),
                            OrderName.Of("ORD_2"));
            order1.Add(GameId.Of(292030), 2, 500);
            order1.Add(GameId.Of(1091500), 2, 500);

            return new List<Order> {order1, order2 };
        }
    }

    private static Game ConvertToGame(GameJsonDto dto)
    {
        return Game.Create(
            id: GameId.Of(dto.AppId),
            name: dto.Title,
            price: ReadGameData.ParsePrice(dto.Price)
            );
    }
    private static Customer ConvertToCustomer(UserJsonDto dto)
    {
        return Customer.Create(
            id: CustomerId.Of(dto.UserId),
            name: dto.UserName,
            email: dto.Email
            );
    }
}