namespace RestaurantOrderApi.Models;

public class Order
{
    public Guid Id { get; set; }

    public DateOnly Date { get; set; }

    public List<MenuItem> Items { get; set; }  = new List<MenuItem>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // TODO: Add customer id to keep track of the orders by the customer.
}