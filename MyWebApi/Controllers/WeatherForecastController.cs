using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static WeatherForecastService _weatherForecastService = new WeatherForecastService();
		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[HttpPut(Name = "AddWeatherForecast")]
		public IActionResult Add(WeatherForecast weatherForecast)
		{
			try
			{
				_weatherForecastService.Add(weatherForecast);
			}
			catch (ApplicationException e)
			{
				return BadRequest(e.Message);
			}

			return Ok();
		}

		[HttpGet("{date}", Name = "GetWeatherForecast")]
		public IActionResult Get(DateTime date)
		{
			var result = _weatherForecastService.Get(date);

			if (result == null) return NotFound();

			return Ok(result);
		}
	}
}