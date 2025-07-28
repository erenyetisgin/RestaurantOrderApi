using RestaurantOrderApi.Exceptions;
using RestaurantOrderApi.Models;
using RestaurantOrderApi.Repositories;

namespace RestaurantOrderApi.Services;

/// <summary>
/// Implements menu management operations for the restaurant system.
/// </summary>
public class MenuService : IMenuService
{
    #region Variables

    private readonly IMenuRepository _menuRepository;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuService"/> class.
    /// </summary>
    /// <param name="menuRepository">The repository for managing menus.</param>
    public MenuService(IMenuRepository menuRepository)
    {
        this._menuRepository = menuRepository;
    }

    #endregion

    #region Methods
    
    /// <inheritdoc/>
    public Menu? GetMenuByDate(DateOnly date)
    {
        return this._menuRepository.GetByDate(date);
    }

    /// <inheritdoc/>
    public Menu CreateMenu(CreateMenuRequest menuRequest)
    {
        if (this._menuRepository.Exists(menuRequest.Date))
        {
            throw new MenuAlreadyExistsException(menuRequest.Date);
        }

        Menu menu = new Menu()
        {
            Date = menuRequest.Date,
            Items = menuRequest.MenuItems
        };

        this._menuRepository.Create(menu);

        return menu;
    }

    /// <inheritdoc/>
    public void UpdateMenu(Menu menu)
    {
        if (!this._menuRepository.Exists(menu.Date))
        {
            throw new KeyNotFoundException("There is no menu to be updated for given date.");
        }

        if (menu.Items.Count == 0)
        {
            throw new InvalidMenuException("The menu should have at least one item.");
        }

        this._menuRepository.Update(menu);
    }

    /// <inheritdoc/>
    public void DeleteMenu(DateOnly date)
    {
        if (!this._menuRepository.Exists(date))
        {
            throw new KeyNotFoundException("There is no menu to delete for given date.");
        }

        this._menuRepository.Delete(date);
    }

    #endregion
}