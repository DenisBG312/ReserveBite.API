using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReserveBite.Api.Data;
using ReserveBite.Api.Models;

namespace ReserveBite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RestaurantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-restaurants")]
        public IActionResult GetRestaurants()
        {
            var response = _context.Restaurants.ToList();

            return Ok(response);
        }

        [HttpGet("get-restaurant/{id}")]
        public IActionResult GetRestaurant(int id)
        {
            var restaurant = _context.Restaurants
                .Include(r => r.Cuisine)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpPost("create-restaurant")]
        public async Task<ActionResult<Restaurant>> CreateRestaurant(RestaurantCreateDto restaurantDto)
        {
            var cuisineExists = await _context.Cuisines.AnyAsync(c => c.Id == restaurantDto.CuisineId);
            if (!cuisineExists)
            {
                return BadRequest("Invalid Cuisine ID.");
            }

            var restaurant = new Restaurant
            {
                Name = restaurantDto.Name,
                CuisineId = restaurantDto.CuisineId,
                Description = restaurantDto.Description,
                Rating = restaurantDto.Rating,
                Address = restaurantDto.Address,
                Phone = restaurantDto.Phone,
                ImageUrl = restaurantDto.ImageUrl
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.Id }, restaurant);
        }


        [HttpPut("update-restaurant/{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            var cuisineExists = await _context.Cuisines.AnyAsync(c => c.Id == restaurant.CuisineId);
            if (!cuisineExists)
            {
                return BadRequest("Invalid Cuisine ID.");
            }

            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpGet("get-restaurants-by-cuisine/{categoryName}")]
        public async Task<ActionResult<IEnumerable<RestaurantCategoryDto>>> GetRestaurantsByCuisine(string categoryName)
        {
            var cuisine = await _context.Cuisines
                .FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());

            if (cuisine == null)
            {
                return NotFound("Cuisine not found");
            }

            var restaurants = await _context.Restaurants
                .Where(r => r.CuisineId == cuisine.Id)
                .ToListAsync();

            var restaurantDtos = restaurants.Select(r => new RestaurantCategoryDto()
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Rating = r.Rating,
                Address = r.Address,
                Phone = r.Phone,
                ImageUrl = r.ImageUrl
            }).ToList();

            return Ok(restaurantDtos);
        }


        [HttpDelete("delete-restaurant/{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.Id == id);
        }
    }
}
