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
	/// Публичный метод, доступный всем пользователям
	/// </summary>
	/// <returns>Авторизован или неавторизован пользователь</returns>
	/// <response code="200">Успешно</response>
	/// <response code="401">Не авторизован</response>
	[ProducesResponseType(typeof(string), 200)]
	[ProducesResponseType(typeof(string), 401)]
	[HttpGet("")]
	public IActionResult NonSecuredMethod()
	{
		var nameClaim = User.Claims.FirstOrDefault(claim=>claim.Type=="name");
		if (nameClaim!=null)
		{
			var name =  nameClaim.Value;
			return Content($"Публичный метод (Пользователь {name} авторизован)");
		}
		return Content($"Публичный метод (пользователь не авторизован)");
	}

	/// <summary>
	/// Метод только для админа
	/// </summary>
	/// <returns></returns>
	/// <response code="200">Успешно</response>
	/// <response code="401">Не авторизован</response>
	[HttpGet("admin/secured")]
	[Authorize(Policy = "AdminPolicy")]
	[ProducesResponseType(typeof(string), 200)]
	public IActionResult AdminSecuredMethod()
	{
		return Content("Защищенная страница админа");
	}
	
	/// <summary>
	/// Метод для обычного пользователя. Доступ есть как у пользователя. так и у админа
	/// </summary>
	/// <returns></returns>
	/// <response code="200">Успешно</response>
	/// <response code="401">Не авторизован</response>
	[HttpGet("user/secured")]
	[Authorize(Policy = "UserPolicy")]
	[ProducesResponseType(typeof(string), 200)]
	public IActionResult UserSecuredMethod()
	{
		return Content("Защищенная страница пользователя");
	}
	
	/// <summary>
	/// Защищенный метод для пользователей старше требуемого возраста. Для доступа у пользователя должна быть claim "age". По умолчанию возраст = 18
	/// </summary>
	/// <returns></returns>
	/// <response code="200">Успешно</response>
	/// <response code="401">Не авторизован</response>
	[HttpGet("adult/secured")]
	[Authorize(Policy = "AdultPolicy")]
	[ProducesResponseType(typeof(string), 200)]
	public IActionResult AdultSecuredMethod()
	{
		return Content("Страница для совершеннолетних");
	}
}
