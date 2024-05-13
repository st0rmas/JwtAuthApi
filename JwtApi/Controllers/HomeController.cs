using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JwtApi.Controllers;

[Route("api/v1/home")]
public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	}
	/// <summary>
	/// Not secured method. Returns claim "name" from jwt token. If jwt token is null, returns string unauthorized
	/// </summary>
	/// <returns>authorized or unauthorized</returns>
	/// <response code="200">Success</response>
	/// <response code="401">User is unathorized</response>
	[ProducesResponseType(typeof(string), 200)]
	[ProducesResponseType(typeof(string), 401)]
	[HttpGet("")]
	public IActionResult NonSecuredMethod()
	{
		var nameClaim = User.Claims.FirstOrDefault(claim=>claim.Type=="name");
		Console.WriteLine(nameClaim==null? "yes":"no");
		if (nameClaim!=null)
		{
			var name =  nameClaim.Value;
			return Content($"Non secured method({name} authorized)");
		}
		return Content($"Non secured method (unauthorized)");
	}

	/// <summary>
	/// Get admin secured page. Role admin has an access.
	/// </summary>
	/// <returns></returns>
	/// <response code="200">Success</response>
	/// <response code="401">User is unathorized</response>
	[HttpGet("admin/secured")]
	[Authorize(Policy = "AdminPolicy")]
	[ProducesResponseType(typeof(string), 200)]
	public IActionResult AdminSecuredMethod()
	{
		return Content("Admin secured page");
	}
	
	/// <summary>
	/// Get user secured page. Roles user end admin has an access.
	/// </summary>
	/// <returns></returns>
	/// <response code="200">Success</response>
	/// <response code="401">User is unathorized</response>
	[HttpGet("user/secured")]
	[Authorize(Policy = "UserPolicy")]
	[ProducesResponseType(typeof(string), 200)]
	public IActionResult UserSecuredMethod()
	{
		return Content("User secured page");
	}
	
	/// <summary>
	/// Get page for custom policy with age. Constraint by default = 18. 
	/// </summary>
	/// <returns></returns>
	/// <response code="200">Success</response>
	/// <response code="401">User is unathorized</response>
	[HttpGet("adult/secured")]
	[Authorize(Policy = "AdultPolicy")]
	[ProducesResponseType(typeof(string), 200)]
	public IActionResult AdultSecuredMethod()
	{
		return Content("You are an adult");
	}
}
