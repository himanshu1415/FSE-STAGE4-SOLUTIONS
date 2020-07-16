using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FinalCheckLan.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : AllowAnonymousAttribute
    {
        [HttpGet]
        public string Get()
        {
            string token = GenerateJSONWebToken(1);
            return token;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            string x = "";

            return x;
        }

        private string GenerateJSONWebToken(int userId)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysuperdupersecret"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>();
            if (userId == 1)
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Admin"),

                    new Claim("UserId", userId.ToString())
                };
            }
            else if (userId == -1)
            {
                claims = new List<Claim>();
            }
            else if (userId != 1 && userId != -1)
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Customer"),

                    new Claim("UserId", userId.ToString())
                };
            }
            var token = new JwtSecurityToken(
            issuer: "mySystem",

            audience: "myUsers",

            claims: claims,

            expires: DateTime.Now.AddMinutes(10),

            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        // GET api/<controller>/5

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
