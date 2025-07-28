using RestaurantOrderApi.Models;

namespace RestaurantOrderApi.Repositories;

public interface IOrderRepository
{
    Order? GetById(Guid id);
    
    void Create(Order order);
    
    bool Exists(Guid id);
    
    List<Order> GetAll();
}