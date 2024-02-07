namespace ShoppingService.Services.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("We could not find what you were looking for!")
    {
        
    }
}