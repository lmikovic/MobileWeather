using MobileWeather.Core.Models;
using System.Threading.Tasks;

namespace MobileWeather.Core.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherForecast> GetForecast(double latitude, double longitude, int days, bool isImperial);
        Task<WeatherForecast> GetForecast(string city, int days, bool isImperial);
        Task<WeatherData> GetWeather(double latitude, double longitude, bool isImperial);
        Task<WeatherData> GetWeather(string city, bool isImperial);
    }
}
