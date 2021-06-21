using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GiveLifeAPI.Models;
using GiveLife_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace GiveLife_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly GiveLifeContext _context;

        public PostsController(GiveLifeContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPost()
        {
            return await _context.Post.ToListAsync();
        }

        //GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Post.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // GET: api/Posts/getCoordinatorposts
        [HttpGet("getCoordinatorposts"), Authorize]
        public ActionResult GetCoorPosts()
        {
            var currentUser = HttpContext.User;
            int coordId = int.Parse(currentUser.Claims.FirstOrDefault(i => i.Type == "CoordinatorID").Value);
            try
            {
                var posts = _context.Post.Where(c => c.CoordId == coordId).ToList();
                return Ok(new { Post = posts, success = true });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        // GET: api/Posts/getRegionposts
        [HttpGet("getRegionposts"), Authorize]
        public ActionResult getRegionposts()
        {
            var currentUser = HttpContext.User;
            int RegionId = int.Parse(currentUser.Claims.FirstOrDefault(i => i.Type == "RegionID").Value);
            try
            {
                var posts = _context.Post.Where(c => c.RegionId == RegionId ).Where(c=>c.Status != PostStatus.completed).ToList();
                return Ok(new { Post = posts, success = true });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /////////////////////////////////FILTER BY NEED CATEGORY 

        [HttpGet("{needCategory:alpha}")]
       
        public async Task<ActionResult<Post>> GetPost(string needCategory)
        {
            
            return new JsonResult( _context.Post.Where(h => h.NeedCatogry.ToString().StartsWith(needCategory)).ToArray());
        }

        //////////////////////////FILTER BY REST AMOUNT
        [HttpGet]
        [Route("filterRestAmount/{restAmount}")]
        public async Task<ActionResult<Post>> GetPost(decimal restAmount)
        {

            return new JsonResult(_context.Post.Where(h => h.RestAmount.Equals(restAmount)).ToArray());
        }


        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.PostId)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            _context.Post.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.PostId }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //RegionCoordinator or inline donner can donnate to post
        // put: api/Posts/donate/5
        [HttpPut("donate/{id}")]
        public async Task<IActionResult> donatePost(int id,int? coordId,int? OnlinedonnerId,decimal donateAmount)
        {
            RegionCoordinator regionCoordinator=null;
            OnlineDonner onlineDonner=null;
            if (!PostExists(id))
            {
                return NotFound();
            }

            //get post
            var post = await _context.Post.FindAsync(id);
            //get region Admin
            var regionAdmin= await _context.RegionAdmin.FirstOrDefaultAsync(ra=>ra.RegionId==post.RegionId);
            if (coordId != null&& OnlinedonnerId != null)
            {
                return BadRequest();
            }
            else if(coordId != null)
            {
                 regionCoordinator = await _context.RegionCoordinator.FirstOrDefaultAsync(rc => rc.CoordId == coordId);
                regionCoordinator.WalletBalance = regionCoordinator.WalletBalance - donateAmount;
            }else if(OnlinedonnerId != null)
            {
                onlineDonner = await _context.OnlineDonner.FirstOrDefaultAsync(od => od.DonnerId == OnlinedonnerId);
                onlineDonner.WalletBalance = onlineDonner.WalletBalance-donateAmount;        
            }
            else
            {
                return NotFound("Donner not exist");
            }


            post.RestAmount = post.RestAmount - donateAmount;
            regionAdmin.BankAccountBalance = regionAdmin.BankAccountBalance + donateAmount;
            
            _context.Entry(post).State = EntityState.Modified;
            _context.Entry(regionAdmin).State = EntityState.Modified;
            if (onlineDonner != null)
            {
            _context.Entry(onlineDonner).State = EntityState.Modified;

            }
            if (regionCoordinator!=null)
            {

            _context.Entry(regionCoordinator).State = EntityState.Modified;
            }
            _context.Add(new Donnation() { DonnationAmout = donateAmount , RegionCoordinatorId=regionCoordinator?.CoordId,OnlineDonnerId=onlineDonner?.DonnerId,RegionId=post.RegionId });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                }

            return Ok(post);
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.PostId == id);
        }
    }
}
