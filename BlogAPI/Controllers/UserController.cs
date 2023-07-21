using BlogAPI.Context;
using BlogAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.AspNetCore.Authorization;

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
            user.token = createJwtToken(user);
            return Ok(new
            {
                Token = user.token,
                Message = "Login Success!"
            }); ;
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

        private string createJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("secretKeysecretKeysecretKeysecretKey");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.role),
                new Claim(ClaimTypes.Name, $"{user.firstName} {user.lastName}")
            });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }
        [Authorize]
        [HttpGet("getAllUsers")]
        public async Task<ActionResult<User>> getAllUsers()
        {
            return Ok(await _authContext.Users.ToListAsync());
        }
    }
}
