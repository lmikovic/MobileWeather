using MobileWeather.Core.Models;
using MobileWeather.Core.Models.DTO;
using System;
using System.Collections.Generic;
using Weather = MobileWeather.Core.Models.Weather;

namespace MobileWeather.Core.Mappers
{
    public class AccuweatherMapper
    {
        public WeatherData ToDomainEntity(AccuweatherDTO input, AccuWeatherLocationDTO location, bool isImperial)
        {
            var weather = new Weather()
            {
                Humidity = input.RelativeHumidity,
                Pressure = isImperial ? input.Pressure.Imperial.Value : input.Pressure.Metric.Value,
                TemperatureCurrent = Math.Round(isImperial ? input.Temperature.Imperial.Value : input.Temperature.Metric.Value),
                WindSpeed = isImperial ? input.Wind.Speed.Imperial.Value : input.Wind.Speed.Metric.Value,
                WeatherDescription = input.WeatherText,
                Icon = $"accuweather{input.WeatherIcon}.png"
            };

            var city = ToWeatherCity(location);

            return new WeatherData { City = city, Weather = weather };
        }

        public WeatherForecast ToDomainEntities(AccuweatherForecastDTO accuweatherForecastDTO, AccuWeatherLocationDTO location)
        {
            List<Weather> weatherList = new List<Weather>();
            foreach (var forecastWeather in accuweatherForecastDTO.DailyForecasts)
            {
                weatherList.Add(ConvertForecastModel(forecastWeather));
            }

            var city = ToWeatherCity(location);

            return new WeatherForecast { City = city, WeatherList = weatherList };
        }

        private City ToWeatherCity(AccuWeatherLocationDTO location)
        {
            return new City()
            {
                Name = location.EnglishName,
                Latitude = location.GeoPosition.Latitude,
                Longitude = location.GeoPosition.Longitude
            };
        }

        private Weather ConvertForecastModel(Dailyforecast input)
        {
            DateTime date = DateTimeOffset.FromUnixTimeSeconds(input.EpochDate).LocalDateTime;
            return new Weather()
            {
                Humidity = 0,
                TemperatureCurrent = 0,
                TemperatureMax = Math.Round(input.Temperature.Maximum.Value),
                TemperatureMin = Math.Round(input.Temperature.Minimum.Value),
                WindSpeed = 0,
                WeatherDescription = "",
                Icon = $"accuweather{input.Day.Icon}.png",
                Date = date
            };
        }
    }
}
