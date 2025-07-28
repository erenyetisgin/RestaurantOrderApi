using RestaurantOrderApi.Models;

namespace RestaurantOrderApi.Repositories;

/// <summary>
/// Provides data access operations for menus.
/// </summary>
public interface IMenuRepository
{
    /// <summary>
    /// Creates a new menu in the repository.
    /// </summary>
    /// <param name="menu">The menu to create.</param>
    void Create(Menu menu);
    
    /// <summary>
    /// Updates an existing menu in the repository.
    /// </summary>
    /// <param name="menu">The menu with updated information.</param>
    void Update(Menu menu);
    
    /// <summary>
    /// Deletes a menu for a specific date.
    /// </summary>
    /// <param name="date">The date of the menu to delete.</param>
    void Delete(DateOnly date);
    
    /// <summary>
    /// Checks if a menu exists for a specific date.
    /// </summary>
    /// <param name="date">The date to check.</param>
    /// <returns>true if the menu exists; otherwise, false.</returns>
    bool Exists(DateOnly date);
    
    /// <summary>
    /// Retrieves a menu for a specific date.
    /// </summary>
    /// <param name="date">The date for which to retrieve the menu.</param>
    /// <returns>The menu if found; otherwise, null.</returns>
    Menu? GetByDate(DateOnly date);

    /// <summary>
    /// Retrieves all menus from the repository.
    /// </summary>
    /// <returns>A list of all menus.</returns>
    List<Menu> GetAll();
}