using Microsoft.EntityFrameworkCore;
using VillageMaker.ProductService.Domain.Models;

namespace VillageMaker.ProductService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
        
    }

    public DbSet<Maker> Makers { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Maker>()
            .HasMany(m => m.Products)
            .WithOne(m => m.Maker!)
            .HasForeignKey(m => m.MakerId);

        modelBuilder
            .Entity<Product>()
            .HasOne(p => p.Maker)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.MakerId);
    }
}