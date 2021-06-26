using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiveLifeAPI.Models;
using Microsoft.AspNetCore.Authorization;
using GiveLife_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace GiveLife_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionCoordinatorsController : ControllerBase
    {
        private readonly GiveLifeContext _context;

        public IConfiguration Configuration { get; }

        public RegionCoordinatorsController(GiveLifeContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] CoordinatorLoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            var loginUser = _context.RegionCoordinator.FirstOrDefault(u => u.NationalId.ToLower() == user.NationalId.ToLower()&&u.Password.ToLower()==user.Password.ToLower());
            if (loginUser==null)
            {
                return NotFound();
            }

            var token = generateToken(new List<Claim>()
            {
                new Claim ("CoordinatorID",loginUser.CoordId.ToString()),
                new Claim("RegionID",loginUser.RegionId.ToString())
            });

            return Ok(new {Token= token,success=true });
           
        }

         private string generateToken(List<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtOptions:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            
            var tokeOptions = new JwtSecurityToken(
                issuer: Configuration["JwtOptions:Issuer"],
                audience: Configuration["JwtOptions:Audience"],
                claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;

        }

        // GET: api/RegionCoordinators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionCoordinator>>> GetRegionCoordinator()
        {
            //var currentUser = HttpContext.User;
            //if (currentUser.HasClaim(c => c.Type == "CoordinatorID"))
            //{
            //    int x = int.Parse(currentUser.Claims.FirstOrDefault(i => i.Type == "CoordinatorID").Value);
            //    return Ok(new { done = x});
            //}
                return await _context.RegionCoordinator.ToListAsync();
        }

        // GET: api/RegionCoordinators/5
        [HttpGet("getProfile"),Authorize]
        public async Task<ActionResult<RegionCoordinator>> GetONERegionCoordinator()
        {
            var currentUser = HttpContext.User;
            int id = int.Parse(currentUser.Claims.FirstOrDefault(i => i.Type == "CoordinatorID").Value);
            var regionCoordinator = await _context.RegionCoordinator.Include(rc => rc.Post).FirstOrDefaultAsync(r=>r.CoordId==id);
            if (regionCoordinator == null)
            {
                return NotFound();
            }

            return Ok(new { coordinator = regionCoordinator,success=true }); 
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
        [HttpPost("Register")]
        public async Task<ActionResult<RegionCoordinator>> PostRegionCoordinator(CoordinatorRegisterModel NewRegionCoordinator)
        {
            if (RegionCoordinatorExistsByNationalID(NewRegionCoordinator.NID, NewRegionCoordinator.visa))
            {
                return Conflict("national id or visa num exist before");
            }
            
            var region = _context.Region.FirstOrDefault(r => r.Name.ToLower() == NewRegionCoordinator.region.ToLower());
            if (region == null)
            {
                return Conflict("region not exist");
            }
            RegionCoordinator regionCoordinator = new RegionCoordinator();
            regionCoordinator.RegionId = region.RegionId;
            regionCoordinator.Name = NewRegionCoordinator.name;
            regionCoordinator.NationalId = NewRegionCoordinator.NID;
            regionCoordinator.Password = NewRegionCoordinator.password;
            regionCoordinator.VisaNum = NewRegionCoordinator.visa;
            regionCoordinator.WalletBalance = 0;
            regionCoordinator.Deleted = false;
            _context.RegionCoordinator.Add(regionCoordinator);
            await _context.SaveChangesAsync();

            //try {
                
            //} catch {
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
          

            return Ok(new {message="coordinator register successfully",success=true});
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


        // PUT: /api/RegionCoordinators/deposite/1?Amount=10

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
        private bool RegionCoordinatorExistsByNationalID(string nationalId,string visanum)
        {
            return _context.RegionCoordinator.Any(e => e.NationalId == nationalId||e.VisaNum==visanum);
        }
    }
}
