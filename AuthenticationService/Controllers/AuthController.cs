using AuthenticationService.DataBase;
using AuthenticationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthentiCationDbContext authentiCationDbContext;
        private readonly IConfiguration config;
        public AuthController(AuthentiCationDbContext _authentiCationDbContext,IConfiguration _config)
        {
            authentiCationDbContext = _authentiCationDbContext;
            config = _config;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await authentiCationDbContext.Users.Select(x=>new {
                Id=x.UserId,
                Name=x.Name,
                UserName=x.Username
            }).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> ValidateUserAsync([FromBody] LoginModel loginModel)
        {
            try
            {
                UserModel userModel = await (from user in authentiCationDbContext.Users
                                       join userrole in authentiCationDbContext.UserRoles
                                       on user.UserId equals userrole.UserId
                                       where user.Username == loginModel.Username
                                       && user.Password == loginModel.Password
                                       select new UserModel
                                       {
                                           UserId = user.UserId,
                                           Username = user.Username,
                                           Name = user.Name,
                                           Roles = authentiCationDbContext.Roles.Where(r => r.RoleId == userrole.RoleId).Select(r => r.Name).ToArray()
                                       }).FirstOrDefaultAsync();
                if(userModel is not null)
                {
                    //generate token
                    userModel.Token = GenerateWebToken(userModel);
                    return Ok(userModel);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private string GenerateWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //For JWT Cliams help : https://tools.ietf.org/html/rfc7519#section-5
            var claims = new List<Claim> {
                             new Claim(JwtRegisteredClaimNames.Sub, userInfo.Name),
                             new Claim(JwtRegisteredClaimNames.Email, userInfo.Username),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                             
                             new Claim("CreatedDate", DateTime.Now.ToString()),
                             };
            foreach(var role in userInfo.Roles)
            {
                claims.Add(new Claim("Roles", role));//claim for authorization )
            }

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                                            config["Jwt:Audience"],
                                            claims,
                                            expires: DateTime.Now.AddMinutes(120),
                                            signingCredentials: credentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedJwt;
        }
    }
}
