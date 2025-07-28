using Microsoft.AspNetCore.Mvc;
using RestaurantOrderApi.Exceptions;
using RestaurantOrderApi.Models;
using RestaurantOrderApi.Services;

namespace RestaurantOrderApi.Controllers;

[ApiController]
[Route("menus")]
public class MenuController : ControllerBase
{
    #region Variables

    private readonly ILogger<MenuController> _logger;

    private readonly IMenuService _menuService;

    #endregion

    #region Constructors

    public MenuController(ILogger<MenuController> logger, IMenuService menuService)
    {
        this._logger = logger;
        this._menuService = menuService;
    }

    #endregion

    #region Methods

    [HttpGet("{date}")]
    public IActionResult GetMenu(DateOnly date)
    {
        Menu? menu = this._menuService.GetMenuByDate(date);

        return menu is null ? this.NotFound() : this.Ok(menu);
    }

    [HttpPost]
    public IActionResult CreateMenu([FromBody] CreateMenuRequest request)
    {
        try
        {
            Menu menu = new Menu
            {
                Date = request.Date,
                Items = request.MenuItems
            };

            this._menuService.CreateMenu(menu);

            return this.CreatedAtAction(nameof(this.GetMenu), new { date = menu.Date }, menu);
        }
        catch (MenuAlreadyExistsException exception)
        {
            this._logger.LogWarning(exception, "Attempt to create duplicate menu for date {Date}", request.Date);

            return this.Conflict(new { error = exception.Message });
        }
        catch (InvalidMenuException exception)
        {
            this._logger.LogWarning(exception, "Invalid menu creation for date {Date}.", request.Date);

            return this.BadRequest(new { error = exception.Message });
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, "An unknown error occured while creating menu for date {RequestDate}.",
                request.Date);

            return this.UnexpectedErrorResult();
        }
    }

    [HttpPut("{date}")]
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

            return this.BadRequest(new { error = exception.Message });
        }
        catch (KeyNotFoundException exception)
        {
            this._logger.LogWarning(exception, "The menu for date {DateOnly} is not found.", date);

            return this.NotFound(new { error = exception.Message });
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, "An unknown error occured while updating menu for date {DateOnly}.", date);

            return this.UnexpectedErrorResult();
        }
    }

    [HttpDelete("{date}")]
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

            return this.NotFound(new { error = exception.Message });
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, "An unknown error occured while updating menu for date {DateOnly}.", date);

            return this.UnexpectedErrorResult();
        }
    }

    private ObjectResult UnexpectedErrorResult()
    {
        return this.StatusCode(500, new { error = "An unexpected error occurred. Please try again later." });
    }

    #endregion
}