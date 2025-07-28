namespace RestaurantOrderApi.Models;

/// <summary>
/// Represents an item on the restaurant menu.
/// </summary>
public class MenuItem
{
    /// <summary>
    /// Gets or sets the name of the menu item.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the menu item.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the price of the menu item.
    /// </summary>
    public decimal Price { get; set; }
}