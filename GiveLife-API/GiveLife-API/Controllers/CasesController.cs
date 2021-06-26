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
    public class CasesController : ControllerBase
    {
        private readonly GiveLifeContext _context;

        public CasesController(GiveLifeContext context)
        {
            _context = context;
        }

        // GET: api/Cases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cases>>> GetCases()
        {
            return await _context.Cases.ToListAsync();
        }

        // GET: api/Cases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cases>> GetCases(string id)
        {
            var cases = await _context.Cases.FindAsync(id);

            if (cases == null)
            {
                return NotFound();
            }

            return cases;
        }

        // PUT: api/Cases/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCases(string id, Cases cases)
        {
            if (id != cases.NationalId)
            {
                return BadRequest();
            }

            _context.Entry(cases).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CasesExists(id))
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
        //Change case status

        [HttpPut]
        [Route("changeStatus/{id}")]

        public async Task<IActionResult> ChangeStatus(string id, string status) {
          
            var case1 =await _context.Cases.FindAsync(id);
            if (case1 == null)
            {
                return BadRequest();
            }
            if (((status.ToLower()) != "pending") && ((status.ToLower()) != "accepted") && ((status.ToLower()) != "rejected"))
            {
                return BadRequest();
            }
           
            case1.Status = (CaseStatus)Enum.Parse(typeof(CaseStatus), status.ToLower(), true); ;
            _context.Entry(case1).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(case1);

        }

        // POST: api/Cases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cases>> PostCases(Cases cases)
        {
            _context.Cases.Add(cases);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CasesExists(cases.NationalId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCases", new { id = cases.NationalId }, cases);
        }

        // DELETE: api/Cases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCases(string id)
        {
            var cases = await _context.Cases.FindAsync(id);
            if (cases == null)
            {
                return NotFound();
            }

            _context.Cases.Remove(cases);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CasesExists(string id)
        {
            return _context.Cases.Any(e => e.NationalId == id);
        }
    }
}
