﻿namespace AirWaterStore.Web.Models.Basket;

public class CartItem
{
    public int GameId { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
