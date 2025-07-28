namespace RestaurantOrderApi.Exceptions;

public class InvalidMenuException : Exception
{
    public InvalidMenuException(string? message) : base(message)
    {
    }
}