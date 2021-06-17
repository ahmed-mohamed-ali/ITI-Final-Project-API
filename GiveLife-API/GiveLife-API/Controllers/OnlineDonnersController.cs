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
    public class OnlineDonnersController : ControllerBase
    {
        private readonly GiveLifeContext _context;

        public OnlineDonnersController(GiveLifeContext context)
        {
            _context = context;
        }

        // GET: api/OnlineDonners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OnlineDonner>>> GetOnlineDonner()
        {
            return await _context.OnlineDonner.ToListAsync();
        }

        // GET: api/OnlineDonners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OnlineDonner>> GetOnlineDonner(int id)
        {
            var onlineDonner = await _context.OnlineDonner.FindAsync(id);

            if (onlineDonner == null)
            {
                return NotFound();
            }

            return onlineDonner;
        }

        // PUT: api/OnlineDonners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOnlineDonner(int id, OnlineDonner onlineDonner)
        {
            if (id != onlineDonner.DonnerId)
            {
                return BadRequest();
            }

            _context.Entry(onlineDonner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OnlineDonnerExists(id))
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

        // POST: api/OnlineDonners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OnlineDonner>> PostOnlineDonner(OnlineDonner onlineDonner)
        {
            _context.OnlineDonner.Add(onlineDonner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOnlineDonner", new { id = onlineDonner.DonnerId }, onlineDonner);
        }

        // DELETE: api/OnlineDonners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOnlineDonner(int id)
        {
            var onlineDonner = await _context.OnlineDonner.FindAsync(id);
            if (onlineDonner == null)
            {
                return NotFound();
            }

            _context.OnlineDonner.Remove(onlineDonner);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        //// PUT: /api/OnlineDonners/deposite/1?Amount=10

        [HttpPut("deposite/{id}")]
        public async Task<IActionResult> DepositeWallet(int id, decimal Amount)
        {
            if (!OnlineDonnerExists(id))
            {
                return NotFound();
            }
            var onlinedonner = await _context.OnlineDonner.FindAsync(id);
            onlinedonner.WalletBalance = onlinedonner.WalletBalance + Amount;
            _context.Entry(onlinedonner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(onlinedonner);
        }


        private bool OnlineDonnerExists(int id)
        {
            return _context.OnlineDonner.Any(e => e.DonnerId == id);
        }
    }
}
