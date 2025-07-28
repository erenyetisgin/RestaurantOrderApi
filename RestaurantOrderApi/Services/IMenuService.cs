using RestaurantOrderApi.Models;

namespace RestaurantOrderApi.Services;

/// <summary>
/// Provides operations for managing menus in the restaurant system.
/// </summary>
public interface IMenuService
{
    /// <summary>
    /// Retrieves a menu for a specific date.
    /// </summary>
    /// <param name="date">The date for which to retrieve the menu.</param>
    /// <returns>The menu if found; otherwise, null.</returns>
    Menu? GetMenuByDate(DateOnly date);
    
    /// <summary>
    /// Creates a new menu based on the provided request.
    /// </summary>
    /// <param name="menuRequest">The request containing menu details.</param>
    /// <returns>The created menu.</returns>
    /// <exception cref="MenuAlreadyExistsException">Thrown when a menu already exists for the specified date.</exception>
    Menu CreateMenu(CreateMenuRequest menuRequest);
    
    /// <summary>
    /// Updates an existing menu.
    /// </summary>
    /// <param name="menu">The menu with updated information.</param>
    /// <exception cref="KeyNotFoundException">Thrown when the menu to update does not exist.</exception>
    /// <exception cref="InvalidMenuException">Thrown when the updated menu is invalid.</exception>
    void UpdateMenu(Menu menu);
    
    /// <summary>
    /// Deletes a menu for a specific date.
    /// </summary>
    /// <param name="date">The date of the menu to delete.</param>
    /// <exception cref="KeyNotFoundException">Thrown when the menu to delete does not exist.</exception>
    void DeleteMenu(DateOnly date);
}