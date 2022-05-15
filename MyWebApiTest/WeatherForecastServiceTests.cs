using MyWebApi;

namespace MyWebApiTest
{
	[TestClass]
	public class WeatherForecastServiceTests
	{
		private readonly WeatherForecastService _weatherForecastService = new WeatherForecastService();

		[TestMethod]
		public void AddDuplicate_ResultInException()
		{
			_weatherForecastService.Add(new WeatherForecast { Date = DateTime.Today, Temp = 10 });

			Assert.ThrowsException<ApplicationException>(() =>
			{
				_weatherForecastService.Add(new WeatherForecast { Date = DateTime.Today, Temp = 100 });
			});
		}

		[TestMethod]
		public void AddHighTemp_ResultInApplicationException()
		{
			Assert.ThrowsException<ApplicationException>(() =>
			{
				_weatherForecastService.Add(new WeatherForecast { Date = DateTime.Today, Temp = 201 });
			});
		}

		[TestMethod]
		public void AddLowTemp_ResultInApplicationException()
		{
			Assert.ThrowsException<ApplicationException>(() =>
			{
				_weatherForecastService.Add(new WeatherForecast { Date = DateTime.Today, Temp = -201 });
			});
		}

		[TestMethod]
		public void AddRightTemp_NoException()
		{
			_weatherForecastService.Add(new WeatherForecast { Date = DateTime.Today, Temp = 200 });
		}

		[TestMethod]
		public void AddTempForTwoDiffDates_NoException()
		{
			_weatherForecastService.Add(new WeatherForecast { Date = DateTime.Today.AddDays(-9), Temp = 80 });
			_weatherForecastService.Add(new WeatherForecast { Date = DateTime.Today.AddDays(-8), Temp = 90 });
		}
	}
}