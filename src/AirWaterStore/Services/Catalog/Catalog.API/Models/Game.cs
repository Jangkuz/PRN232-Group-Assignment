namespace Catalog.API.Models;

public class Game
{
    public int Id { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public List<string> Genre { get; set; } = [];

    public string? Developer { get; set; }

    public string? Publisher { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
