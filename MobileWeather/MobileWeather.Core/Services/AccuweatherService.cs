using MobileWeather.Core.Mappers;
using MobileWeather.Core.Models;
using MobileWeather.Core.Models.DTO;
using MobileWeather.Core.Services.Interfaces;
using MobileWeather.Core.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MobileWeather.Core.Services
{
    public class AccuweatherService : IWeatherService
    {
        private readonly IRequestService _requestService;
        private readonly string _lang;
        private readonly bool _isImperial;
        private readonly string _baseEndpoint;
        private readonly string _serviceKey;

        public AccuweatherService(IRequestService requestService, IRuntimeContext runtimeContext, string lang = "sr-RS", bool isImperial = false)
        {
            _requestService = requestService;
            _lang = lang;
            _isImperial = isImperial;
            _baseEndpoint = runtimeContext.GetBaseEndpoint(GetType().Name);
            _serviceKey = runtimeContext.GetKey(GetType().Name);
        }

        public async Task<WeatherForecast> GetForecast(double latitude, double longitude, int days)
        {
            var location = await GetLocationByGeolocationAsync(latitude, longitude);

            UriBuilder builder = new UriBuilder(_baseEndpoint)
            {
                Path = $"forecasts/v1/daily/{NumberOfDays(days)}day/{location.Key}",
                Query = $"apikey={_serviceKey}&language={_lang}&metric={!_isImperial}"
            };

            AccuweatherForecastDTO weatherResponse = await _requestService.GetAsync<AccuweatherForecastDTO>(builder.Uri);
            AccuweatherMapper accuweatherMapper = new AccuweatherMapper();
            WeatherForecast weatherForecast = accuweatherMapper.ToDomainEntities(weatherResponse, location);

            return weatherForecast;
        }

        public async Task<WeatherForecast> GetForecast(string city, int days)
        {
            var location = await GetLocationByNameAsync(city);

            UriBuilder builder = new UriBuilder(_baseEndpoint)
            {
                Path = $"forecasts/v1/daily/{NumberOfDays(days)}day/{location.Key}",
                Query = $"apikey={_serviceKey}&language={_lang}&metric={!_isImperial}"
            };

            AccuweatherForecastDTO weatherResponse = await _requestService.GetAsync<AccuweatherForecastDTO>(builder.Uri);
            AccuweatherMapper accuweatherMapper = new AccuweatherMapper();
            WeatherForecast weatherForecast = accuweatherMapper.ToDomainEntities(weatherResponse, location);

            return weatherForecast;
        }

        public async Task<WeatherData> GetWeather(double latitude, double longitude)
        {
            var location = await GetLocationByGeolocationAsync(latitude, longitude);

            UriBuilder builder = new UriBuilder(_baseEndpoint)
            {
                Path = $"currentconditions/v1/{location.Key}",
                Query = $"apikey={_serviceKey}&language={_lang}&details=true"
            };

            IEnumerable<AccuweatherDTO> weatherResponse = await _requestService.GetAsync<IEnumerable<AccuweatherDTO>>(builder.Uri);
            AccuweatherMapper accuweatherMapper = new AccuweatherMapper();
            WeatherData weather = accuweatherMapper.ToDomainEntity(weatherResponse.First(), location, _isImperial);

            return weather;
        }

        public async Task<WeatherData> GetWeather(string city)
        {
            var location = await GetLocationByNameAsync(city);

            UriBuilder builder = new UriBuilder(_baseEndpoint)
            {
                Path = $"currentconditions/v1/{location.Key}",
                Query = $"apikey={_serviceKey}&language={_lang}&details=true"
            };

            IEnumerable<AccuweatherDTO> weatherResponse = await _requestService.GetAsync<IEnumerable<AccuweatherDTO>>(builder.Uri);
            AccuweatherMapper accuweatherMapper = new AccuweatherMapper();
            WeatherData weather = accuweatherMapper.ToDomainEntity(weatherResponse.First(), location, _isImperial);

            return weather;
        }

        private int NumberOfDays(int days)
        {
            switch (days)
            {
                case int i when (i > 10):
                    return 15;
                case int i when (i > 5):
                    return 10;
                default:
                    return 5;
            }
        }

        private async Task<AccuWeatherLocationDTO> GetLocationByNameAsync(string cityName)
        {
            UriBuilder builder = new UriBuilder(_baseEndpoint)
            {
                Path = $"locations/v1/cities/search",
                Query = $"apikey={_serviceKey}&q={Uri.EscapeDataString(cityName)}"
            };
            var location = await _requestService.GetAsync<IEnumerable<AccuWeatherLocationDTO>>(builder.Uri);
            return location.FirstOrDefault();
        }

        private async Task<AccuWeatherLocationDTO> GetLocationByGeolocationAsync(double latitude, double longitude)
        {
            UriBuilder builder = new UriBuilder(_baseEndpoint)
            {
                Path = $"locations/v1/cities/geoposition/search",
                Query = $"apikey={_serviceKey}&q={latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}"
            };
            var location = await _requestService.GetAsync<IEnumerable<AccuWeatherLocationDTO>>(builder.Uri);
            return location.FirstOrDefault();
        }
    }
}
