using MobileWeather.Core.Models;
using System.Threading.Tasks;

namespace MobileWeather.Core.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherForecast> GetForecast(double latitude, double longitude, int days);
        Task<WeatherForecast> GetForecast(string city, int days);
        Task<WeatherData> GetWeather(double latitude, double longitude);
        Task<WeatherData> GetWeather(string city);
    }
}
