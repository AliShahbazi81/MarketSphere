using ShoppingService.Services.DTO;

namespace ShoppingService.Services.Models;

public record struct ItemModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
    public bool IsDeleted { get; set; }
}