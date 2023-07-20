using BlogAPI.Context;
using BlogAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly AppDbContext _authContext;

        public UserController(AppDbContext authContext)
        {
            _authContext = authContext;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Models.User userObject)
        {
            if(userObject == null)
            {
                return BadRequest();
            }
            //userObject.password = PasswordHasher.HashPassword(userObject.password);
            var user =  await _authContext.Users
                .FirstOrDefaultAsync(x => x.username== userObject.username || x.password == userObject.password);

            if (user == null)
            {
                return NotFound(new {Message = "User Not Found!"});
            }

            if (!PasswordHasher.VerifyPassword(userObject.password, user.password))
            {
                return BadRequest(new { Message = "Password is Incorrect" }); ;
            }

            return Ok(new 
            {
                Message = "Login Success!"
            });
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] Models.User userObject)
        {
            if(userObject == null)
            {
                return BadRequest();
            }
            if(await CheckusernameExistAsync(userObject.username))
            {
                return BadRequest(new { Message = "username already exist!" });
            }
            if (await CheckEmailExistAsync(userObject.email))
            {
                return BadRequest(new { Message = "Email already exist!" });
            }
            userObject.password = PasswordHasher.HashPassword(userObject.password);
            userObject.role = "User";
            userObject.token = "";
            await _authContext.Users.AddAsync(userObject);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "User Registered!"
            });
        }

        private Task<bool> CheckusernameExistAsync(string username)
            => _authContext.Users.AnyAsync(x=>x.username==username);
        private Task<bool> CheckEmailExistAsync(string email)
            => _authContext.Users.AnyAsync(x => x.email == email);
    }
}
