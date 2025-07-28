using RestaurantOrderApi.Exceptions;
using RestaurantOrderApi.Models;
using RestaurantOrderApi.Repositories;

namespace RestaurantOrderApi.Services;

public class MenuService : IMenuService
{
    #region Variables

    private readonly IMenuRepository _menuRepository;

    #endregion

    #region Constructor

    public MenuService(IMenuRepository menuRepository)
    {
        this._menuRepository  = menuRepository;
    }

    #endregion

    #region Methods
    
    public Menu? GetMenuByDate(DateOnly date)
    {
        return this._menuRepository.GetByDate(date);
    }

    public void CreateMenu(Menu menu)
    {
        if (this._menuRepository.Exists(menu.Date))
        {
            throw new MenuAlreadyExistsException(menu.Date);
        }

        this._menuRepository.Create(menu);
    }

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