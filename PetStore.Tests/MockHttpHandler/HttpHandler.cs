using System.Net.Http;
using System.Threading.Tasks;

namespace PetStore.Tests.MockHttpHandler
{
    public class HttpClientHandler : IHttpHandler
    {
        public HttpClient _client = new HttpClient();

        public HttpResponseMessage Get(string url)
        {
            return GetAsync(url).Result;
        }

        public HttpResponseMessage Post(string url, HttpContent content)
        {
            return SendAsync(url, content).Result;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> SendAsync(string url, HttpContent content)
        {
            return await _client.PostAsync(url, content);
        }

        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            throw new System.NotImplementedException();
        }
    }
}
