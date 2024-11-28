using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReserveBite.Api.Data;
using ReserveBite.Api.Models;

[Route("api/[controller]")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MenuController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Create Menu for a specific Restaurant
    [HttpPost("create-menu/{restaurantId}")]
    public async Task<IActionResult> CreateMenu(int restaurantId, [FromBody] MenuCreateDto menuDto)
    {
        // Check if the restaurant exists
        var restaurant = await _context.Restaurants
            .FirstOrDefaultAsync(r => r.Id == restaurantId);
        if (restaurant == null)
        {
            return NotFound(new { Message = "Restaurant not found." });
        }

        var menu = new Menu
        {
            Name = menuDto.Name,
            RestaurantId = restaurantId
        };

        // Add the menu to the database
        _context.Menus.Add(menu);
        await _context.SaveChangesAsync();

        Console.WriteLine($"Menu created: {menu.Name}"); // Log menu creation

        return CreatedAtAction(nameof(GetMenuById), new { id = menu.Id }, menu);
    }

    // View Menu by Restaurant ID (Fetching menu for a specific restaurant)
    [HttpGet("get-menu-by-restaurant/{restaurantId}")]
    public async Task<IActionResult> ViewMenuByRestaurant(int restaurantId)
    {
        var menu = await _context.Menus
            .Include(r => r.Restaurant)
            .Include(m => m.MenuItems)  // Include menu items if needed
            .Where(m => m.RestaurantId == restaurantId)
            .FirstOrDefaultAsync();

        if (menu == null)
        {
            return NotFound("Menu not found.");
        }

        return Ok(menu);
    }

    // Create Menu Item for a specific Menu
    [HttpPost("create-menu-item/{menuId}")]
    public async Task<IActionResult> CreateMenuItem(int menuId, [FromBody] MenuItemCreateDto menuItemDto)
    {
        // Check if menu exists
        var menu = await _context.Menus
            .FirstOrDefaultAsync(m => m.Id == menuId);
        if (menu == null)
        {
            return NotFound(new { Message = "Menu not found." });
        }

        // Map the DTO to the actual MenuItem entity
        var menuItem = new MenuItem
        {
            Name = menuItemDto.Name,
            Description = menuItemDto.Description,
            Price = menuItemDto.Price,
            ImageUrl = menuItemDto.ImageUrl,
            MenuId = menuId
        };

        // Add the menu item to the database
        _context.MenuItems.Add(menuItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMenuItemById), new { id = menuItem.Id }, menuItem);
    }

    // Get Menu by Menu ID
    [HttpGet("get-menu-by-id/{id}")]
    public async Task<IActionResult> GetMenuById(int id)
    {
        var menu = await _context.Menus
            .Include(m => m.MenuItems) // Include items for the menu
            .FirstOrDefaultAsync(m => m.Id == id);

        if (menu == null)
        {
            return NotFound(new { Message = "Menu not found." });
        }

        return Ok(menu);
    }

    // Get Menu Item by ID
    [HttpGet("get-menu-item/{id}")]
    public async Task<IActionResult> GetMenuItemById(int id)
    {
        var menuItem = await _context.MenuItems
            .FirstOrDefaultAsync(mi => mi.Id == id);

        if (menuItem == null)
        {
            return NotFound(new { Message = "Menu item not found." });
        }

        return Ok(menuItem);
    }

    // Get Menu Items by Menu ID
    [HttpGet("get-menu-items-by-menu/{menuId}")]
    public async Task<IActionResult> GetMenuItemsByMenu(int menuId)
    {
        // Fetch all menu items for the specific menu
        var menuItems = await _context.MenuItems
            .Where(mi => mi.MenuId == menuId)
            .ToListAsync();

        if (menuItems == null || !menuItems.Any())
        {
            return NotFound(new { Message = "No menu items found for this menu." });
        }

        return Ok(menuItems);
    }
}
