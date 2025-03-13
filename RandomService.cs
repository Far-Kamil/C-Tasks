using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _6_task
{
    class RandomService: IDisposable
    {
        private const string baseURL= "http://www.randomnumberapi.com/";
        private const string pattern = @"\d+";
        private ApiClient client;
        private string endpoint = "api/v1.0/random";
        public RandomService() 
        {
            client=new ApiClient(baseURL);
        }
        public async Task<int> Next(int maxvalue) 
        {
            return await Next(0, maxvalue);
        }
        public async Task<int> Next(int minvalue, int maxvalue)
        {
            var Params = $"?min={minvalue}&max={maxvalue}";
            var response = await client.GetAsync(endpoint, Params);
            var matches = Regex.Match(response, pattern);
            return Int32.Parse(matches.Groups[0].Value);
        }
        public void Dispose() 
        {
            client.Dispose();
        } 
    }
}
