namespace AirWaterStore.Web.Models.Catalog;

public partial class Review
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public int GameId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? ReviewDate { get; set; }

}

public record GetReviewsByGameIdResponse(IEnumerable<Review> Reviews);

public record CreateReviewDto(
    int UserId,
    string UserName,
    int GameId,
    int Rating,
    string Comment,
    DateTime ReviewDate
    );

public record UpdateReviewDto(
    int Id,
    int Rating,
    string Comment
    );

public record PostReviewResponse(int Id);

public record PutReviewResponse(bool IsSuccess);
public record DeleteReviewResponse(bool IsSuccess);
