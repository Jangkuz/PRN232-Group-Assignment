﻿using AirWaterStore.Web.Models.Catalog;

namespace AirWaterStore.Web.Models.Ordering;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int GameId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
