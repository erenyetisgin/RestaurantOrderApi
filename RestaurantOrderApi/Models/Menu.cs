namespace RestaurantOrderApi.Models;

public class Menu
{
    public DateOnly Date { get; set; }

    public List<MenuItem> Items { get; set; }  = new List<MenuItem>();
}