using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Data;

public static class ReadGameData
{
    private static List<GameJsonDto> gameJsons = default!;
    public static async Task<List<GameJsonDto>> ReadMockDataAsync()
    {

        if (gameJsons != null)
        {
            return gameJsons;
        }

        List<GameJsonDto>? rawList = null;

        try
        {
            var dir = Path.GetDirectoryName(typeof(ReadGameData).Assembly.Location)!;
            var path = Path.Combine(dir, "Data", "steam_games.json");
            var json = await File.ReadAllTextAsync(path);

            rawList = JsonSerializer.Deserialize<List<GameJsonDto>>(json);

        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message);
        }

        gameJsons = rawList?.ToList() ?? new();
        return gameJsons;
    }

    public static List<string> ParseTags(string tagString)
    {
        if (string.IsNullOrWhiteSpace(tagString)) return new();

        return tagString
            .Trim('[', ']') // remove brackets
            .Split(',')     // split by comma
            .Select(tag => tag.Trim().Trim('\'')) // trim whitespace + single quotes
            .Where(tag => !string.IsNullOrWhiteSpace(tag))
            .ToList();
    }

    public static DateOnly? ParseDateOnly(string? dateStr)
    {
        if (string.IsNullOrWhiteSpace(dateStr)) return null;

        if (DateTime.TryParseExact(dateStr, "dd MMM, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
            return DateOnly.FromDateTime(dt);

        return null;
    }

    public static decimal ParsePrice(string price)
    {
        var cleaned = new string(price.Where(c => char.IsDigit(c)).ToArray());
        //cleaned = cleaned.Replace(",", "."); // ensure dot-decimal format

        return decimal.TryParse(cleaned, NumberStyles.Any, CultureInfo.InvariantCulture, out var val) ? val : 0;
    }
}
public class GameJsonDto
{
    [JsonPropertyName("app_id")]
    public int AppId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("thumbnail_url")]
    public string? ThumbnailUrl { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("release_date")]
    public string? ReleaseDate { get; set; }

    [JsonPropertyName("developer")]
    public string? Developer { get; set; }

    [JsonPropertyName("publisher")]
    public string? Publisher { get; set; }

    [JsonPropertyName("tags")]
    public string Tags { get; set; } = "[]";

    [JsonPropertyName("price")]
    public string Price { get; set; } = "0";

    [JsonPropertyName("user_review")]
    public string? UserReview { get; set; }

    [JsonPropertyName("url")]
    public int Url { get; set; }
}
