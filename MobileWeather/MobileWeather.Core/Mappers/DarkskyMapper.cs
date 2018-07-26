using MobileWeather.Core.Models;
using MobileWeather.Core.Models.DTO;
using System;
using System.Collections.Generic;
using Weather = MobileWeather.Core.Models.Weather;

namespace MobileWeather.Core.Mappers
{
    public class DarkskyMapper
    {
        public WeatherData ToDomainEntity(DarkskyDTO darkskyDTO, string cityName)
        {
            var input = darkskyDTO.currently;
            var weather = new Weather()
            {
                Humidity = input.humidity,
                Pressure = input.pressure,
                TemperatureCurrent = Math.Round(input.temperature),
                WindSpeed = input.windSpeed,
                WeatherDescription = input.summary,
                Icon = $"{input.icon.Replace('-', '_')}.png"
            };

            var city = ToWeatherCity(darkskyDTO, cityName);

            return new WeatherData { City = city, Weather = weather };
        }

        public WeatherForecast ToDomainEntities(DarkskyForecastDTO darkskyForecastDTO, string cityName)
        {
            List<Weather> weatherList = new List<Weather>();
            foreach (var forecastWeather in darkskyForecastDTO.daily.data)
            {
                weatherList.Add(ConvertForecastModel(forecastWeather));
            }

            var city = ToWeatherCity(darkskyForecastDTO, cityName);

            return new WeatherForecast { City = city, WeatherList = weatherList };
        }

        private City ToWeatherCity(IDarkskyCity city, string cityName)
        {
            return new City()
            {
                Name = cityName,
                Latitude = city.latitude,
                Longitude = city.longitude
            };
        }

        private Weather ConvertForecastModel(Datum input)
        {
            DateTime date = DateTimeOffset.FromUnixTimeSeconds(input.time).LocalDateTime;
            return new Weather()
            {
                Humidity = input.humidity,
                TemperatureCurrent = 0,
                TemperatureMax = Math.Round(input.temperatureMax),
                TemperatureMin = Math.Round(input.temperatureMin),
                WindSpeed = input.windSpeed,
                WeatherDescription = input.summary,
                Icon = $"{input.icon.Replace('-', '_')}.png",
                Date = date
            };
        }
    }
}
