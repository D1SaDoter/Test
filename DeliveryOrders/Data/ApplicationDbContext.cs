using DeliveryOrders.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOrders.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(order => order.OrderNumber).IsUnique();
            entity.Property(order => order.CargoWeightKg).HasPrecision(10, 2);
        });
    }
}
