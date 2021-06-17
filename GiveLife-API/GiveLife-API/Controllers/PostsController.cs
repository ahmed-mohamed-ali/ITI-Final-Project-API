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
        [HttpPut("{id}"),Authorize]
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

        // POST: coord/publish

        [HttpPost("coord/publish"),Authorize]
        public async Task<ActionResult<Post>> PostPostBycoord(PublishPost publishPost)
        {
            var caseObj = _context.Cases.Find(publishPost.CaseNationalId);
            var currentUser = HttpContext.User;
            int coordId = int.Parse(currentUser.Claims.FirstOrDefault(i => i.Type == "CoordinatorID").Value);
            int RegionId = int.Parse(currentUser.Claims.FirstOrDefault(i => i.Type == "RegionID").Value);
            if (caseObj == null)
            {
                return NotFound("case not exist");
             }
            if (caseObj.RegionId != RegionId)
            {
                return NotFound("case not exist in coordinator region");
            }
            if (caseObj.Status != CaseStatus.accepted)
            {
                return NotFound("case status not accepted");
            }
          
            Post post = new Post();
            post.CaseId = publishPost.CaseNationalId;
            post.CoordId = coordId;
            post.Deleted = false;
            post.NeedCatogry= (NeedCatogry)Enum.Parse(typeof(NeedCatogry), publishPost.NeedCatogry, true);
            post.PostTxt = publishPost.PostMessage;
            post.RequiredAmount = publishPost.RequiredAmount;
            post.RestAmount = publishPost.RequiredAmount;
            post.Status = PostStatus.pending;
            post.CreatedTime = DateTime.Now;
            post.RegionId = RegionId;
            _context.Post.Add(post);
            await _context.SaveChangesAsync();
            //try
            //{

            //}
            //catch
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            return Ok(new { post, success=true });
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
        [HttpPut("donate/{postId}"),Authorize]
        public async Task<IActionResult> donatePost(int postId, int? OnlinedonnerId,decimal donateAmount)
        {
            RegionCoordinator regionCoordinator=null;
            OnlineDonner onlineDonner=null;
            if (!PostExists(postId))
            {
                return NotFound();
            }
            //get post
            var post = await _context.Post.FindAsync(postId);

            var currentUser = HttpContext.User;
            int coordId = int.Parse(currentUser.Claims.FirstOrDefault(i => i.Type == "CoordinatorID").Value);
            int RegionId = int.Parse(currentUser.Claims.FirstOrDefault(i => i.Type == "RegionID").Value);
            regionCoordinator = await _context.RegionCoordinator.FirstOrDefaultAsync(rc => rc.CoordId == coordId);
           //get region Admin
            var regionAdmin = await _context.RegionAdmin.FirstOrDefaultAsync(ra => ra.RegionId == post.RegionId);
                      
            if (regionCoordinator.WalletBalance < donateAmount)
            {
             return   NotFound("your wallet balance is not allow to donnate");
            }
            if (post.RestAmount < donateAmount)
            {
               return NotFound("donnation is greater than required");
            }

            regionCoordinator.WalletBalance = regionCoordinator.WalletBalance - donateAmount;

            regionAdmin.BankAccountBalance = regionAdmin.BankAccountBalance + donateAmount;
            post.RestAmount = post.RestAmount - donateAmount;
            if (post.RestAmount==0)
            {
                var cuponKey = RandomString(10);
                _context.Add(new Cupon {AmountOfMoney=post.RequiredAmount, CaseNationalId = post.CaseId,CoordId=post.CoordId,CuponIdentity= cuponKey,Deleted=false,ExpireDate=DateTime.Now.AddDays(7),ProductCategory=post.NeedCatogry,RegionId=post.RegionId });
                post.Status = PostStatus.completed;
            }
            
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
            _context.Add(new Donnation() { DonnationAmout = donateAmount , RegionCoordinatorId=regionCoordinator.CoordId,RegionId=post.RegionId });


            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                }

            return Ok(new { post = post, success = true });
        }

        [HttpPut("OnlineDonate/{PostId}")]
        public async Task<IActionResult> donatePost(int PostId, int OnlinedonnerId, decimal donateAmount)
        {
           
            OnlineDonner onlineDonner = null;
            if (!PostExists(PostId))
            {
                return NotFound();
            }

            //get post
            var post = await _context.Post.FindAsync(PostId);
            //get region Admin
            var regionAdmin = await _context.RegionAdmin.FirstOrDefaultAsync(ra => ra.RegionId == post.RegionId);
            
            
            if (!OnlineDonnerExists(OnlinedonnerId))
            {
                return NotFound("Donner not exist");

            }
                onlineDonner = await _context.OnlineDonner.FirstOrDefaultAsync(od => od.DonnerId == OnlinedonnerId);
                onlineDonner.WalletBalance = onlineDonner.WalletBalance - donateAmount;

            post.RestAmount = post.RestAmount - donateAmount;
            regionAdmin.BankAccountBalance = regionAdmin.BankAccountBalance + donateAmount;

            _context.Entry(post).State = EntityState.Modified;
            _context.Entry(regionAdmin).State = EntityState.Modified;
            
            
                _context.Entry(onlineDonner).State = EntityState.Modified;

            
            
            _context.Add(new Donnation() { DonnationAmout = donateAmount, OnlineDonnerId = onlineDonner.DonnerId, RegionId = post.RegionId });

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


        private object PostCupon(Cupon cupon)
        {
            _context.Cupon.Add(cupon);
            try
            {

             _context.SaveChangesAsync();
                return new { success = true };
            }
            catch
            {
                return new { success = false };
            }
           

        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.PostId == id);
        }
        private bool OnlineDonnerExists(int id)
        {
            return _context.OnlineDonner.Any(e => e.DonnerId == id);
        }
    }
}
