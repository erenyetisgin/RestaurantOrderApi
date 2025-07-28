using Microsoft.AspNetCore.Mvc;
using RestaurantOrderApi.Exceptions;
using RestaurantOrderApi.Models;
using RestaurantOrderApi.Services;

namespace RestaurantOrderApi.Controllers;

/// <summary>
/// Controller for managing restaurant menus.
/// </summary>
[ApiController]
[Route("menus")]
[Produces("application/json")]
public class MenuController : ControllerBase
{
    #region Variables

    private readonly ILogger<MenuController> _logger;
    private readonly IMenuService _menuService;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuController"/> class.
    /// </summary>
    /// <param name="logger">The logger instance for this controller.</param>
    /// <param name="menuService">The service for managing menus.</param>
    public MenuController(ILogger<MenuController> logger, IMenuService menuService)
    {
        this._logger = logger;
        this._menuService = menuService;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Retrieves a menu for a specific date.
    /// </summary>
    /// <param name="date">The date for which to retrieve the menu.</param>
    /// <response code="200">Returns the requested menu</response>
    /// <response code="404">If the menu is not found</response>
    [HttpGet("{date}")]
    [ProducesResponseType(typeof(Menu), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetMenu(DateOnly date)
    {
        Menu? menu = this._menuService.GetMenuByDate(date);

        return menu is null ? this.NotFound() : this.Ok(menu);
    }

    /// <summary>
    /// Creates a new menu.
    /// </summary>
    /// <param name="request">The request containing menu details.</param>
    /// <response code="201">Returns the newly created menu</response>
    /// <response code="400">If the menu request is invalid</response>
    /// <response code="409">If a menu already exists for the specified date</response>
    /// <response code="500">If there was an internal server error</response>
    [HttpPost]
    [ProducesResponseType(typeof(Menu), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult CreateMenu([FromBody] CreateMenuRequest request)
    {
        try
        {
            Menu menu = this._menuService.CreateMenu(request);

            return this.CreatedAtAction(nameof(this.GetMenu), new { date = request.Date }, menu);
        }
        catch (MenuAlreadyExistsException exception)
        {
            this._logger.LogWarning(exception, "Attempt to create duplicate menu for date {Date}", request.Date);

            return this.Conflict(new ErrorResponse() { Error = exception.Message });
        }
        catch (InvalidMenuException exception)
        {
            this._logger.LogWarning(exception, "Invalid menu creation for date {Date}.", request.Date);

            return this.BadRequest(new ErrorResponse() { Error = exception.Message });
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, "An unknown error occured while creating menu for date {RequestDate}.",
                request.Date);

            return this.UnexpectedErrorResult();
        }
    }

    /// <summary>
    /// Updates an existing menu.
    /// </summary>
    /// <param name="date">The date of the menu to update.</param>
    /// <param name="menuItems">The updated list of menu items.</param>
    /// <response code="204">If the menu was successfully updated</response>
    /// <response code="400">If the menu update request is invalid</response>
    /// <response code="404">If the menu to update is not found</response>
    /// <response code="500">If there was an internal server error</response>
    [HttpPut("{date}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult UpdateMenu(DateOnly date, List<MenuItem> menuItems)
    {
        try
        {
            Menu menu = new Menu
            {
                Date = date,
                Items = menuItems
            };

            this._menuService.UpdateMenu(menu);

            return this.NoContent();
        }
        catch (InvalidMenuException exception)
        {
            this._logger.LogWarning(exception, "Invalid menu update for date {Date}.", date);

            return this.BadRequest(new ErrorResponse() { Error = exception.Message });
        }
        catch (KeyNotFoundException exception)
        {
            this._logger.LogWarning(exception, "The menu for date {DateOnly} is not found.", date);

            return this.NotFound(new ErrorResponse() { Error = exception.Message });
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, "An unknown error occured while updating menu for date {DateOnly}.", date);

            return this.UnexpectedErrorResult();
        }
    }

    /// <summary>
    /// Deletes a menu for a specific date.
    /// </summary>
    /// <param name="date">The date of the menu to delete.</param>
    /// <response code="204">If the menu was successfully deleted</response>
    /// <response code="404">If the menu to delete is not found</response>
    /// <response code="500">If there was an internal server error</response>
    [HttpDelete("{date}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteMenu(DateOnly date)
    {
        try
        {
            this._menuService.DeleteMenu(date);

            return this.NoContent();
        }
        catch (KeyNotFoundException exception)
        {
            this._logger.LogWarning(exception, "The menu for date {DateOnly} is not found.", date);

            return this.NotFound(new ErrorResponse() { Error = exception.Message });
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, "An unknown error occured while updating menu for date {DateOnly}.", date);

            return this.UnexpectedErrorResult();
        }
    }

    /// <summary>
    /// Creates a standardized response for unexpected errors.
    /// </summary>
    /// <returns>A 500 Internal Server Error response with a generic error message.</returns>
    private ObjectResult UnexpectedErrorResult()
    {
        return this.StatusCode(500,
            new ErrorResponse() { Error = "An unexpected error occurred. Please try again later." });
    }

    #endregion
}