using MobileWeather.Core.Models;
using MobileWeather.Core.Models.DTO;
using System;
using System.Collections.Generic;
using Weather = MobileWeather.Core.Models.Weather;

namespace MobileWeather.Core.Mappers
{
    public class AmbeeMapper
    {
        public WeatherData ToDomainEntity(AmbeeDTO ambeeDTO, string cityName, bool isImperial)
        {
            var input = ambeeDTO.data;
            var weather = new Weather()
            {
                Humidity = input.humidity,
                Pressure = input.pressure,
                TemperatureCurrent = isImperial ? Math.Round(input.temperature) : Math.Round(FahrenheitToCelsius(input.temperature)),
                WindSpeed = input.windSpeed,
                WeatherDescription = "",
                Icon = $""
            };

            var city = ToWeatherCity(ambeeDTO.data.lat, ambeeDTO.data.lng, cityName);

            return new WeatherData { City = city, Weather = weather };
        }

        public WeatherForecast ToDomainEntities(AmbeeForecastDTO ambeeForecastDTO, string cityName, bool isImperial)
        {
            List<Weather> weatherList = new List<Weather>();
            foreach (var forecastWeather in ambeeForecastDTO.data.forecast)
            {
                weatherList.Add(ConvertForecastModel(forecastWeather, isImperial));
            }

            var city = ToWeatherCity(ambeeForecastDTO.data.lat, ambeeForecastDTO.data.lng, cityName);

            return new WeatherForecast { City = city, WeatherList = weatherList };
        }

        private City ToWeatherCity(float lat, float lng, string cityName)
        {
            return new City()
            {
                Name = cityName,
                Latitude = lat,
                Longitude = lng
            };
        }

        private Weather ConvertForecastModel(AmbeeForecast input, bool isImperial)
        {
            DateTime date = DateTimeOffset.FromUnixTimeSeconds(input.time).LocalDateTime;
            return new Weather()
            {
                Humidity = input.humidity,
                TemperatureCurrent = 0,
                TemperatureMax = isImperial ? Math.Round(input.temperatureMax) : Math.Round(FahrenheitToCelsius(input.temperatureMax)),
                TemperatureMin = isImperial ? Math.Round(input.temperatureMin) : Math.Round(FahrenheitToCelsius(input.temperatureMin)),
                WindSpeed = input.windSpeed,
                WeatherDescription = input.summary,
                Icon = $"{input.icon.Replace('-', '_')}.png",
                Date = date
            };
        }

        private float FahrenheitToCelsius(float temp)
        {
            return (temp - 32) * 5 / 9;
        }
    }
}
