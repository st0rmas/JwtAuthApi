using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtApi.Controllers;
[Route("api/v1/auth")]
public class AuthController : Controller
{
	/// <summary>
	/// Authentication method. Returns jwt token for user data from post query
	/// </summary>
	/// <remarks>
	/// Sample request:
	/// POST /api/v1/login
	///{
	///		"login": "your_login",
	///		"password: "your_password"
	/// }
	/// </remarks>
	/// <param name="loginDto"></param>
	/// <returns></returns>
	[HttpPost("login")]
	public IActionResult login([FromBody] LoginDto loginDto)
	{
		var user = Consts.users.FirstOrDefault(user => user.Login == loginDto.Login);
		if (user==null)
		{
			return BadRequest();
		}
		var claims = new List<Claim>()
		{
			new Claim(ClaimTypes.Name, user.Login),
			new Claim(ClaimTypes.Role, user.Role.ToString()),
			new Claim("age", user.Age.ToString())
		};
		var jwt = new JwtSecurityToken(
			issuer: "",
			audience:"",
			claims: claims,
			expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
			signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Consts.SECRET_KEY)), SecurityAlgorithms.HmacSha256)
		);
		return Json(new JwtSecurityTokenHandler().WriteToken(jwt));
	}
}
