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

        public DarkskyService(IRequestService requestService, ILocationService locationService)
            : this(requestService, locationService, new RuntimeContext())
        { }

        public DarkskyService(IRequestService requestService, ILocationService locationService, IRuntimeContext runtimeContext, string lang = "sr")
        {
            _requestService = requestService;
            _runtimeContext = runtimeContext;
            _locationService = locationService;
            _lang = lang;
        }

        public async Task<WeatherForecast> GetForecast(double latitude, double longitude, int days, bool isImperial)
        {
            string unit = GetUnit(isImperial);

            UriBuilder builder = new UriBuilder(_runtimeContext.DarkskyBaseEndpoint)
            {
                Path = $"forecast/{_runtimeContext.DarkskyKey}/{latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}",
                Query = $"exclude=[currently,minutely,hourly,alerts,flags]&units={unit}&lang={_lang}"
            };

            DarkskyForecastDTO weatherResponse = await _requestService.GetAsync<DarkskyForecastDTO>(builder.Uri);
            DarkskyMapper darkskyMapper = new DarkskyMapper();
            WeatherForecast weatherForecast = darkskyMapper.ToDomainEntities(weatherResponse, _runtimeContext.CityName);

            var indexOfTodaysWeahterItem = weatherForecast.WeatherList.FindIndex(x => x.Date.Date == DateTime.Now.Date);

            weatherForecast.WeatherList = weatherForecast.WeatherList.GetRange(indexOfTodaysWeahterItem, days);

            return weatherForecast;
        }

        public async Task<WeatherForecast> GetForecast(string city, int days, bool isImperial)
        {
            City cityResponse = await _locationService.GetCityByCityName(city);
            WeatherForecast weatherForecast = await GetForecast(cityResponse.Latitude, cityResponse.Longitude, days, isImperial);
            return weatherForecast;
        }

        public async Task<WeatherData> GetWeather(double latitude, double longitude, bool isImperial)
        {
            string unit = GetUnit(isImperial);

            UriBuilder builder = new UriBuilder(_runtimeContext.DarkskyBaseEndpoint)
            {
                Path = $"forecast/{_runtimeContext.DarkskyKey}/{latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}",
                Query = $"exclude=[minutely,hourly,daily,alerts,flags]&units={unit}&lang={_lang}"
            };

            DarkskyDTO weatherResponse = await _requestService.GetAsync<DarkskyDTO>(builder.Uri);
            DarkskyMapper darkskyMapper = new DarkskyMapper();
            WeatherData weather = darkskyMapper.ToDomainEntity(weatherResponse, _runtimeContext.CityName);

            return weather;
        }

        public async Task<WeatherData> GetWeather(string city, bool isImperial)
        {
            City cityResponse = await _locationService.GetCityByCityName(city);
            WeatherData weather = await GetWeather(cityResponse.Latitude, cityResponse.Longitude, isImperial);
            return weather;
        }

        private string GetUnit(bool isImperial)
        {
            return isImperial ? "us" : "si";
        }
    }
}
