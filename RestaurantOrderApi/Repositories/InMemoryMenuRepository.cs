using System.Collections.Concurrent;
using RestaurantOrderApi.Models;

namespace RestaurantOrderApi.Repositories;

public class InMemoryMenuRepository : IMenuRepository
{
    #region Variables

    private readonly ConcurrentDictionary<DateOnly, Menu> _menus = new ConcurrentDictionary<DateOnly, Menu>();

    #endregion

    #region Methods
    
    public void Create(Menu menu)
    {
        bool added = this._menus.TryAdd(menu.Date, menu);
        if (!added)
        {
            throw new Exception("An error occurred while attempting to create a new menu.");
        }
    }

    public void Update(Menu menu)
    {
        this._menus[menu.Date] = menu;
    }

    public void Delete(DateOnly date)
    {
        bool removed = this._menus.TryRemove(date, out _);
        if (!removed)
        {
            throw new Exception("An error occurred while attempting to delete the menu.");
        }
    }

    public bool Exists(DateOnly date)
    {
        return this._menus.ContainsKey(date);
    }

    public Menu? GetByDate(DateOnly date)
    {
        this._menus.TryGetValue(date, out Menu? menu);
        return menu;
    }

    public List<Menu> GetAll()
    {
        return this._menus.Values.ToList();
    }

    #endregion
}