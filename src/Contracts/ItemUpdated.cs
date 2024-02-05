namespace Contracts;

public class ItemUpdated
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public DateTime UpdatedAt { get; set; }
}