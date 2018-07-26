using System;

namespace MobileWeather.Core.Models
{
    public class Weather
    {
        public double TemperatureCurrent { get; set; }
        public double TemperatureMax { get; set; }
        public double TemperatureMin { get; set; }
        public float Pressure { get; set; }
        public float Humidity { get; set; }
        public float WindSpeed { get; set; }
        public string WeatherDescription { get; set; }
        public string Icon { get; set; }
        public DateTime Date { get; set; }
    }
}