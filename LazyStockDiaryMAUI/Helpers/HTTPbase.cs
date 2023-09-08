using System;
using System.Text;
using Newtonsoft.Json;

namespace LazyStockDiaryMAUI.Helpers
{
	public class HTTPbase
	{
        private HttpClient client;

        private string prepareGetUrl(Uri uri, Dictionary<string, string> parameters)
        {
            string url = uri.AbsoluteUri;
            url = string.Format("{0}?{1}", url,
                string.Join("&",
                parameters.Select(kvp =>
                    string.Format("{0}={1}", kvp.Key, kvp.Value))));
            return url;
        }

        public HTTPbase()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
        }

        public async Task<T> Get<T>(Uri uri, Dictionary<string, string>? parameters = null)
        {
            string url = prepareGetUrl(uri, parameters);
            var jsonText = await client.GetStringAsync(url);
            var o = JsonConvert.DeserializeObject<T>(jsonText);
            return o;
        }

        public async Task<T> Post<T>(Uri uri, object? parameter = null)
        {
            string serializedData = JsonConvert.SerializeObject(parameter);
            var content = new StringContent(serializedData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri.AbsoluteUri, content);
            var responseData = await response.Content.ReadAsStringAsync();
            var o = JsonConvert.DeserializeObject<T>(responseData);
            return o;
        }
    }
}

