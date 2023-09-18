using BlogAPI.Context;
using BlogAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace BlogAPI.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class BlogController : Controller
    {
        private readonly AppDbContext _authContext;

        public BlogController(AppDbContext authContext)
        {
            _authContext = authContext;
        }
        [HttpGet("getAllBlogs")]
        public async Task<IActionResult> getAllBlogs()
        {
            var blogs = await _authContext.Blog.ToListAsync();

            return Ok(blogs);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> getBlogById(int id)
        {
            var blogs = await _authContext.Blog.FirstOrDefaultAsync(x=> x.id== id);

            if (blogs != null)
            {
                return Ok(blogs);
            }
            return NotFound();
        }

        [HttpPost("postBlog")]
        public async Task<IActionResult> addBlog(Blog blogAddRequest)
        {
            var blog = new Blog()
            {
                title = blogAddRequest.title,
                content = blogAddRequest.content,
                summary = blogAddRequest.summary,
                author = blogAddRequest.author,
                imageUrl = blogAddRequest.imageUrl,
                publishDate = DateTime.Now,
                updatedDate = DateTime.Now
            };
            await _authContext.Blog.AddAsync(blog);
            await _authContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> updateBlog([FromRoute] int id ,Blog blogUpdateRequest)
        {
            var existingPost = await _authContext.Blog.FindAsync(id);
            if(existingPost != null)
            {
                existingPost.title = blogUpdateRequest.title;
                existingPost.content = blogUpdateRequest.content;
                existingPost.summary = blogUpdateRequest.summary;
                existingPost.author = blogUpdateRequest.author;
                existingPost.imageUrl = blogUpdateRequest.imageUrl;
                existingPost.updatedDate = DateTime.Now;

                await _authContext.SaveChangesAsync();
                return Ok(existingPost);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> deleteBlog([FromRoute] int id)
        {
            var existingPost = await _authContext.Blog.FindAsync(id);
            if (existingPost != null)
            { 
                _authContext.Remove(existingPost);
                await _authContext.SaveChangesAsync();
                return Ok(existingPost);
            }
            return NotFound();
        }
    }
}
