using System;
using System.Globalization;
using System.Threading.Tasks;
using MobileWeather.Core.Mappers;
using MobileWeather.Core.Models;
using MobileWeather.Core.Models.DTO;
using MobileWeather.Core.Services.Interfaces;
using MobileWeather.Core.Settings;

namespace MobileWeather.Core.Services
{
    public class ApixuService : IWeatherService
    {
        private readonly IRequestService _requestService;
        private readonly IRuntimeContext _runtimeContext;
        private readonly string _lang;
        private readonly bool _isImperial;

        public ApixuService(IRequestService requestService, IRuntimeContext runtimeContext, string lang = "sr", bool isImperial = false)
        {
            _requestService = requestService;
            _runtimeContext = runtimeContext;
            _lang = lang;
            _isImperial = isImperial;
        }

        public async Task<WeatherForecast> GetForecast(double latitude, double longitude, int days)
        {
            UriBuilder builder = new UriBuilder(_runtimeContext.ApixuBaseEndpoint)
            {
                Path = $"v1/forecast.json",
                Query = $"key={_runtimeContext.ApixuKey}&q={latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}&lang={_lang}&days={days}"
            };

            ApixuForecastDTO weatherResponse = await _requestService.GetAsync<ApixuForecastDTO>(builder.Uri);
            ApixuMapper apixuMapper = new ApixuMapper();
            WeatherForecast weatherForecast = apixuMapper.ToDomainEntities(weatherResponse, _isImperial);

            return weatherForecast;
        }

        public async Task<WeatherForecast> GetForecast(string city, int days)
        {
            UriBuilder builder = new UriBuilder(_runtimeContext.ApixuBaseEndpoint)
            {
                Path = $"v1/forecast.json",
                Query = $"key={_runtimeContext.ApixuKey}&q={city}&lang={_lang}&days={days}"
            };

            ApixuForecastDTO weatherResponse = await _requestService.GetAsync<ApixuForecastDTO>(builder.Uri);
            ApixuMapper apixuMapper = new ApixuMapper();
            WeatherForecast weatherForecast = apixuMapper.ToDomainEntities(weatherResponse, _isImperial);

            return weatherForecast;
        }

        public async Task<WeatherData> GetWeather(double latitude, double longitude)
        {
            UriBuilder builder = new UriBuilder(_runtimeContext.ApixuBaseEndpoint)
            {
                Path = $"v1/current.json",
                Query = $"key={_runtimeContext.ApixuKey}&q={latitude.ToString(CultureInfo.InvariantCulture)},{longitude.ToString(CultureInfo.InvariantCulture)}&lang={_lang}"
            };

            ApixuDTO weatherResponse = await _requestService.GetAsync<ApixuDTO>(builder.Uri);
            ApixuMapper apixuMapper = new ApixuMapper();
            WeatherData weather = apixuMapper.ToDomainEntity(weatherResponse, _isImperial);

            return weather;
        }

        public async Task<WeatherData> GetWeather(string city)
        {
            UriBuilder builder = new UriBuilder(_runtimeContext.ApixuBaseEndpoint)
            {
                Path = $"v1/current.json",
                Query = $"key={_runtimeContext.ApixuKey}&q={city}&lang={_lang}"
            };

            ApixuDTO weatherResponse = await _requestService.GetAsync<ApixuDTO>(builder.Uri);
            ApixuMapper apixuMapper = new ApixuMapper();
            WeatherData weather = apixuMapper.ToDomainEntity(weatherResponse, _isImperial);

            return weather;
        }
    }
}
