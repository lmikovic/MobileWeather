using MobileWeather.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileWeather.Core.Settings
{
    public class RuntimeContext : IRuntimeContext
    {
        public string LocationBaseEndpoint
        {
            get => AppSettings.LocationBaseEndpoint;
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

        public string UpdateTime
        {
            get => AppSettings.UpdateTime;
            set => AppSettings.UpdateTime = value;
        }

        public void RemoveCachedWeather()
        {
            AppSettings.RemoveCachedWeather();
        }

        public IEnumerable<string> GetServices()
        {
            var type = typeof(IWeatherService);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.FullName.Contains("Hidden") && !assembly.FullName.Contains("Private"))
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass);

            return types.Select(x => x.Name.Replace("Service", ""));
        }

        public string GetBaseEndpoint(string serviceName)
        {
            return GetValueByPostfix(serviceName, "BaseEndpoint");
        }

        public string GetKey(string serviceName)
        {
            return GetValueByPostfix(serviceName, "Key");
        }

        private string GetValueByPostfix(string serviceName, string postfix)
        {
            var type = typeof(AppSettings);
            var name = $"{serviceName.Replace("Service", "")}{postfix}";
            return type.GetProperty(name).GetValue(null).ToString();
        }
    }
}
