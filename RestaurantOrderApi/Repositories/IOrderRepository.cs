using RestaurantOrderApi.Models;

namespace RestaurantOrderApi.Repositories;

/// <summary>
/// Provides data access operations for orders.
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Retrieves an order by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order.</param>
    /// <returns>The order if found; otherwise, null.</returns>
    Order? GetById(Guid id);
    
    /// <summary>
    /// Creates a new order in the repository.
    /// </summary>
    /// <param name="order">The order to create.</param>
    void Create(Order order);
    
    /// <summary>
    /// Checks if an order with the specified ID exists.
    /// </summary>
    /// <param name="id">The unique identifier of the order to check.</param>
    /// <returns>true if the order exists; otherwise, false.</returns>
    bool Exists(Guid id);
    
    /// <summary>
    /// Retrieves all orders from the repository.
    /// </summary>
    /// <returns>A list of all orders.</returns>
    List<Order> GetAll();
}