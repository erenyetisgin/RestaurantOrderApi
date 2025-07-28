namespace RestaurantOrderApi.Models;

public class CreateMenuRequest
{
    public DateOnly Date { get; set; }

    public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}