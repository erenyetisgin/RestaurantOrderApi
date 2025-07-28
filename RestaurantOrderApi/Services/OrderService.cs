using RestaurantOrderApi.Exceptions;
using RestaurantOrderApi.Models;
using RestaurantOrderApi.Repositories;

namespace RestaurantOrderApi.Services;

public class OrderService : IOrderService
{
    #region Variables

    private readonly IOrderRepository _orderRepository;

    private readonly IMenuRepository _menuRepository;

    #endregion

    #region Constructors

    public OrderService(IOrderRepository orderRepository, IMenuRepository menuRepository)
    {
        this._orderRepository = orderRepository;
        this._menuRepository = menuRepository;
    }

    #endregion

    #region Methods

    public Order CreateOrder(CreateOrderRequest orderRequest)
    {
        if (orderRequest.ItemNames.Count == 0)
        {
            throw new InvalidOrderException("The order request must have at least one item name.");
        }

        Menu? menu = this._menuRepository.GetByDate(orderRequest.Date);
        if (menu is null)
        {
            throw new MenuNotFoundException(orderRequest.Date);
        }

        List<string> invalidItems =
            orderRequest.ItemNames.Except(menu.Items.Select(menuItem => menuItem.Name)).ToList();
        if (invalidItems.Count != 0)
        {
            throw new InvalidOrderException(
                $"Following items from the order are not on the menu: {string.Join(',', invalidItems)}");
        }

        List<MenuItem> orderItems = menu.Items.Where(item => orderRequest.ItemNames.Contains(item.Name)).ToList();

        Order order = new Order()
        {
            Id = Guid.NewGuid(),
            Date = orderRequest.Date,
            Items = orderItems
        };

        this._orderRepository.Create(order);

        return order;
    }

    public Order? GetOrder(Guid id)
    {
        return this._orderRepository.GetById(id);
    }

    #endregion
}