using RestaurantOrderApi.Models;

namespace RestaurantOrderApi.Services;

public interface IMenuService
{
    Menu? GetMenuByDate(DateOnly date);
    
    void CreateMenu(Menu menu);
    
    void UpdateMenu(Menu menu);
    
    void DeleteMenu(DateOnly date);
}