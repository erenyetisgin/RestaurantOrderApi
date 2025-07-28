namespace RestaurantOrderApi.Exceptions;

/// <summary>
/// Exception thrown when an order request is invalid.
/// </summary>
public class InvalidOrderException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidOrderException"/> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public InvalidOrderException(string? message) : base(message)
    {
    }
}