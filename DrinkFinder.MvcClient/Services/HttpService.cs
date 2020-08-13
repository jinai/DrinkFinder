using System.Net.Http;

namespace DrinkFinder.MvcClient.Services
{
    public abstract class HttpService
    {
        protected string GenerateExceptionMessage(HttpResponseMessage response)
        {
            return $"{response.RequestMessage.Method} {response.RequestMessage.RequestUri} - {(int)response.StatusCode} {response.ReasonPhrase}";
        }
    }
}
