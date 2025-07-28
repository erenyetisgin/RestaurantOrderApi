namespace RestaurantOrderApi.Models;

/// <summary>
/// Represents a customer order with its items and metadata.
/// </summary>
public class Order
{
    /// <summary>
    /// Gets or sets the unique identifier for the order.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the date for which the order was placed.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the list of items included in the order.
    /// </summary>
    public List<MenuItem> Items { get; set; } = new List<MenuItem>();

    /// <summary>
    /// Gets or sets the UTC timestamp when the order was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}