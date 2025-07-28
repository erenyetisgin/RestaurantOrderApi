using RestaurantOrderApi.Models;

namespace RestaurantOrderApi.Services;

/// <summary>
/// Provides operations for managing orders in the restaurant system.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Creates a new order based on the provided order request.
    /// </summary>
    /// <param name="orderRequest">The order request containing order details.</param>
    /// <returns>The created order.</returns>
    Order CreateOrder(CreateOrderRequest orderRequest);
    
    /// <summary>
    /// Retrieves an order by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order.</param>
    /// <returns>The order if found; otherwise, null.</returns>
    Order? GetOrder(Guid id);
}