using MassTransit;
using Microsoft.EntityFrameworkCore;
using ShoppingService.Entities;

namespace ShoppingService.Data;

public class ShoppingDbContext : DbContext
{
    public ShoppingDbContext(DbContextOptions options): base(options)
    {
        
    }
    
    // DbSets
    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Add Inbox and Outbox for Masstransit -> When the bus is down
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxMessageEntity();
    }
}