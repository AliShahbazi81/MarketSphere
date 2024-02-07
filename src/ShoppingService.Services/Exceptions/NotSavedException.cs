namespace ShoppingService.Services.Exceptions;

public class NotSavedException : Exception
{
    public NotSavedException() : base("We faced an issue while creating what you were creating!")
    {
        
    }
}