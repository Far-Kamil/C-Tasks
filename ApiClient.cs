using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _6_task
{
    class ApiClient: IDisposable
    {
        private readonly HttpClient _httpClient;
        public ApiClient(string baseURL)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseURL)
            };
        }
        public async Task<string> GetAsync(string endpoint, string Params = "")
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{endpoint}/{Params}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        public void Dispose() 
        {
            _httpClient.Dispose();
        }
    }
}
