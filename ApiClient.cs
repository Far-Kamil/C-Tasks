namespace _10_task
{
    public class ApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        public ApiClient(string baseUrl)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
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
