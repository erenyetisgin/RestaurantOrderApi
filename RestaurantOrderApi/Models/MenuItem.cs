namespace RestaurantOrderApi.Models;

public class MenuItem
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }
    
    // TODO: Add allergens.
}