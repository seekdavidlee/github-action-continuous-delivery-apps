namespace MyWebApi
{
	public class WeatherForecastService
	{
		private List<WeatherForecast> _weatherForecasts = new List<WeatherForecast>();

		public void Add(WeatherForecast weatherForecast)
		{
			if (_weatherForecasts.Any(x => x.Date == weatherForecast.Date))
			{
				throw new ApplicationException("Duplicate");
			}

			if (weatherForecast.Temp > 200)
			{
				throw new ApplicationException("Out of range!");
			}

			if (weatherForecast.Temp < -200)
			{
				throw new ApplicationException("Out of range!");
			}

			_weatherForecasts.Add(weatherForecast);
		}

		public WeatherForecast? Get(DateTime dateTime)
		{
			return _weatherForecasts.SingleOrDefault(x => x.Date == dateTime);
		}
	}
}
