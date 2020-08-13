using DrinkFinder.Common.Enums;
using DrinkFinder.MvcClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DrinkFinder.MvcClient.Services
{
    public class EstablishmentService : HttpService, IEstablishmentService
    {
        private readonly HttpClient _client;

        public EstablishmentService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<EstablishmentModel>> GetAll()
        {
            var response = await _client.GetAsync("establishments?includes=BusinessHours");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(GenerateExceptionMessage(response));
            }

            return JsonConvert.DeserializeObject<IEnumerable<EstablishmentModel>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<EstablishmentModel> GetById(Guid establishmentId)
        {
            var response = await _client.GetAsync($"establishments/{establishmentId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(GenerateExceptionMessage(response));
            }

            return JsonConvert.DeserializeObject<EstablishmentModel>(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<EstablishmentModel>> GetAllOpenOn(IsoDay day)
        {
            var response = await _client.GetAsync($"establishments?day={day}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(GenerateExceptionMessage(response));
            }

            return JsonConvert.DeserializeObject<IEnumerable<EstablishmentModel>>(await response.Content.ReadAsStringAsync());
        }
    }
}
