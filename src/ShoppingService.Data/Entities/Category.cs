namespace ShoppingService.Data.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    
    // Relations
    public virtual Item Item{ get; set; }
}