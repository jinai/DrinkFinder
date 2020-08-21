using DrinkFinder.MvcClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkFinder.MvcClient.Services
{
    public interface IGeocodingService
    {
        public Task<IEnumerable<MapMarker>> GetMarkers(IEnumerable<EstablishmentModel> establishments);
    }
}
