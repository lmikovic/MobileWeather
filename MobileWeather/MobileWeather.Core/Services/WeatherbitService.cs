﻿using System;
using System.Globalization;
using System.Threading.Tasks;
using MobileWeather.Core.Mappers;
using MobileWeather.Core.Models;
using MobileWeather.Core.Models.DTO;
using MobileWeather.Core.Services.Interfaces;
using MobileWeather.Core.Settings;

namespace MobileWeather.Core.Services
{
    public class WeatherbitService : IWeatherService
    {
        private readonly IRequestService _requestService;
        private readonly IRuntimeContext _runtimeContext;
        private readonly string _lang;

        public WeatherbitService(IRequestService requestService)
            : this(requestService, new RuntimeContext())
        { }

        public WeatherbitService(IRequestService requestService, IRuntimeContext runtimeContext, string lang = "sr")
        {
            _requestService = requestService;
            _runtimeContext = runtimeContext;
            _lang = lang;
        }

        public async Task<WeatherForecast> GetForecast(double latitude, double longitude, int days, bool isImperial)
        {
            string unit = GetUnit(isImperial);

            UriBuilder builder = new UriBuilder(_runtimeContext.WeatherbitBaseEndpoint)
            {
                Path = "v2.0/forecast/daily",
                Query = $"lat={latitude.ToString(CultureInfo.InvariantCulture)}&lon={longitude.ToString(CultureInfo.InvariantCulture)}&units={unit}&days={days}&lang={_lang}&key={_runtimeContext.WeatherbitKey}"
            };

            WeatherbitForecastDTO weatherResponse = await _requestService.GetAsync<WeatherbitForecastDTO>(builder.Uri);
            WeatherbitMapper weatherbitMapper = new WeatherbitMapper();
            WeatherForecast weatherForecast = weatherbitMapper.ToDomainEntities(weatherResponse);

            return weatherForecast;
        }

        public async Task<WeatherForecast> GetForecast(string city, int days, bool isImperial)
        {
            string unit = GetUnit(isImperial);

            UriBuilder builder = new UriBuilder(_runtimeContext.WeatherbitBaseEndpoint)
            {
                Path = "v2.0/forecast/daily",
                Query = $"city={city}&units={unit}&days={days}&lang={_lang}&key={_runtimeContext.WeatherbitKey}"
            };

            WeatherbitForecastDTO weatherResponse = await _requestService.GetAsync<WeatherbitForecastDTO>(builder.Uri);
            WeatherbitMapper weatherbitMapper = new WeatherbitMapper();
            WeatherForecast weatherForecast = weatherbitMapper.ToDomainEntities(weatherResponse);

            return weatherForecast;
        }

        public async Task<WeatherData> GetWeather(double latitude, double longitude, bool isImperial)
        {
            string unit = GetUnit(isImperial);

            UriBuilder builder = new UriBuilder(_runtimeContext.WeatherbitBaseEndpoint)
            {
                Path = "v2.0/current",
                Query = $"lat={latitude.ToString(CultureInfo.InvariantCulture)}&lon={longitude.ToString(CultureInfo.InvariantCulture)}&units={unit}&lang={_lang}&key={_runtimeContext.WeatherbitKey}"
            };

            WeatherbitDTO weatherResponse = await _requestService.GetAsync<WeatherbitDTO>(builder.Uri);
            WeatherbitMapper weatherbitMapper = new WeatherbitMapper();
            WeatherData weather = weatherbitMapper.ToDomainEntity(weatherResponse);

            return weather;
        }

        public async Task<WeatherData> GetWeather(string city, bool isImperial)
        {
            string unit = GetUnit(isImperial);

            UriBuilder builder = new UriBuilder(_runtimeContext.WeatherbitBaseEndpoint)
            {
                Path = "v2.0/current",
                Query = $"city={city}&units={unit}&lang={_lang}&key={_runtimeContext.WeatherbitKey}"
            };

            WeatherbitDTO weatherResponse = await _requestService.GetAsync<WeatherbitDTO>(builder.Uri);
            WeatherbitMapper weatherbitMapper = new WeatherbitMapper();
            WeatherData weather = weatherbitMapper.ToDomainEntity(weatherResponse);

            return weather;
        }

        private string GetUnit(bool isImperial)
        {
            return isImperial ? "I" : "M";
        }
    }
}