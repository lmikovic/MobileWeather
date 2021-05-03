using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using MobileWeather.Core.Mappers;
using MobileWeather.Core.Models;
using MobileWeather.Core.Models.DTO;
using MobileWeather.Core.Services.Interfaces;
using MobileWeather.Core.Settings;

namespace MobileWeather.Core.Services
{
    public class AmbeeService : IWeatherService
    {
        private readonly IRequestService _requestService;
        private readonly IRuntimeContext _runtimeContext;
        private readonly ILocationService _locationService;
        private readonly bool _isImperial;
        private readonly string _baseEndpoint;
        private readonly string _serviceKey;

        public AmbeeService(IRequestService requestService, IRuntimeContext runtimeContext, ILocationService locationService, string lang = "sr", bool isImperial = false)
        {
            _requestService = requestService;
            _runtimeContext = runtimeContext;
            _locationService = locationService;
            _isImperial = isImperial;
            _baseEndpoint = runtimeContext.GetBaseEndpoint(GetType().Name);
            _serviceKey = runtimeContext.GetKey(GetType().Name);
        }

        public async Task<WeatherForecast> GetForecast(double latitude, double longitude, int days)
        {
            UriBuilder builder = new UriBuilder(_baseEndpoint)
            {
                Path = $"weather/forecast/by-lat-lng",
                Query = $"lat={latitude.ToString(CultureInfo.InvariantCulture)}&lng={longitude.ToString(CultureInfo.InvariantCulture)}&filter=daily"
            };
            var header = new Dictionary<string, string>
            {
                { "x-api-key", _serviceKey }
            };
            AmbeeForecastDTO weatherResponse = await _requestService.GetAsync<AmbeeForecastDTO>(builder.Uri, header);
            AmbeeMapper ambeeMapper = new AmbeeMapper();
            WeatherForecast weatherForecast = ambeeMapper.ToDomainEntities(weatherResponse, _runtimeContext.CityName, _isImperial);
            weatherForecast.WeatherList = weatherForecast.WeatherList.GetRange(0, days);

            return weatherForecast;
        }

        public async Task<WeatherForecast> GetForecast(string city, int days)
        {
            City cityResponse = await _locationService.GetCityByCityName(city);
            WeatherForecast weatherForecast = await GetForecast(cityResponse.Latitude, cityResponse.Longitude, days);
            return weatherForecast;
        }

        public async Task<WeatherData> GetWeather(double latitude, double longitude)
        {
            UriBuilder builder = new UriBuilder(_baseEndpoint)
            {
                Path = $"weather/latest/by-lat-lng",
                Query = $"lat={latitude.ToString(CultureInfo.InvariantCulture)}&lng={longitude.ToString(CultureInfo.InvariantCulture)}"
            };
            var header = new Dictionary<string, string>
            {
                { "x-api-key", _serviceKey }
            };
            AmbeeDTO weatherResponse = await _requestService.GetAsync<AmbeeDTO>(builder.Uri, header);
            AmbeeMapper ambeeMapper = new AmbeeMapper();
            WeatherData weather = ambeeMapper.ToDomainEntity(weatherResponse, _runtimeContext.CityName, _isImperial);

            return weather;
        }

        public async Task<WeatherData> GetWeather(string city)
        {
            City cityResponse = await _locationService.GetCityByCityName(city);
            WeatherData weather = await GetWeather(cityResponse.Latitude, cityResponse.Longitude);
            return weather;
        }
    }
}
