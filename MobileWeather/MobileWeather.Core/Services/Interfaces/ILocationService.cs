using MobileWeather.Core.Models;
using System.Threading.Tasks;

namespace MobileWeather.Core.Services.Interfaces
{
    public interface ILocationService
    {
        Task<City> GetCityByCityName(string cityName);
    }
}
