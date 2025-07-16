
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Catalog.API.Data;

internal static class ReadGameData
{
    public static Game ConvertToGame(GameJsonDto dto)
    {
        //IMPORTANT: this exist because the current mock data file is a string<Python List> so it needed to be process.
        List<string> genreList = ParseTags(dto.Tags);

        return new Game
        {
            Id = dto.AppId,
            Title = dto.Title,
            ThumbnailUrl = dto.ThumbnailUrl,
            Description = dto.Description,
            Genre = genreList ?? new List<string>(),
            Developer = dto.Developer,
            Publisher = dto.Publisher,
            ReleaseDate = ParseDateOnly(dto.ReleaseDate),
            Price = ParsePrice(dto.Price),
            Quantity = Random.Shared.Next(5, 50) // mock quantity
        };
    }

    private static List<string> ParseTags(string tagString)
    {
        if (string.IsNullOrWhiteSpace(tagString)) return new();

        return tagString
            .Trim('[', ']') // remove brackets
            .Split(',')     // split by comma
            .Select(tag => tag.Trim().Trim('\'')) // trim whitespace + single quotes
            .Where(tag => !string.IsNullOrWhiteSpace(tag))
            .ToList();
    }

    private static DateOnly? ParseDateOnly(string? dateStr)
    {
        if (string.IsNullOrWhiteSpace(dateStr)) return null;

        if (DateTime.TryParseExact(dateStr, "dd MMM, yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
            return DateOnly.FromDateTime(dt);

        return null;
    }

    private static decimal ParsePrice(string price)
    {
        var cleaned = new string(price.Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());
        cleaned = cleaned.Replace(",", "."); // ensure dot-decimal format

        return decimal.TryParse(cleaned, NumberStyles.Any, CultureInfo.InvariantCulture, out var val) ? val : 0;
    }
}
internal class GameJsonDto
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
