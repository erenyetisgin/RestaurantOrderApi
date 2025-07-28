using System.Collections.Concurrent;
using RestaurantOrderApi.Models;

namespace RestaurantOrderApi.Repositories;

/// <summary>
/// In-memory implementation of the menu repository using a concurrent dictionary for thread-safe operations.
/// </summary>
public class InMemoryMenuRepository : IMenuRepository
{
    #region Variables

    private readonly ConcurrentDictionary<DateOnly, Menu> _menus = new ConcurrentDictionary<DateOnly, Menu>();

    #endregion

    #region Methods
    
    /// <inheritdoc/>
    /// <exception cref="Exception">Thrown when the menu cannot be added to the repository.</exception>
    public void Create(Menu menu)
    {
        bool added = this._menus.TryAdd(menu.Date, menu);
        if (!added)
        {
            throw new Exception("An error occurred while attempting to create a new menu.");
        }
    }

    /// <inheritdoc/>
    public void Update(Menu menu)
    {
        this._menus[menu.Date] = menu;
    }

    /// <inheritdoc/>
    /// <exception cref="Exception">Thrown when the menu cannot be removed from the repository.</exception>
    public void Delete(DateOnly date)
    {
        bool removed = this._menus.TryRemove(date, out _);
        if (!removed)
        {
            throw new Exception("An error occurred while attempting to delete the menu.");
        }
    }

    /// <inheritdoc/>
    public bool Exists(DateOnly date)
    {
        return this._menus.ContainsKey(date);
    }

    /// <inheritdoc/>
    public Menu? GetByDate(DateOnly date)
    {
        this._menus.TryGetValue(date, out Menu? menu);
        return menu;
    }

    /// <inheritdoc/>
    public List<Menu> GetAll()
    {
        return this._menus.Values.ToList();
    }

    #endregion
}