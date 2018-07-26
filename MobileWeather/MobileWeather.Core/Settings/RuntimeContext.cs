using System;

namespace MobileWeather.Core.Settings
{
    public class RuntimeContext : IRuntimeContext
    {
        public string ApixuBaseEndpoint
        {
            get => AppSettings.ApixuBaseEndpoint;
        }
        public string DarkskyBaseEndpoint
        {
            get => AppSettings.DarkskyBaseEndpoint;
        }
        public string WeatherbitBaseEndpoint
        {
            get => AppSettings.WeatherbitBaseEndpoint;
        }
        public string LocationBaseEndpoint
        {
            get => AppSettings.LocationBaseEndpoint;
        }

        public string ApixuKey
        {
            get => AppSettings.ApixuKey;
        }
        public string DarkskyKey
        {
            get => AppSettings.DarkskyKey;
        }
        public string WeatherbitKey
        {
            get => AppSettings.WeatherbitKey;
        }
        public string BingMapKey
        {
            get => AppSettings.BingMapKey;
        }

        public string CityName
        {
            get => AppSettings.CityName;
            set => AppSettings.CityName = value;
        }

        public string SelectedWeatherServices
        {
            get => AppSettings.SelectedWeatherServices;
            set => AppSettings.SelectedWeatherServices = value;
        }

        public bool IsImperial
        {
            get => AppSettings.IsImperial;
            set => AppSettings.IsImperial = value;
        }

        public string CurrentWeather
        {
            get => AppSettings.CurrentWeather;
            set => AppSettings.CurrentWeather = value;
        }

        public string WeatherForecast
        {
            get => AppSettings.WeatherForecast;
            set => AppSettings.WeatherForecast = value;
        }

        public DateTime UpdateTime
        {
            get => AppSettings.UpdateTime;
            set => AppSettings.UpdateTime = value;
        }

        public void RemoveCachedWeather()
        {
            AppSettings.RemoveCachedWeather();
        }
    }
}
