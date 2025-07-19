using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Data;

public static class ReadReviewData
{
    private static List<ReviewJsonDto> reviewJsons = default!;
    public static async Task<List<ReviewJsonDto>> ReadMockDataAsync()
    {
        if (reviewJsons != null)
        {
            return reviewJsons;
        }

        List<ReviewJsonDto>? rawList = null;

        try
        {
            var dir = Path.GetDirectoryName(typeof(ReadReviewData).Assembly.Location)!;
            var path = Path.Combine(dir, "Data", "steam_reviews.json");
            var json = await File.ReadAllTextAsync(path);

            rawList = JsonSerializer.Deserialize<List<ReviewJsonDto>>(json);
        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message);
        }

        reviewJsons = rawList?.ToList() ?? new();
        return reviewJsons;
    }
}

public class ReviewJsonDto
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

