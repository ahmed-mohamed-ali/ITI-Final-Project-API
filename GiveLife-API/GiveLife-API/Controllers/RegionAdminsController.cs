using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiveLifeAPI.Models;
using GiveLife_API.Models;

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

        //RegionAdmin transform money to coordinator
        ///put: api/RegionAdmins/transformToCoord/1?coordID=1&Amount=100"
        [HttpPut("transformToCoord/{AdminId}")]
        public async Task<IActionResult> TransformMoney (int AdminId,int coordID, decimal Amount)
        {
            if (!RegionAdminExists(AdminId)||!RegionCoordExists(coordID))
            {
                return NotFound();
            }
            var regionCoordinator = await _context.RegionCoordinator.FindAsync(coordID);
            var regionAdmin = await _context.RegionAdmin.FindAsync(AdminId);
            if (regionAdmin.BankAccountBalance < Amount)
            {
                return Content("Bank Account Balance is less than required Amount of money");
            }

                regionCoordinator.WalletBalance = regionCoordinator.WalletBalance + Amount;
            
            regionAdmin.BankAccountBalance = regionAdmin.BankAccountBalance - Amount;

            _context.Entry(regionCoordinator).State = EntityState.Modified;
            _context.Entry(regionAdmin).State = EntityState.Modified;
            _context.Add(new MoneyTransformation() { RegionAdminId = regionAdmin.AdminId, RegionCoordinatorId = regionCoordinator.CoordId,MoneyAmount=Amount });

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

        //RegionAdmin transform money to organisation
        ///put: api/RegionAdmins/transformToCoord/1?OrgId=1&Amount=100"
        [HttpPut("transformToOrg/{AdminId}")]
        public async Task<IActionResult> transformMoney(int AdminId, int OrgId, decimal Amount)
        {
            if (!RegionAdminExists(AdminId) || !RegionCoordExists(OrgId))
            {
                return NotFound();
            }
            var organisation = await _context.Organization.FindAsync(OrgId);
            var regionAdmin = await _context.RegionAdmin.FindAsync(AdminId);
            if (regionAdmin.BankAccountBalance < Amount)
            {
                return Content("Bank Account Balance is less than required Amount of money");
            }
            organisation.WalletBalance = organisation.WalletBalance + Amount;

            regionAdmin.BankAccountBalance = regionAdmin.BankAccountBalance - Amount;
            _context.Add(new MoneyTransformation() { RegionAdminId = regionAdmin.AdminId, OrganizationId = organisation.OrganizationId, MoneyAmount = Amount });

              try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(regionAdmin);

        }


        ///RegionAdmin change status
        [HttpPut("changeStatus/{AdminId}")]
        public async Task<IActionResult> ChangeStatus(int AdminId, string caseId, string status)
        {
            if (!RegionAdminExists(AdminId) || !CaseExists(caseId))
            {
                return NotFound();
            }
            var case1 = await _context.Cases.FindAsync(caseId);
            var admin1 = await _context.RegionAdmin.FindAsync(AdminId);
            if (case1.RegionId != admin1.RegionId)
            {
                return NotFound();
            }



            if (((status.ToLower()) != "pending") && ((status.ToLower()) != "accepted") && ((status.ToLower()) != "rejected"))
            {
                return BadRequest();
            }



            case1.Status = (CaseStatus)Enum.Parse(typeof(CaseStatus), status.ToLower(), true); ;
            _context.Entry(case1).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {



                return StatusCode(StatusCodes.Status500InternalServerError);
            }



            return Ok(case1);
        }

        private bool CaseExists(string id)
        {
            return _context.Cases.Any(e => e.NationalId == id);
        }

        private bool RegionAdminExists(int id)
        {
            return _context.RegionAdmin.Any(e => e.AdminId == id);
        }
        private bool RegionCoordExists(int id)
        {
            return _context.RegionCoordinator.Any(e => e.CoordId == id);
        }
    }
}
