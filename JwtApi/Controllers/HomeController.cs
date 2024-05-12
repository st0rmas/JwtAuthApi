using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JwtApi.Controllers;

[Route("/{controller}")]
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

	[HttpGet("secured")]
	[Authorize]
	public IActionResult SecuredMethod()
	{
		return Content("Secured method");
	}

}