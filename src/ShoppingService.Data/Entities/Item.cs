using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShoppingService.Data.Entities;

public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    
    // Relations
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
}

public class ItemBuilder : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Category)
            .WithOne(x => x.Item)
            .HasForeignKey<Item>(x => x.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}