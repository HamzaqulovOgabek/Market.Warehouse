using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Warehouse.DataAccess.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<CartItem> Carts { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Favourite> Favourites { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<CartItem> CartItems { get; set; }  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(p => p.Price)
            .HasColumnType("decimal(18, 2)"); //SQLServer Column type
        });
        modelBuilder.Entity<Discount>(e =>
        {
            e.Property(d => d.InterestRate)
            .HasPrecision(18, 2);
        });

        modelBuilder.Entity<ProductImage>(options =>
        {
            options.ToTable("ProductImage");
        });
    }
}
