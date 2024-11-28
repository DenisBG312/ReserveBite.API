using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveBite.Api.Data;

namespace ReserveBite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuisinesController : ControllerBase
    {
        private ApplicationDbContext _context;
        public CuisinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-cuisines")]
        public IActionResult GetCuisines()
        {
            var response = _context.Cuisines.ToList();

            return Ok(response);
        }
    }
}
