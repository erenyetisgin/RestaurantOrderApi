namespace RestaurantOrderApi.Models;

public class CreateOrderRequest
{
    public DateOnly Date { get; set; }

    public HashSet<string> ItemNames { get; set; } = new HashSet<string>();
}