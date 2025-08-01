using Discount.gRPC.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Discount.gRPC.Data;

public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    public DiscountContext(DbContextOptions<DiscountContext> options)
       : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, GameId = 413150, Description = "Stardew Valley discount", Amount = 10 },
            new Coupon { Id = 2, GameId = 1245620, Description = "ELDEN RING discount", Amount = 15 }
            );
    }
}
