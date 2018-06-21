using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Albion.Client
{
    public static class NetworkTools
    {
        public static string Hostname { get; set; } = "localhost";
        public static HttpClient HttpClient = new HttpClient();

        public static async Task<List<T>> GetAsync<T>(string path)
        {
            return JsonConvert.DeserializeObject<List<T>>(await (await HttpClient.GetAsync($"http://{Hostname}{path}")).Content.ReadAsStringAsync());
        }

        public static async Task<(HttpStatusCode StatusCode, T Body)> PostAsync<T>(string path, T data)
        {
            var requestResult = (await HttpClient.PostAsync($"http://{Hostname}{path}", new StringContent(JsonConvert.SerializeObject(data))));
            return (requestResult.StatusCode, JsonConvert.DeserializeObject<T>(await requestResult.Content.ReadAsStringAsync()));
        }
    }
}
