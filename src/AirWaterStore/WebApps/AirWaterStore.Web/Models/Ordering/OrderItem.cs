namespace AirWaterStore.Web.Models.Ordering;

public partial class OrderItem
{
    //public int OrderDetailId { get; set; }

    public string OrderId { get; set; } = string.Empty;

    public int GameId { get; set; }

    public string GameTitle { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    //public virtual Game Game { get; set; } = null!;

    //public virtual Order Order { get; set; } = null!;
}

public record OrderItemDto(
    string OrderId,
    int GameId,
    string GameTitle,
    int Quantity,
    decimal Price
    );
