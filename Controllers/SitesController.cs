using ICTS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTS_API.Controllers
{
    [ApiController]
    public class SitesController : Controller
    {
        private readonly MyWebApiContext _context;

        public SitesController(MyWebApiContext context)
        {
            _context = context;
        }

        [Route("sites")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Site>>> GetAllSites()
        {
            return await _context.Sites.ToListAsync();
        }

        [Route("sites/{SiteID}")]
        [HttpGet]
        public async Task<ActionResult<Site>> GetSiteByID(int SiteID)
        {
            var site = await _context.Sites.FindAsync(SiteID);

            if (site == null)
            {
                return NotFound();
            }

            return site;
        }

        [Route("sites")]
        [HttpPost]
        public async Task<ActionResult<Site>> AddSite(Site site)
        {
            _context.Sites.Add(site);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSiteByID), new { siteID = site.SiteID }, site);
        }

        [Route("sites/{SiteID}")]
        [HttpDelete]
        public async Task<IActionResult> RemoveSite(int SiteID)
        {
            var site = await _context.Sites.FindAsync(SiteID);

            if (site == null)
            {
                return NotFound();
            }

            _context.Sites.Remove(site);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //TODO:UpdateSites*********************************************************************************
        //[Route("sites")]
        //[HttpPut]
        //public void UpdateSite()
        //{

        //}
    }
}
