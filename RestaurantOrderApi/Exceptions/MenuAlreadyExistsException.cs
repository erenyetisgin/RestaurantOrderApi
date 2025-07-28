namespace RestaurantOrderApi.Exceptions;

public class MenuAlreadyExistsException : Exception
{
    public MenuAlreadyExistsException(DateOnly date) : base($"Menu for date {date} is already present.")
    {
    }
}