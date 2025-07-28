using Microsoft.AspNetCore.Mvc;
using RestaurantOrderApi.Exceptions;
using RestaurantOrderApi.Models;
using RestaurantOrderApi.Services;

namespace RestaurantOrderApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    #region Variables

    private readonly ILogger<OrderController> _logger;

    private readonly IOrderService _orderService;

    #endregion

    #region Constructors

    public OrderController(ILogger<OrderController> logger, IOrderService orderService)
    {
        this._logger = logger;
        this._orderService = orderService;
    }

    #endregion

    #region Methods

    [HttpGet("{id:guid}")]
    public IActionResult GetOrder(Guid id)
    {
        try
        {
            Order? order = this._orderService.GetOrder(id);

            return order is null ? this.NotFound() : this.Ok(order);
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, "An unknown error occured while retrieving order.");

            return this.UnexpectedErrorResult();
        }
    }

    [HttpPost]
    public IActionResult CreateOrder(CreateOrderRequest orderRequest)
    {
        try
        {
            Order order = this._orderService.CreateOrder(orderRequest);

            return CreatedAtAction(nameof(this.GetOrder), new { id = order.Id }, order);
        }
        catch (InvalidOrderException exception)
        {
            this._logger.LogWarning(exception, "Order request is invalid.");

            return this.BadRequest(new { error = exception.Message });
        }
        catch (MenuNotFoundException exception)
        {
            this._logger.LogWarning(exception, "Menu for date {date} not found.", orderRequest.Date);

            return this.NotFound(new { error = exception.Message });
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, "An unknown error occured while creating order.");

            return this.UnexpectedErrorResult();
        }
    }

    private ObjectResult UnexpectedErrorResult()
    {
        return StatusCode(500, new { error = "An unexpected error occurred. Please try again later." });
    }

    #endregion
}