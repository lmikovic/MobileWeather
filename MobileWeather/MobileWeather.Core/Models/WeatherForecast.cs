using System.Collections.Generic;

namespace MobileWeather.Core.Models
{
    public class WeatherForecast
    {
        public List<Weather> WeatherList { get; set; }
        public City City { get; set; }
    }
}