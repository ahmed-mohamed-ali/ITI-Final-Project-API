using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiveLifeAPI.Models;

namespace GiveLife_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionAdminsController : ControllerBase
    {
        private readonly GiveLifeContext _context;

        public RegionAdminsController(GiveLifeContext context)
        {
            _context = context;
        }

        // GET: api/RegionAdmins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionAdmin>>> GetRegionAdmin()
        {
            return await _context.RegionAdmin.ToListAsync();
        }

        // GET: api/RegionAdmins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegionAdmin>> GetRegionAdmin(int id)
        {
            var regionAdmin = await _context.RegionAdmin.FindAsync(id);

            if (regionAdmin == null)
            {
                return NotFound();
            }

            return regionAdmin;
        }

        // PUT: api/RegionAdmins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegionAdmin(int id, RegionAdmin regionAdmin)
        {
            if (id != regionAdmin.AdminId)
            {
                return BadRequest();
            }

            _context.Entry(regionAdmin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionAdminExists(id))
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

        // POST: api/RegionAdmins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RegionAdmin>> PostRegionAdmin(RegionAdmin regionAdmin)
        {
            _context.RegionAdmin.Add(regionAdmin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegionAdmin", new { id = regionAdmin.AdminId }, regionAdmin);
        }

        // DELETE: api/RegionAdmins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegionAdmin(int id)
        {
            var regionAdmin = await _context.RegionAdmin.FindAsync(id);
            if (regionAdmin == null)
            {
                return NotFound();
            }

            _context.RegionAdmin.Remove(regionAdmin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegionAdminExists(int id)
        {
            return _context.RegionAdmin.Any(e => e.AdminId == id);
        }
    }
}
