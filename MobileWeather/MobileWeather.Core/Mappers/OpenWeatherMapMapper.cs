using MobileWeather.Core.Models;
using MobileWeather.Core.Models.DTO;
using System;
using System.Collections.Generic;
using Weather = MobileWeather.Core.Models.Weather;

namespace MobileWeather.Core.Mappers
{
    public class OpenWeatherMapMapper
    {
        public WeatherData ToDomainEntity(OpenWeatherMapDTO openWeatherMapDTO, string cityName)
        {
            var input = openWeatherMapDTO.current;
            var weather = new Weather()
            {
                Humidity = input.humidity,
                Pressure = input.pressure,
                TemperatureCurrent = Math.Round(input.temp),
                WindSpeed = input.wind_speed,
                WeatherDescription = input.weather[0].description,
                Icon = $"openweathermap{input.weather[0].icon}.png"
            };

            var city = new City
            {
                Name = cityName,
                Latitude = openWeatherMapDTO.lat,
                Longitude = openWeatherMapDTO.lon
            };

            return new WeatherData { City = city, Weather = weather };
        }

        public WeatherForecast ToDomainEntities(OpenWeatherMapForecastDTO openWeatherMapForecastDTO, string cityName)
        {
            List<Weather> weatherList = new List<Weather>();
            foreach (var forecastWeather in openWeatherMapForecastDTO.daily)
            {
                weatherList.Add(ConvertForecastModel(forecastWeather));
            }

            var city = new City
            {
                Name = cityName,
                Latitude = openWeatherMapForecastDTO.lat,
                Longitude = openWeatherMapForecastDTO.lon
            };

            return new WeatherForecast { City = city, WeatherList = weatherList };
        }

        private Weather ConvertForecastModel(OpenWeatherMapDaily input)
        {
            DateTime date = DateTimeOffset.FromUnixTimeSeconds(input.dt).LocalDateTime;
            return new Weather()
            {
                Humidity = input.humidity,
                TemperatureCurrent = 0,
                TemperatureMax = Math.Round(input.temp.max),
                TemperatureMin = Math.Round(input.temp.min),
                WindSpeed = input.wind_speed,
                WeatherDescription = input.weather[0].description,
                Icon = $"openweathermap{input.weather[0].icon}.png",
                Date = date
            };
        }
    }
}
