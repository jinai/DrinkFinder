using Newtonsoft.Json;

namespace DrinkFinder.Infrastructure.Vat
{
    public class VatResponse
    {
        [JsonProperty("valid")]
        public bool IsValid { get; set; }
        [JsonProperty("database")]
        public string Database { get; set; }
        [JsonProperty("format_valid")]
        public bool IsFormatValid { get; set; }
        [JsonProperty("query")]
        public string Query { get; set; }
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        [JsonProperty("vat_number")]
        public string VatNumber { get; set; }
        [JsonProperty("company_name")]
        public string CompanyName { get; set; }
        [JsonProperty("company_address")]
        public string CompanyAddress { get; set; }
    }
}
