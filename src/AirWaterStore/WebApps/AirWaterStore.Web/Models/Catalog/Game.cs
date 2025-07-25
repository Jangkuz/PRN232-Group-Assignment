﻿using AirWaterStore.Web.Models.Ordering;

namespace AirWaterStore.Web.Models.Catalog;

public partial class Game
{
    public int Id { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public List<string> Genres { get; set; } = [];
    public string GenresString { get {
            var result = "N/A.";
            if (Genres != null && Genres.Any())
            {
                result = string.Join(", ", Genres.Take(4)) + ".";
            }
            return result;
        } 
    }
    public string? Developer { get; set; }

    public string? Publisher { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}

//wrapper classes
public record GetGamesResponse(IEnumerable<Game> Games);
public record GetGameByIdResponse(Game Game);
