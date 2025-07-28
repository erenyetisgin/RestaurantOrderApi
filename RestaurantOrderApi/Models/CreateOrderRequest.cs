namespace RestaurantOrderApi.Models;

/// <summary>
/// Represents a request to create a new order.
/// </summary>
public class CreateOrderRequest
{
    /// <summary>
    /// Gets or sets the date for which the order is being placed.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the unique names of items to be included in the order.
    /// </summary>
    public HashSet<string> ItemNames { get; set; } = new HashSet<string>();
}