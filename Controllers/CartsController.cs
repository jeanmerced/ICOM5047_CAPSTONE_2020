using ICTS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTS_API.Controllers
{
    [ApiController]
    public class CartsController : Controller
    {
        private readonly MyWebApiContext _context;

        public CartsController(MyWebApiContext context)
        {
            _context = context;
        }

        [Route("carts")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetAllCarts()
        {
            return await _context.Carts.Include(c => c.Products).Include(c => c.Location).ToListAsync();
        }

        [Route("carts/{CartID}")]
        [HttpGet]
        public async Task<ActionResult<Cart>> GetCartByID(int CartID)
        {
            var cart = await _context.Carts.Include(c => c.Products).Include(c => c.Location).FirstOrDefaultAsync(c =>c.CartID == CartID);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        [Route("carts/tagaddress/{TagAddress}")]
        [HttpGet]
        public async Task<ActionResult<Cart>> GetCartByTagAddress(string TagAddress)
        {
            var cart = await _context.Carts.Include(c => c.Products).Include(c => c.Location).FirstOrDefaultAsync(c => c.TagAddress == TagAddress);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        [Route("carts/lotid/{LotID}")]
        [HttpGet]
        public async Task<ActionResult<Cart>> GetCartByLotID(string LotID)
        {
            return await _context.Carts.Include(c => c.Products).Include(c => c.Location)
            .Where(c => c.Products.Any(p => p.LotID == LotID)).FirstOrDefaultAsync();
        }

        [Route("carts/productname/{ProductName}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartsByProductName(string ProductName)
        {
            return await _context.Carts.Include(c => c.Products).Include(c => c.Location)
            .Where(c => c.Products.Any(p => p.ProductName == ProductName)).ToListAsync();
        }
      
        [Route("carts/expirationdate/{ExpirationDate}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartsByProductExpirationDate(DateTime ExpirationDate)
        {
            return await _context.Carts.Include(c => c.Products).Include(c => c.Location)
            .Where(c => c.Products.Any(p => p.ExpirationDate == ExpirationDate)).ToListAsync();
        }

        [Route("carts/nearexpiration")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartsWithProductsNearExpirationDate()
        {
            return await _context.Carts.Include(c => c.Products).Include(c => c.Location)
            .Where(c => c.Products.Any(p => (p.ExpirationDate - DateTime.Today).TotalDays <= 7
            && (p.ExpirationDate - DateTime.Today).TotalDays > 0)).ToListAsync();
        }

        [Route("carts/expired")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartsWithExpiredProducts()
        {
            return await _context.Carts.Include(c => c.Products).Include(c => c.Location)
            .Where(c => c.Products.Any(p => (p.ExpirationDate - DateTime.Today).TotalDays <= 0)).ToListAsync();
        }

        [Route("carts/sitename/{SiteName}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartsBySiteName(string SiteName)
        {
            return await _context.Carts.Include(c => c.Products).Include(c => c.Location)
            .Where(c => c.Location.Site.SiteName == SiteName).ToListAsync();
        }

        [Route("carts")]
        [HttpPost]
        public async Task<ActionResult<Cart>> AddCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCartByID), new { cartID = cart.CartID }, cart);
        }

        [Route("carts/{CartID}")]
        [HttpDelete]
        public async Task<IActionResult> RemoveCart(int CartID)
        {
            var cart = await _context.Carts.FindAsync(CartID);

            if (cart == null)
            {
                return NotFound(); 
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //TODO:UpdateCarts*********************************************************************************
        //[Route("carts")]
        //[HttpPut]
        //public void UpdateCart(int CartID, [FromBody]string TagAddress)
        //{

        //}
    }
}
