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
    public class ProductsController : Controller
    {
        private readonly MyWebApiContext _context;

        public ProductsController(MyWebApiContext context)
        {
            _context = context;
        }

        [Route("products/cartid/{CartID}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCartID(int CartID)
        {
            return await _context.Products.Include(p => p.Cart).ThenInclude(c => c.Location)
                .Where(p => p.CartID == CartID).ToListAsync();
        }

        [Route("products/cartid/{CartID}/lotid/{LotID}")]
        [HttpGet]
        public async Task<ActionResult<Product>> GetProductByLotID(int CartID, string LotID)
        {
            var product = await _context.Products.Include(p => p.Cart).ThenInclude(c => c.Location)
                .Where(p => p.CartID == CartID && p.LotID == LotID).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [Route("products/cartid/{CartID}/productname/{ProductName}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByName(int CartID, string ProductName)
        {
            return await _context.Products.Include(p => p.Cart).ThenInclude(c => c.Location)
                .Where(p => p.CartID == CartID && p.ProductName == ProductName).ToListAsync();
        }

        [Route("products/cartid/{CartID}/expirationdate/{ExpirationDate}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByExpirationDate(int CartID, DateTime ExpirationDate)
        {
            return await _context.Products.Include(p => p.Cart).ThenInclude(c => c.Location)
                .Where(p => p.CartID == CartID && p.ExpirationDate == ExpirationDate).ToListAsync();
        }

        [Route("products/cartid/{CartID}/nearexpiration")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsNearExpirationDate(int CartID)
        {
            return await _context.Products.Include(p => p.Cart).ThenInclude(c => c.Location)
                .Where(p => p.CartID == CartID
                && (p.ExpirationDate - DateTime.Today).TotalDays <= 7
                && (p.ExpirationDate - DateTime.Today).TotalDays > 0).ToListAsync();
        }

        [Route("products/cartid/{CartID}/expired")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetExpiredProducts(int CartID)
        {
            return await _context.Products.Include(p => p.Cart).ThenInclude(c => c.Location)
                .Where(p => p.CartID == CartID
                && (p.ExpirationDate - DateTime.Today).TotalDays <= 0).ToListAsync();
        }

        [Route("products")]
        [HttpPost]
        public async Task<ActionResult<Product>> AddProductToCart(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductByLotID), new { cartID = product.CartID, lotID = product.LotID }, product);
        }

        [Route("products/{LotID}")]
        [HttpDelete]
        public async Task<IActionResult> RemoveProductFromCart(string LotID)
        {
            var product = await _context.Products.FindAsync(LotID);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //TODO:UpdateProduct*********************************************************************************
        //[Route("products")]
        //[HttpPut]
        //public async Task<ActionResult<Product>> UpdateProduct(int CartID, [FromBody]string TagAddress)
        //{
        //    return null;
        //}
    }
}
