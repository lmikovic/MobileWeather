using MobileWeather.Core.Mappers;
using MobileWeather.Core.Models;
using MobileWeather.Core.Models.DTO;
using MobileWeather.Core.Services.Interfaces;
using MobileWeather.Core.Settings;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace MobileWeather.Core.Services
{
    public class DarkskyService : IWeatherService
    {
        private readonly IRequestService _requestService;
        private readonly IRuntimeContext _runtimeContext;
        private readonly ILocationService _locationService;
        private readonly string _lang;
        private readonly string _unit;
        private readonly string _baseEndpoint;
        private readonly string _serviceKey;

        public DarkskyService(IRequestService requestService, ILocationService locationService, IRuntimeContext runtimeContext, string lang = "sr", bool isImperial = false)
        {
            _requestService = requestService;
            _runtimeContext = runtimeContext;
            _locationService = locationService;
            _lang = lang;
            _unit = GetUnit(isImperial);
            _baseEndpoint = runtimeContext.GetBaseEndpoint(GetType().Name);
            _serviceKey = runtimeContext.GetKey(GetType().Name);
        }

        public async Task<WeatherForecast> GetForecast(double latitude, double longitude, int days)
        {
            UriBuilder builder = new UriBuilder(_baseEndpoint)
            {
                Path = $"forecast/{_serviceKey}/{latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}",
                Query = $"exclude=[currently,minutely,hourly,alerts,flags]&units={_unit}&lang={_lang}"
            };

            DarkskyForecastDTO weatherResponse = await _requestService.GetAsync<DarkskyForecastDTO>(builder.Uri);
            DarkskyMapper darkskyMapper = new DarkskyMapper();
            WeatherForecast weatherForecast = darkskyMapper.ToDomainEntities(weatherResponse, _runtimeContext.CityName);

            var indexOfTodaysWeahterItem = weatherForecast.WeatherList.FindIndex(x => x.Date.Date == DateTime.Now.Date);

            weatherForecast.WeatherList = weatherForecast.WeatherList.GetRange(indexOfTodaysWeahterItem, days);

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
                Path = $"forecast/{_serviceKey}/{latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}",
                Query = $"exclude=[minutely,hourly,daily,alerts,flags]&units={_unit}&lang={_lang}"
            };

            DarkskyDTO weatherResponse = await _requestService.GetAsync<DarkskyDTO>(builder.Uri);
            DarkskyMapper darkskyMapper = new DarkskyMapper();
            WeatherData weather = darkskyMapper.ToDomainEntity(weatherResponse, _runtimeContext.CityName);

            return weather;
        }

        public async Task<WeatherData> GetWeather(string city)
        {
            City cityResponse = await _locationService.GetCityByCityName(city);
            WeatherData weather = await GetWeather(cityResponse.Latitude, cityResponse.Longitude);
            return weather;
        }

        private string GetUnit(bool isImperial)
        {
            return isImperial ? "us" : "si";
        }
    }
}
