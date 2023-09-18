using BlogAPI.Context;
using BlogAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly AppDbContext _authContext;

        public CommentController(AppDbContext authContext)
        {
            _authContext = authContext;
        }
        [HttpGet("getAllComments")]
        public async Task<IActionResult> getAllComments()
        {
            var blogs = await _authContext.Comments.ToListAsync();

            return Ok(blogs);
        }
        [HttpGet]
        [Route("getById/{id:int}")]
        public async Task<IActionResult> getCommentByBlogId(int id)
        {
            //var comments = await _authContext.Comments.FirstOrDefaultAsync(x => x.blogId == id);
            var filterData=await _authContext.Comments.Select(x=>x.blogId==id).ToListAsync();
            var resultsets = filterData.ToList();

           

            if (resultsets != null)
            {   
                return Ok(resultsets);
            }
            return NotFound();
        }
        [HttpPost("postComment")]
        public async Task<IActionResult> addBlog(Comment commentAddRequest)
        {
            var commentObject = new Comment()
            {
                blogId = commentAddRequest.blogId,
                comment = commentAddRequest.comment,
                userName = commentAddRequest.userName
            };
            await _authContext.Comments.AddAsync(commentObject);
            await _authContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("deleteCommentById/{id:int}")]
        public async Task<IActionResult> deleteBlog([FromRoute] int id)
        {
            var existingComment = await _authContext.Comments.FindAsync(id);
            if (existingComment != null)
            {
                _authContext.Remove(existingComment);
                await _authContext.SaveChangesAsync();
                return Ok(existingComment);
            }
            return NotFound();
        }
    }
}
