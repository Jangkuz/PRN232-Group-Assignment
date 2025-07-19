using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;
public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                productId => productId.Value,
                dbId => GameId.Of(dbId));

        builder.Property(p => p.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Price).IsRequired()
            .HasPrecision(18, 4);
    }
}
