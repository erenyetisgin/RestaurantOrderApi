using Moq;
using RestaurantOrderApi.Exceptions;
using RestaurantOrderApi.Models;
using RestaurantOrderApi.Repositories;
using RestaurantOrderApi.Services;

namespace RestaurantOrderApi.Tests;

public class MenuServiceTests
{
    #region Variables

    private readonly Mock<IMenuRepository> _menuRepositoryMock;

    private readonly MenuService _menuService;

    #endregion

    #region Constructors

    public MenuServiceTests()
    {
        this._menuRepositoryMock = new Mock<IMenuRepository>();
        this._menuService = new MenuService(this._menuRepositoryMock.Object);
    }

    #endregion

    #region Tests

    [Fact]
    public void CreateMenu_ShouldThrow_WhenMenuAlreadyExists()
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Today);

        this._menuRepositoryMock.Setup(repository => repository.Exists(date)).Returns(true);

        Menu menu = new Menu()
        {
            Date = date,
            Items = new List<MenuItem>()
            {
                new MenuItem() { Name = "Mantı", Description = "Bol sarımsaklı", Price = 250 },
                new MenuItem() { Name = "Yaprak Sarması", Description = "Zeytinyağlı", Price = 150 }
            }
        };

        Assert.Throws<MenuAlreadyExistsException>(() => this._menuService.CreateMenu(menu));
    }

    // TODO: Write more tests.
    
    #endregion
}