using AirWaterStore.Web.Models.AirWaterStore;

namespace AirWaterStore.Web.Models.Catalog;

public partial class Review
{
    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public int GameId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? ReviewDate { get; set; }

}

public record GetReviewsByGameIdResponse(IEnumerable<Review> Review);
