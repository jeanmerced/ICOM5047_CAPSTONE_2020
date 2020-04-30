using ICTS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTS_API.Controllers
{
    [ApiController]
    public class LocationsController : Controller
    {
        private readonly MyWebApiContext _context;

        public LocationsController(MyWebApiContext context)
        {
            _context = context;
        }

        [Route("locations")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetAllLocations()
        {
            return await _context.Locations.ToListAsync();
        }

        [Route("locations/cartid/{CartID}")]
        [HttpGet]
        public async Task<ActionResult<Location>> GetLocationByCartID(int CartID)
        {
            var location = await _context.Locations.Where(l => l.CartID == CartID).FirstOrDefaultAsync();

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        [Route("locations")]
        [HttpPost]
        public async Task<ActionResult<Location>> AddLocation(Location location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLocationByCartID), new { cartID = location.CartID }, location);
        }

        [Route("locations/{LocationID}")]
        [HttpDelete]
        public async Task<IActionResult> RemoveLocation(int LocationID)
        {
            var location = await _context.Locations.FindAsync(LocationID);

            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //TODO:*********************************************************************************
        //[Route("locations")]
        //[HttpPut]
        //public async Task<ActionResult<IEnumerable<Location>>> UpdateLocation(int LocationID)
        //{
        //    return null;
        //}
    }
}
