﻿using System;
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
    public class RegionsController : ControllerBase
    {
        private readonly GiveLifeContext _context;

        public RegionsController(GiveLifeContext context)
        {
            _context = context;
        }

        // GET: api/Regions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegion()
        {
            return await _context.Region.ToListAsync();
        }

        // GET: api/Regions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Region>> GetRegion(int id)
        {
            var region = await _context.Region.FindAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return region;
        }

        /////////////////////////////////FILTER BY Name 

        [HttpGet("{name:alpha}")]

        public async Task<ActionResult<Post>> GetRegion(string name)
        {

            return new JsonResult(_context.Region.Where(h => h.Name.StartsWith(name)).ToArray());
        }

        // PUT: api/Regions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegion(int id, Region region)
        {
            if (id != region.RegionId)
            {
                return BadRequest();
            }

            _context.Entry(region).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionExists(id))
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

        // POST: api/Regions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Region>> PostRegion(Region region)
        {
            _context.Region.Add(region);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegion", new { id = region.RegionId }, region);
        }

        // DELETE: api/Regions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            var region = await _context.Region.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            _context.Region.Remove(region);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegionExists(int id)
        {
            return _context.Region.Any(e => e.RegionId == id);
        }
    }
}
