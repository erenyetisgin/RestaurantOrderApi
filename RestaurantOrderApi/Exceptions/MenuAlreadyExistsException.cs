namespace RestaurantOrderApi.Exceptions;

/// <summary>
/// Exception thrown when attempting to create a menu for a date that already has a menu.
/// </summary>
public class MenuAlreadyExistsException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MenuAlreadyExistsException"/> class.
    /// </summary>
    /// <param name="date">The date for which a menu already exists.</param>
    public MenuAlreadyExistsException(DateOnly date) : base($"Menu for date {date} is already present.")
    {
    }
}