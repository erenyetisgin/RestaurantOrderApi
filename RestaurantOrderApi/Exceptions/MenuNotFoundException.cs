namespace RestaurantOrderApi.Exceptions;

public class MenuNotFoundException : Exception
{
    public MenuNotFoundException(DateOnly date) : base($"Menu for date {date} not found.")
    {
    }
}