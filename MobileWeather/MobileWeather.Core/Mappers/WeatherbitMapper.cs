using MobileWeather.Core.Models;
using MobileWeather.Core.Models.DTO;
using System;
using System.Collections.Generic;
using Weather = MobileWeather.Core.Models.Weather;

namespace MobileWeather.Core.Mappers
{
    public class WeatherbitMapper
    {
        public WeatherData ToDomainEntity(WeatherbitDTO weatherbitDTO)
        {
            var input = weatherbitDTO.data[0];

            Weather weather = ToWeather(input);

            var city = ToWeatherCity(input);

            return new WeatherData { City = city, Weather = weather };
        }

        public WeatherForecast ToDomainEntities(WeatherbitForecastDTO weatherbitForecastDTO)
        {
            List<Weather> weatherList = new List<Weather>();
            foreach (var forecastWeather in weatherbitForecastDTO.data)
            {
                weatherList.Add(ToWeather(forecastWeather));
            }

            var city = ToWeatherCity(weatherbitForecastDTO);

            return new WeatherForecast { City = city, WeatherList = weatherList };
        }

        private City ToWeatherCity(IWeatherbitCity city)
        {
            return new City()
            {
                Name = city.city_name,
                Latitude = city.lat,
                Longitude = city.lon
            };
        }

        private Weather ToWeather(WeatherbitData input)
        {
            DateTime date = DateTime.MinValue;
            DateTime.TryParse(input.datetime, out date);
            var weather = new Weather()
            {
                Pressure = input.pres,
                TemperatureCurrent = Math.Round(input.temp),
                TemperatureMax = Math.Round(input.max_temp),
                TemperatureMin = Math.Round(input.min_temp),
                WindSpeed = input.wind_spd,
                WeatherDescription = input.weather.description,
                Icon = $"{input.weather.icon}.png",
                Date = date
            };
            return weather;
        }
    }
}
