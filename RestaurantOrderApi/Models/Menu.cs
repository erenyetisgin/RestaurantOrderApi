namespace RestaurantOrderApi.Models;

/// <summary>
/// Represents a menu for a specific date with its items.
/// </summary>
public class Menu
{
    /// <summary>
    /// Gets or sets the date for which this menu is valid.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the list of menu items available on this menu.
    /// </summary>
    public List<MenuItem> Items { get; set; } = new List<MenuItem>();
}