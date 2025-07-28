using Microsoft.AspNetCore.Mvc;
using RestaurantOrderApi.Exceptions;
using RestaurantOrderApi.Models;
using RestaurantOrderApi.Services;

namespace RestaurantOrderApi.Controllers;

/// <summary>
/// Controller for managing restaurant orders.
/// </summary>
[ApiController]
[Route("orders")]
[Produces("application/json")]
public class OrderController : ControllerBase
{
    #region Variables

    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderController"/> class.
    /// </summary>
    /// <param name="logger">The logger instance for this controller.</param>
    /// <param name="orderService">The service for managing orders.</param>
    public OrderController(ILogger<OrderController> logger, IOrderService orderService)
    {
        this._logger = logger;
        this._orderService = orderService;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Retrieves an order by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the order.</param>
    /// <response code="200">Returns the requested order</response>
    /// <response code="404">If the order is not found</response>
    /// <response code="500">If there was an internal server error</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
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

    /// <summary>
    /// Creates a new order based on the provided request.
    /// </summary>
    /// <param name="orderRequest">The order request containing order details.</param>
    /// <response code="201">Returns the newly created order</response>
    /// <response code="400">If the order request is invalid</response>
    /// <response code="404">If the menu for the specified date is not found</response>
    /// <response code="500">If there was an internal server error</response>
    [HttpPost]
    [ProducesResponseType(typeof(Order), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
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

            return this.BadRequest(new ErrorResponse() { Error = exception.Message });
        }
        catch (MenuNotFoundException exception)
        {
            this._logger.LogWarning(exception, "Menu for date {date} not found.", orderRequest.Date);

            return this.NotFound(new ErrorResponse() { Error = exception.Message });
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, "An unknown error occured while creating order.");

            return this.UnexpectedErrorResult();
        }
    }

    /// <summary>
    /// Creates a standardized response for unexpected errors.
    /// </summary>
    /// <returns>A 500 Internal Server Error response with a generic error message.</returns>
    private ObjectResult UnexpectedErrorResult()
    {
        return StatusCode(500, new ErrorResponse() { Error = "An unexpected error occurred. Please try again later." });
    }

    #endregion
}