namespace Discount.gRPC.Models;

public class Coupon
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public string Description { get; set; } = default!;
    public int Amount { get; set; }
}