using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiveLifeAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace GiveLife_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponsController : ControllerBase
    {
        private readonly GiveLifeContext _context;

        public CuponsController(GiveLifeContext context)
        {
            _context = context;
        }

        // GET: api/Cupons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cupon>>> GetCupon()
        {
            return await _context.Cupon.ToListAsync();
        }

        // GET: api/Cupons/getCoordinatorCupon
        [HttpGet("getCoordinatorCupon"), Authorize]
        public  ActionResult GetCoorCupons()
        {
            var currentUser = HttpContext.User;
            int coordId = int.Parse(currentUser.Claims.FirstOrDefault(i => i.Type == "CoordinatorID").Value);
            try { 
            var cupons=  _context.Cupon.Include(cs => cs.CaseNational).Where(c => c.CoordId == coordId).ToList();
                return Ok(new { Cupon = cupons, success = true });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Cupons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cupon>> GetCupon(int id)
        {
            var cupon = await _context.Cupon.FindAsync(id);

            if (cupon == null)
            {
                return NotFound();
            }

            return cupon;
        }

        // PUT: api/Cupons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCupon(int id, Cupon cupon)
        {
            if (id != cupon.CuponId)
            {
                return BadRequest();
            }

            _context.Entry(cupon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuponExists(id))
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

        //Check cupon 

        [HttpPut]
        [Route("checkCupon/{id}")]

        public async Task<IActionResult> PutCases(int id, string caseNationalId)
        {

            var cupon1 = await _context.Cupon.FindAsync(id);
            if (cupon1 == null)
            {
                return BadRequest();
            }
            if (cupon1.CaseNationalId != caseNationalId) {
                return NotFound();
            }

            return Ok(cupon1);

        }


        // POST: api/Cupons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cupon>> PostCupon(Cupon cupon)
        {
            _context.Cupon.Add(cupon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCupon", new { id = cupon.CuponId }, cupon);
        }

        // DELETE: api/Cupons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupon(int id)
        {
            var cupon = await _context.Cupon.FindAsync(id);
            if (cupon == null)
            {
                return NotFound();
            }

            _context.Cupon.Remove(cupon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Cupons/5
        [HttpGet("CheckCupon/{Cuponid}")]
        public  IActionResult checkCupon(int Cuponid, string CaseID, string CuponIdentity, string needCatogry)
        {

            if (!CuponExists(Cuponid))
                return NotFound();
            var cupon = _context.Cupon.Find(Cuponid);
            if (cupon.CaseNationalId.ToLower() == CaseID.ToLower() && cupon.CuponIdentity.ToLower() == CuponIdentity.ToLower() && cupon.ProductCategory.ToString() == needCatogry.ToLower()&&cupon.ExpireDate>=DateTime.Now)
            {
                return Ok(cupon);
            }
            return Content("cupon not valid");

        }

        private bool CuponExists(int id)
        {
            return _context.Cupon.Any(e => e.CuponId == id);
        }
    }
}
