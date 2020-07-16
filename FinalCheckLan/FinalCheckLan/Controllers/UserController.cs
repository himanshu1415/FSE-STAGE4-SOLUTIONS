using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FinalCheckLan.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = model.Email,
                UserName = model.Email
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!await roleManager.RoleExistsAsync("subscriber"))
                await roleManager.CreateAsync(new IdentityRole("subscriber"));

            var result1 = await userManager.AddToRoleAsync(user, "subscriber");

            if (!result1.Succeeded)
                return BadRequest(result1.Errors);

            return Ok();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await userManager.FindByNameAsync(login.UserName);

            if (user != null && await userManager.CheckPasswordAsync(user, login.Password))
            {
                var role = await userManager.GetRolesAsync(user);

                var claims = await userManager.GetClaimsAsync(user);
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, login.UserName));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                claims.Add(new Claim(ClaimTypes.Role, role.First()));

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["jwt:key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var jwt = new JwtSecurityToken(issuer: configuration["jwt:issuer"],
                                               audience: configuration["jwt:issuer"],
                                               claims: claims,
                                               expires: DateTime.Now.AddMinutes(30),
                                               signingCredentials: credentials);

                var handler = new JwtSecurityTokenHandler();
                return Ok(handler.WriteToken(jwt));
            }
            return BadRequest("Invalid username/password");
        }

    }

    public class RegistrationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

