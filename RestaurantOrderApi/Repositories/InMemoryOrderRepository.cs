using System.Collections.Concurrent;
using RestaurantOrderApi.Models;

namespace RestaurantOrderApi.Repositories;

/// <summary>
/// In-memory implementation of the order repository using a concurrent dictionary for thread-safe operations.
/// </summary>
public class InMemoryOrderRepository : IOrderRepository
{
    #region Variables

    private readonly ConcurrentDictionary<Guid, Order> _orders = new ConcurrentDictionary<Guid, Order>();

    #endregion
    
    #region Methods

    /// <inheritdoc/>
    public Order? GetById(Guid id)
    {
        return this._orders.TryGetValue(id, out var order) ? order : null;
    }

    /// <inheritdoc/>
    /// <exception cref="Exception">Thrown when the order cannot be added to the repository.</exception>
    public void Create(Order order)
    {
        bool added = this._orders.TryAdd(order.Id, order);
        if (!added)
        {
            throw new Exception("An error occurred while attempting to create a new order.");
        }
    }

    /// <inheritdoc/>
    public bool Exists(Guid id)
    {
        return this._orders.ContainsKey(id);
    }

    /// <inheritdoc/>
    public List<Order> GetAll()
    {
        return this._orders.Values.ToList();
    }

    #endregion
}