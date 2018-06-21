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
        //TODO: Deal with base "object" Body.
        public static async Task<(HttpStatusCode StatusCode, object Body)> PostAsync<T>(string path, object data)
        {
            var requestResult = (await HttpClient.PostAsync($"http://{Hostname}{path}", new StringContent(JsonConvert.SerializeObject(data))));
            return (requestResult.StatusCode, (requestResult.IsSuccessStatusCode
                ? (object)JsonConvert.DeserializeObject<T>(await requestResult.Content.ReadAsStringAsync())
                : null));
        }

        public static async Task<HttpStatusCode> PostAsync(string path, object data)
        {
            return (await HttpClient.PostAsync($"http://{Hostname}{path}", new StringContent((data.GetType() == typeof(string))
                ? (string)data
                : JsonConvert.SerializeObject(data)))).StatusCode;
        }
    }
}
