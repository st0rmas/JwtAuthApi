using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JwtApi.Controllers;

[Route("{controller}")]
public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	}

	[HttpGet("")]
	public IActionResult NonSecuredMethod()
	{
		return Content("Non secured method");
	}

	[HttpGet("admin/secured")]
	[Authorize(Policy = "AdminPolicy")]
	public IActionResult AdminSecuredMethod()
	{
		Console.WriteLine(User.IsInRole("admin"));
		Console.WriteLine(User.IsInRole("ADMIN"));
		Console.WriteLine(User.IsInRole("user"));
		return Content("Admin secured page");
	}
	
	[HttpGet("user/secured")]
	[Authorize(Policy = "UserPolicy")]
	public IActionResult UserSecuredMethod()
	{
		var ageClaim = User.FindFirst(claim => claim.Type == "age");
		int.TryParse(ageClaim.Value, out var age);
		Console.WriteLine(ageClaim.Value + " " + ageClaim.Type + " " + ageClaim.ValueType);
		Console.WriteLine(age);

		return Content("User secured page");
	}
	
	[HttpGet("adult/secured")]
	[Authorize(Policy = "AdultPolicy")]
	public IActionResult AdultSecuredMethod()
	{
		return Content("You are an adult");
	}

}