using System.Collections.Concurrent;
using RestaurantOrderApi.Models;

namespace RestaurantOrderApi.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    #region Variables

    private readonly ConcurrentDictionary<Guid, Order> _orders = new ConcurrentDictionary<Guid, Order>();

    #endregion
    
    #region Methods

    public Order? GetById(Guid id)
    {
        return this._orders.TryGetValue(id, out var order) ? order : null;
    }

    public void Create(Order order)
    {
        bool added = this._orders.TryAdd(order.Id, order);
        if (!added)
        {
            throw new Exception("An error occurred while attempting to create a new order.");
        }
    }

    public bool Exists(Guid id)
    {
        return this._orders.ContainsKey(id);
    }

    public List<Order> GetAll()
    {
        return this._orders.Values.ToList();
    }

    #endregion
}