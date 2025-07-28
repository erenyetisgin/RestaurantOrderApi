using Moq;
using RestaurantOrderApi.Exceptions;
using RestaurantOrderApi.Models;
using RestaurantOrderApi.Repositories;
using RestaurantOrderApi.Services;

namespace RestaurantOrderApi.Tests;

public class OrderServiceTests
{
    #region Variables

    private Mock<IMenuRepository> _menuRepositoryMock;

    private Mock<IOrderRepository> _orderRepositoryMock;

    private OrderService _orderService;

    #endregion

    #region Constructors

    public OrderServiceTests()
    {
        this._menuRepositoryMock = new Mock<IMenuRepository>();
        this._orderRepositoryMock = new Mock<IOrderRepository>();
        this._orderService = new OrderService(this._orderRepositoryMock.Object, this._menuRepositoryMock.Object);
    }

    #endregion

    #region Methods

    [Fact]
    public void CreateOrder_ShouldSucceed_WhenValidRequest()
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Today);

        Menu menu = new Menu()
        {
            Date = date,
            Items =
            [
                new MenuItem()
                {
                    Name = "Pizza",
                    Description = "Italiano Mamma Mia",
                    Price = 250
                },
                new MenuItem()
                {
                    Name = "Pasta",
                    Description = "Pesto Pasta",
                    Price = 200
                }
            ]
        };

        this._menuRepositoryMock.Setup(repository => repository.GetByDate(date)).Returns(menu);

        CreateOrderRequest createOrderRequest = new CreateOrderRequest()
        {
            Date = date,
            ItemNames =
            [
                "Pizza", "Pasta"
            ]
        };

        Order result = this._orderService.CreateOrder(createOrderRequest);

        Assert.NotNull(result);
        Assert.Equal(createOrderRequest.Date, result.Date);
        Assert.Equal(2, result.Items.Count);
        Assert.Contains(result.Items, item => item.Name == "Pizza");
        Assert.Contains(result.Items, item => item.Name == "Pasta");
        this._orderRepositoryMock.Verify(repository => repository.Create(It.IsAny<Order>()), Times.Once);
    }

    [Fact]
    public void CreateOrder_ShouldThrow_WhenMenuDoesNotExist()
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Today);

        this._menuRepositoryMock.Setup(repository => repository.GetByDate(date)).Returns((Menu?)null);

        CreateOrderRequest orderRequest = new CreateOrderRequest()
        {
            Date = date,
            ItemNames = ["Karnıyarık"]
        };

        Assert.Throws<MenuNotFoundException>(() => this._orderService.CreateOrder(orderRequest));
    }

    [Fact]
    public void CreateOrder_ShouldThrow_WhenOrderItemsEmpty()
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Today);

        Menu menuMock = new Menu()
        {
            Date = date,
            Items =
            [
                new MenuItem()
                {
                    Name = "Spagetti",
                    Description = "Çıbık makarna",
                    Price = 150
                },
                new MenuItem()
                {
                    Name = "Köfte Ekmek",
                    Description = "Köfte Ekmek",
                    Price = 200
                }
            ]
        };

        this._menuRepositoryMock.Setup(repository => repository.GetByDate(date)).Returns(menuMock);

        CreateOrderRequest orderRequest = new CreateOrderRequest()
        {
            Date = date,
            ItemNames = []
        };

        Assert.Throws<InvalidOrderException>(() => this._orderService.CreateOrder(orderRequest));
    }

    [Fact]
    public void CreateOrder_ShouldThrow_WhenOrderItemsNotInMenu()
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.Today);

        Menu menuMock = new Menu()
        {
            Date = date,
            Items =
            [
                new MenuItem()
                {
                    Name = "Spagetti",
                    Description = "Çıbık makarna",
                    Price = 150
                },
                new MenuItem()
                {
                    Name = "Köfte Ekmek",
                    Description = "Köfte Ekmek",
                    Price = 200
                }
            ]
        };

        this._menuRepositoryMock.Setup(repository => repository.GetByDate(date)).Returns(menuMock);

        CreateOrderRequest orderRequest = new CreateOrderRequest()
        {
            Date = date,
            ItemNames = ["Spagetti", "Balık Ekmek"]
        };

        Assert.Throws<InvalidOrderException>(() => this._orderService.CreateOrder(orderRequest));
    }
    
    #endregion
}