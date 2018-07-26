using System;

namespace MobileWeather.Core.Settings
{
    public interface IRuntimeContext
    {
        string ApixuBaseEndpoint { get; }
        string DarkskyBaseEndpoint { get; }
        string WeatherbitBaseEndpoint { get; }
        string LocationBaseEndpoint { get; }

        string ApixuKey { get; }
        string DarkskyKey { get; }
        string WeatherbitKey { get; }
        string BingMapKey { get; }

        string CityName { get; set; }
        string SelectedWeatherServices { get; set; }
        bool IsImperial { get; set; }

        string CurrentWeather { get; set; }
        string WeatherForecast { get; set; }
        DateTime UpdateTime { get; set; }
        void RemoveCachedWeather();
    }
}
