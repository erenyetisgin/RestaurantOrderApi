namespace RestaurantOrderApi.Exceptions;

/// <summary>
/// Exception thrown when a requested menu cannot be found.
/// </summary>
public class MenuNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MenuNotFoundException"/> class.
    /// </summary>
    /// <param name="date">The date for which the menu was not found.</param>
    public MenuNotFoundException(DateOnly date) : base($"Menu for date {date} not found.")
    {
    }
}