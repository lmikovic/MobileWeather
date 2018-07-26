using MobileWeather.Core.Models;
using MobileWeather.Core.Models.DTO;
using System;
using System.Collections.Generic;
using Weather = MobileWeather.Core.Models.Weather;

namespace MobileWeather.Core.Mappers
{
    public class ApixuMapper
    {
        public WeatherData ToDomainEntity(ApixuDTO apixuDTO, bool isImperial)
        {
            var input = apixuDTO.current;
            var weather = new Weather()
            {
                Humidity = input.humidity,
                Pressure = isImperial ? input.pressure_in : input.pressure_mb,
                TemperatureCurrent = isImperial ? Math.Round(input.temp_f) : Math.Round(input.temp_c),
                WindSpeed = isImperial ? input.wind_mph : input.wind_kph,
                WeatherDescription = input.condition.text,
                Icon = GetIcon(input.condition.icon)
            };

            var city = ToWeatherCity(apixuDTO.location);

            return new WeatherData { City = city, Weather = weather };
        }

        private string GetIcon(string iconPath)
        {
            var paths = iconPath.Split('/');

            var partOfTheDay = paths[paths.Length - 2];
            var icon = paths[paths.Length -1];

            return $"{partOfTheDay}_{icon}";
        }

        public WeatherForecast ToDomainEntities(ApixuForecastDTO apixuForecastDTO, bool isImperial)
        {
            List<Weather> weatherList = new List<Weather>();
            foreach (var forecastWeather in apixuForecastDTO.forecast.forecastday)
            {
                weatherList.Add(ConvertForecastModel(forecastWeather, isImperial));
            }

            var city = ToWeatherCity(apixuForecastDTO.location);

            return new WeatherForecast { City = city, WeatherList = weatherList };
        }

        private City ToWeatherCity(Location city)
        {
            return new City()
            {
                Name = city.name,
                Latitude = city.lat,
                Longitude = city.lon
            };
        }

        private Weather ConvertForecastModel(Forecastday input, bool isImperial)
        {
            DateTime date = DateTime.MinValue;
            DateTime.TryParse(input.date, out date);
            return new Weather()
            {
                Humidity = input.day.avghumidity,
                TemperatureCurrent = isImperial ? Math.Round(input.day.avgtemp_f) : Math.Round(input.day.avgtemp_c),
                TemperatureMax = isImperial ? Math.Round(input.day.maxtemp_f) : Math.Round(input.day.maxtemp_c),
                TemperatureMin = isImperial ? Math.Round(input.day.mintemp_f) : Math.Round(input.day.mintemp_c),
                WindSpeed = isImperial ? input.day.maxwind_mph : input.day.maxwind_kph,
                WeatherDescription = input.day.condition.text,
                Icon = GetIcon(input.day.condition.icon),
                Date = date
            };
        }
    }
}
