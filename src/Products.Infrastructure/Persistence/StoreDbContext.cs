using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;

namespace Products.Infrastructure.Persistence;

public class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships and other entity properties
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId);

        // Example of configuring other properties
        modelBuilder.Entity<Product>(b =>
        {
            b.Property(p => p.Name).IsRequired().HasMaxLength(255);
            b.Property(p => p.Price).HasColumnType("decimal(18, 2)");
            b.HasIndex(p => p.CategoryId);
        });

        modelBuilder.Entity<Category>(b =>
        {
            b.Property(c => c.Name).IsRequired().HasMaxLength(255);
        });
    }
}
