using ApiDayOne.DTOs;
using ApiDayOne.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiDayOne.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly IConfiguration config;
		public AccountController(UserManager<ApplicationUser> userManager, IConfiguration config)
		{
			this.userManager = userManager;
			this.config = config;
		}
		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterDTO model)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser user = new ApplicationUser();
				user.Email = model.Email;
				user.UserName = model.Username;
				var result = await userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					return Ok("registered successfully");
				}
				else
					return BadRequest(result.Errors.ToList());
			}
			return BadRequest();
		}
		[HttpPost("Login")]
		public async Task<IActionResult> Login(LoginDTO model)
		{
			if(ModelState.IsValid)
			{
				ApplicationUser user = await userManager.FindByNameAsync(model.Username);
				if(user != null)
				{
					bool isFound = await userManager.CheckPasswordAsync(user , model.Password);
					
					var myclaims = new List<Claim>();
					myclaims.Add(new Claim(ClaimTypes.Name , user.UserName));
					myclaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
					myclaims.Add(new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()));
					
					var roles = await userManager.GetRolesAsync(user);
					foreach (var role in roles)
					{
						myclaims.Add(new Claim(ClaimTypes.Role, role));
					}

					SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_keyyyy_123_tokennnnnnnnnnnnnn_keyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy"));
					SigningCredentials credentials = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);
					if (isFound)
					{
						JwtSecurityToken token = new JwtSecurityToken(
							issuer: "https://localhost:7185/",
							audience: "http://localhost:5050/",
							claims: myclaims,
							expires: DateTime.Now.AddDays(14),
							signingCredentials: credentials
							);
						return Ok(new {msg = "valid user", Token = new JwtSecurityTokenHandler().WriteToken(token) ,expiration = token.ValidTo});
					}
				}
			}
			return Unauthorized();
		}

		private object JwtSecurityTokenHandler()
		{
			throw new NotImplementedException();
		}
	}
}
