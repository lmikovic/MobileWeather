using MobileWeather.Core.Mappers;
using MobileWeather.Core.Models;
using MobileWeather.Core.Models.DTO;
using MobileWeather.Core.Services.Interfaces;
using MobileWeather.Core.Settings;
using System;
using System.Threading.Tasks;

namespace MobileWeather.Core.Services
{
    public class LocationService : ILocationService
    {
        private readonly IRequestService _requestService;
        private readonly IRuntimeContext _runtimeContext;

        public LocationService(IRequestService requestService)
            : this(requestService, new RuntimeContext())
        { }

        public LocationService(IRequestService requestService, IRuntimeContext runtimeContext)
        {
            _requestService = requestService;
            _runtimeContext = runtimeContext;
        }

        public async Task<City> GetCityByCityName(string cityName)
        {
            UriBuilder builder = new UriBuilder(_runtimeContext.LocationBaseEndpoint)
            {
                Path = "REST/v1/Locations",
                Query = $"locality={Uri.EscapeDataString(cityName)}&key={_runtimeContext.BingMapKey}"
            };

            LocationDTO locationResponse = await _requestService.GetAsync<LocationDTO>(builder.Uri);
            LocationMapper locationMapper = new LocationMapper();
            City city = locationMapper.ToDomainEntities(locationResponse);

            return city;
        }
    }
}
