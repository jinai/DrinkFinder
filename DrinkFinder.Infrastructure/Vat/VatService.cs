using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DrinkFinder.Infrastructure.Vat
{
    public class VatService : IVatService
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        public VatService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _apiKey = configuration?["ApiKeys:vatlayer"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<VatResponse> Validate(string vatNumber)
        {
            var queryString = $"access_key={_apiKey}&vat_number={vatNumber}";
            var requestUri = new Uri($"validate?{queryString}", UriKind.Relative);
            var response = await _client.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                return new VatResponse { IsValid = false }; // Kind of a hack for now
            }

            var content = await response.Content.ReadAsStringAsync();
            var vatResponse = JsonConvert.DeserializeObject<VatResponse>(content);
            return vatResponse;
        }
    }
}
