using MobileWeather.Core.Models;
using MobileWeather.Core.Models.DTO;

namespace MobileWeather.Core.Mappers
{
    public class LocationMapper
    {
        public City ToDomainEntities(LocationDTO locationDTO)
        {
            var resource = locationDTO.resourceSets[0].resources[0];

            City city = new City()
            {
                Latitude = resource.point.coordinates[0],
                Longitude = resource.point.coordinates[1],
                Name = resource.address.locality
            };

            return city;
        }
    }
}
