using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.Instruction;
using WebApplication2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
      
        private readonly IJwtSecurity jwtSecurity;

        public AuthController(IJwtSecurity jwtSecurity)
        {
            
            this.jwtSecurity = jwtSecurity;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]LoginModel model)
        {
            if (ModelState.IsValid)
            {
              
                var tokenHandler = new JwtSecurityTokenHandler();
                var userId = 38;
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Expires = DateTime.Now.AddMinutes(5),
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name,userId.ToString())
                    }),
                    SigningCredentials = new SigningCredentials(jwtSecurity.Create(), SecurityAlgorithms.HmacSha256Signature)
                };
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    model.Email,
                    Token = tokenString
                });
            } 
            return BadRequest("Kullanıcı adı veya şifre boş olamaz");
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
