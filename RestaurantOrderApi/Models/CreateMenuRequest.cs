namespace RestaurantOrderApi.Models;

/// <summary>
/// Represents a request to create a new menu.
/// </summary>
public class CreateMenuRequest
{
    /// <summary>
    /// Gets or sets the date for which the menu should be created.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the list of menu items to be included in the menu.
    /// </summary>
    public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}