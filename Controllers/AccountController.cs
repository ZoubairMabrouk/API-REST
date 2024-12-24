using API_EXAMEN_APP.DTO;
using API_EXAMEN_APP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_EXAMEN_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> AddNewuser(Register userDTO)
        {
            var user = await _userManager.FindByNameAsync(userDTO.UserName);
            if (user != null)
            {
                return BadRequest("User exsiste !!");
            }
            User appUser = new()
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email
            };
            var res = await _userManager.CreateAsync(appUser, userDTO.Password);
            if (res.Succeeded)
            {
                return Ok("Sccussefuly");
            }
            return BadRequest(new {error = res.Errors});


        }
        [HttpPost("login")]
        public async Task<IActionResult> login(Login login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null)
            {
                return Unauthorized(new {error = user.UserName});
            }
            if (await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var claims = new List<Claim>();
                //claims.Add(new Claim("name", "value"));
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti,
               Guid.NewGuid().ToString()));
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                }
                //signingCredentials
                var key = new
               SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));
                var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                claims: claims,
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                expires: DateTime.Now.AddHours(1),
                signingCredentials: sc
                );
                var _token = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    username = login.UserName,
                    role = roles
                };
                return Ok(_token);
            }
            return Unauthorized("wrang credentiels");
        }
        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            // Invalidate the JWT token by instructing the client to discard it
            return Ok(new { message = "Logout successful" });
        }
        [HttpGet("Admin/GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.Select(user => new
            {
                user.Id,
                user.UserName,
                user.Email
            }).ToList();

            return Ok(users);
        }

        // GET api/<AccountController>/5
        [HttpGet("/GetUser/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email
            });
        }
        [HttpPut("Account/UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(string id, Register userDTO)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            user.UserName = userDTO.UserName;
            user.Email = userDTO.Email;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                
                
              
                return Ok("User updated successfully.");
            }
            return BadRequest("Error while updating user.");
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("Admin/DeleteUser/{id}")]
        
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok("User deleted successfully.");
            }
            return BadRequest("Error while deleting user.");
        }
    }
}
