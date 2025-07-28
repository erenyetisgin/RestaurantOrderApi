namespace RestaurantOrderApi.Exceptions;

/// <summary>
/// Exception thrown when a menu is invalid, such as when it contains no items.
/// </summary>
public class InvalidMenuException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidMenuException"/> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public InvalidMenuException(string? message) : base(message)
    {
    }
}