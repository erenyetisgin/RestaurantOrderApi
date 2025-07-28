using RestaurantOrderApi.Models;

namespace RestaurantOrderApi.Repositories;

public interface IMenuRepository
{
    void Create(Menu menu);
    
    void Update(Menu menu);
    
    void Delete(DateOnly date);
    
    bool Exists(DateOnly date);
    
    Menu? GetByDate(DateOnly date);

    List<Menu> GetAll();
}