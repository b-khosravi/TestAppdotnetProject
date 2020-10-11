using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using TestApp.Authentication;
//using Microsoft.Extensions.caching.memory;

namespace TestApp.Controllers  
{  
    [Route("api/[controller]/[action]")]  
    [ApiController]  
    public class AuthenticateController : ControllerBase  
    {  
        private readonly UserManager<ApplicationUser> _userManager;  
        private readonly RoleManager<ApplicationRole> _roleManager;  
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

  
        public AuthenticateController(UserManager<ApplicationUser> userManager,
         RoleManager<ApplicationRole> roleManager, IConfiguration configuration,AppDbContext context)  
        {  
            
            this._userManager = userManager;  
            this._roleManager = roleManager;  
            _configuration = configuration; 
            _context=context; 
        }  
  
        [HttpPost()]  
        public async Task<IActionResult> Login([FromBody] LoginModel model)  
        {  
            var user = await _userManager.FindByNameAsync(model.UserName);  
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))  
            {  
                var userRoles = await _userManager.GetRolesAsync(user);  
  
                var authClaims = new List<Claim>  
                {  
                    new Claim(ClaimTypes.Name, user.UserName),  
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
                    new Claim("UserId", user.Id),  
                    new Claim("FirstName",user.FirstName)
                };  
  
                foreach (var _userRole in userRoles)  
                {  
                    authClaims.Add(new Claim(ClaimTypes.Role, _userRole));  
                }  
  
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));  
  
                var token = new JwtSecurityToken(  
                    issuer: _configuration["Jwt:Issuer"],  
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(3),  
                    claims: authClaims,  
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
                    );  
                var accessToken =    new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new  
                {  
                    token = accessToken,  
                    expiration = token.ValidTo  
                });  
            }  
            return Unauthorized();  
        }  
  
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)  
        {  
                        var userExists = await _userManager.FindByNameAsync(model.UserName);  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status500InternalServerError,
                 new  { Status = "Error", Message = "User already exists!" });  
  
            ApplicationUser user = new ApplicationUser()  
            {  
                Id=model.Id,
                FirstName=model.FirstName,
                LastName=model.LastName,
                UserName = model.UserName,
                Email = model.EmailAddress,  
                SecurityStamp = Guid.NewGuid().ToString()  
            };  
            var result = await _userManager.CreateAsync(user, model.Password);  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, new   { Status = "Error", Message = "User creation failed! Please check user details and try again." });  
  
            return Ok(new   { Status = "Success", Message = "User created successfully!" });  
        }  

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowClaims()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in User.Claims)
            {
                sb.AppendLine($"ClaimName:{item.Type} = {item.Value}");
            }
            return await Task.FromResult(Ok(sb.ToString()));
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PromoteToAdmin([FromBody] string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user is null)
            return NotFound();
            String adminRoleId = "asd651a61d-asd-as4d6a-asdad4asd";
            var result = await _userManager.AddToRoleAsync(user,adminRoleId);
            if (!result.Succeeded)
                return BadRequest();
            return Ok(new {Status = true, Message= "User promoted to admin"});
        }  
    }  
}  