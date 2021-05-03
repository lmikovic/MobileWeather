using System;
using System.Collections.Generic;

namespace MobileWeather.Core.Settings
{
    public interface IRuntimeContext
    {
        string GetBaseEndpoint(string serviceName);
        string GetKey(string serviceName);
        IEnumerable<string> GetServices();

        string LocationBaseEndpoint { get; }

        string BingMapKey { get; }

        string CityName { get; set; }
        string SelectedWeatherServices { get; set; }
        bool IsImperial { get; set; }

        string CurrentWeather { get; set; }
        string WeatherForecast { get; set; }
        string UpdateTime { get; set; }
        void RemoveCachedWeather();
    }
}
