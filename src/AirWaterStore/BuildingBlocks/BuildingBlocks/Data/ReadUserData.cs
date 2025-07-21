using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Data;
public static class ReadUserData
{
    private static List<UserJsonDto> userJsons = default!;
    public static async Task<List<UserJsonDto>> ReadMockDataAsync()
    {
        if (userJsons != null)
        {
            return userJsons;
        }
        List<UserJsonDto>? rawList = null;
        try
        {
            var dir = Path.GetDirectoryName(typeof(ReadUserData).Assembly.Location)!;
            var path = Path.Combine(dir, "Data", "steam_users.json");
            var json = await File.ReadAllTextAsync(path);

            rawList = JsonSerializer.Deserialize<List<UserJsonDto>>(json);

        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.Message);
        }

        userJsons = rawList?.ToList() ?? new();
        return userJsons;
    }
}

public class UserJsonDto
{
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }

    [JsonPropertyName("username")]
    public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("role")]
    public int Role { get; set; }

    [JsonPropertyName("is_ban")]
    public int IsBan { get; set; }
}
