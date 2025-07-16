using System.Text.Json.Serialization;

namespace Catalog.API.Data;

internal class ReadReviewData
{
    public static Review ConvertToReview(ReviewJsonDto dto)
    {
        return new Review
        {
            Id = dto.ReviewId,
            UserId = dto.UserId,
            GameId = dto.GameId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            ReviewDate = dto.ReviewDate
        };
    }
}

internal class ReviewJsonDto
{
    [JsonPropertyName("review_id")]
    public int ReviewId { get; set; }

    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("game_id")]
    public int GameId { get; set; }

    [JsonPropertyName("rating")]
    public int? Rating { get; set; }

    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    [JsonPropertyName("review_date")]
    public DateTime? ReviewDate { get; set; }
}

