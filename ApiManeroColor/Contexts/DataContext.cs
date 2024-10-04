using ApiManeroColor.Entites;
using Microsoft.EntityFrameworkCore;

namespace ApiManeroColor.Contexts;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ColorEntity> Color { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ColorEntity>()
            .ToContainer("Colors")
            .HasPartitionKey(x => x.PartitionKey);
    }
}
