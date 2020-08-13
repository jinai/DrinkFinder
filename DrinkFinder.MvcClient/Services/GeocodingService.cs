using DrinkFinder.Common.Extensions;
using DrinkFinder.MvcClient.Models;
using Geocoding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkFinder.MvcClient.Services
{
    public class GeocodingService : HttpService, IGeocodingService
    {
        private readonly IGeocoder _geocoder;

        public GeocodingService(IGeocoder geocoder)
        {
            _geocoder = geocoder;
        }

        public async Task<IEnumerable<MapMarker>> GetMarkers(IEnumerable<EstablishmentModel> establishments)
        {
            IList<MapMarker> markers = new List<MapMarker>();

            foreach (var establishment in establishments)
            {
                var inputAddress = establishment.Address.GetFormatted();
                var addresses = await _geocoder.GeocodeAsync(inputAddress);
                var result = addresses.First();

                var marker = new MapMarker
                {
                    Latitude = result.Coordinates.Latitude,
                    Longitude = result.Coordinates.Longitude,
                    ShortCode = establishment.ShortCode,
                    Name = establishment.Name,
                    Description = establishment.Description,
                    Type = establishment.Type,
                    Logo = establishment.Logo,
                    FormattedAddress = result.FormattedAddress,
                    BusinessHours = establishment.BusinessHours,
                };

                markers.Add(marker);
            }

            return markers;
        }
    }
}
