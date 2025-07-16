namespace Catalog.API.Models;

public class Review
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int GameId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? ReviewDate { get; set; }

}
