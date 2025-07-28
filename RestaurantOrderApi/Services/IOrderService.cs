using RestaurantOrderApi.Models;

namespace RestaurantOrderApi.Services;

public interface IOrderService
{
    Order CreateOrder(CreateOrderRequest orderRequest);
    
    Order? GetOrder(Guid id);
}