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
    public class RegionCoordinatorsController : ControllerBase
    {
        private readonly GiveLifeContext _context;

        public RegionCoordinatorsController(GiveLifeContext context)
        {
            _context = context;
        }

        // GET: api/RegionCoordinators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionCoordinator>>> GetRegionCoordinator()
        {
            return await _context.RegionCoordinator.ToListAsync();
        }

        // GET: api/RegionCoordinators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegionCoordinator>> GetRegionCoordinator(int id)
        {
            var regionCoordinator = await _context.RegionCoordinator.FindAsync(id);

            if (regionCoordinator == null)
            {
                return NotFound();
            }

            return regionCoordinator;
        }

        // PUT: api/RegionCoordinators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegionCoordinator(int id, RegionCoordinator regionCoordinator)
        {
            if (id != regionCoordinator.CoordId)
            {
                return BadRequest();
            }

            _context.Entry(regionCoordinator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionCoordinatorExists(id))
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

        // POST: api/RegionCoordinators
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RegionCoordinator>> PostRegionCoordinator(RegionCoordinator regionCoordinator)
        {
            _context.RegionCoordinator.Add(regionCoordinator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegionCoordinator", new { id = regionCoordinator.CoordId }, regionCoordinator);
        }

        // DELETE: api/RegionCoordinators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegionCoordinator(int id)
        {
            var regionCoordinator = await _context.RegionCoordinator.FindAsync(id);
            if (regionCoordinator == null)
            {
                return NotFound();
            }

            _context.RegionCoordinator.Remove(regionCoordinator);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // PUT: api/RegionCoordinators/deposite/5

        [HttpPut("deposite/{id}")]
        public async Task<IActionResult> DepositeWallet(int id, decimal Amount)
        {
            if (!RegionCoordinatorExists(id))
            {
                return NotFound();
            }
            var regionCoordinator = await _context.RegionCoordinator.FindAsync(id);
            regionCoordinator.WalletBalance = regionCoordinator.WalletBalance + Amount;
            _context.Entry(regionCoordinator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(regionCoordinator);
        }


        private bool RegionCoordinatorExists(int id)
        {
            return _context.RegionCoordinator.Any(e => e.CoordId == id);
        }
    }
}
