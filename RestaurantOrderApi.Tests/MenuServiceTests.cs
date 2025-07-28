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
    public void CreateMenu_ShouldSucceed_WhenValidRequest()
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Today);

        this._menuRepositoryMock.Setup(repository => repository.Exists(date)).Returns(false);

        CreateMenuRequest menuRequest = new CreateMenuRequest()
        {
            Date = date,
            MenuItems = new List<MenuItem>()
            {
                new MenuItem() { Name = "Hot Pot", Description = "Çin usulü, acılı bir lezzet.", Price = 600 },
                new MenuItem() { Name = "Noodles", Description = "Makarna gibi bir şey.", Price = 300 }
            }
        };

        Menu result = this._menuService.CreateMenu(menuRequest);

        Assert.NotNull(result);
        Assert.Equal(date, result.Date);
        Assert.Equal(2, result.Items.Count);
        Assert.Contains(result.Items, item => item.Name == "Hot Pot");
        Assert.Contains(result.Items, item => item.Name == "Noodles");
        this._menuRepositoryMock.Verify(r => r.Create(It.Is<Menu>(m => m.Date == date && m.Items.Count == 2)),
            Times.Once);
    }

    [Fact]
    public void CreateMenu_ShouldThrow_WhenMenuAlreadyExists()
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Today);

        this._menuRepositoryMock.Setup(repository => repository.Exists(date)).Returns(true);

        CreateMenuRequest menuRequest = new CreateMenuRequest()
        {
            Date = date,
            MenuItems = new List<MenuItem>()
            {
                new MenuItem() { Name = "Mantı", Description = "Bol sarımsaklı", Price = 250 },
                new MenuItem() { Name = "Yaprak Sarması", Description = "Zeytinyağlı", Price = 150 }
            }
        };

        Assert.Throws<MenuAlreadyExistsException>(() => this._menuService.CreateMenu(menuRequest));
    }

    [Fact]
    public void UpdateMenu_ShouldSucceed_WhenValidRequest()
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Today);

        this._menuRepositoryMock.Setup(repository => repository.Exists(date)).Returns(true);

        Menu menu = new Menu()
        {
            Date = date,
            Items = new List<MenuItem>()
            {
                new MenuItem() { Name = "Hot Pot", Description = "Çin usulü, acılı bir lezzet.", Price = 600 },
                new MenuItem() { Name = "Noodles", Description = "Makarna gibi bir şey.", Price = 300 }
            }
        };

        this._menuService.UpdateMenu(menu);

        this._menuRepositoryMock.Verify(r => r.Update(It.Is<Menu>(m => m.Date == date && m.Items.Count == 2)),
            Times.Once);
    }

    [Fact]
    public void UpdateMenu_ShouldThrow_WhenMenuDoesNotExists()
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Today);

        this._menuRepositoryMock.Setup(repository => repository.Exists(date)).Returns(false);

        Menu menu = new Menu()
        {
            Date = date,
            Items = new List<MenuItem>()
            {
                new MenuItem() { Name = "Cağ Kebabı", Description = "Kıymadan değil!", Price = 520 },
                new MenuItem() { Name = "Gobit", Description = "Sanayi usulü", Price = 130 }
            }
        };

        Assert.Throws<KeyNotFoundException>(() => this._menuService.UpdateMenu(menu));
    }

    [Fact]
    public void UpdateMenu_ShouldThrow_WhenMenuHasNoItems()
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Today);

        this._menuRepositoryMock.Setup(repository => repository.Exists(date)).Returns(true);

        Menu menu = new Menu()
        {
            Date = date,
            Items = []
        };

        Assert.Throws<InvalidMenuException>(() => this._menuService.UpdateMenu(menu));
    }

    [Fact]
    public void DeleteMenu_ShouldSucceed_WhenValidRequest()
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Today);

        this._menuRepositoryMock.Setup(repository => repository.Exists(date)).Returns(true);

        this._menuService.DeleteMenu(date);

        this._menuRepositoryMock.Verify(r => r.Delete(date), Times.Once);
    }

    [Fact]
    public void DeleteMenu_ShouldThrow_WhenMenuDoesNotExists()
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Today);

        this._menuRepositoryMock.Setup(repository => repository.Exists(date)).Returns(false);

        Assert.Throws<KeyNotFoundException>(() => this._menuService.DeleteMenu(date));
    }

    #endregion
}