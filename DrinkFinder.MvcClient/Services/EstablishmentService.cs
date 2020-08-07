using DrinkFinder.MvcClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DrinkFinder.MvcClient.Services
{
    public class EstablishmentService : IEstablishmentService
    {
        private readonly HttpClient _client;

        public EstablishmentService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<EstablishmentModel>> GetAll()
        {
            var response = await _client.GetAsync("establishments");
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

        private string GenerateExceptionMessage(HttpResponseMessage response)
        {
            return $"{response.RequestMessage.Method} {response.RequestMessage.RequestUri} - {(int)response.StatusCode} {response.ReasonPhrase}";
        }
    }
}
