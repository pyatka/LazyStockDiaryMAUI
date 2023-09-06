using System;
using LazyStockDiaryMAUI.Extentions;
using LazyStockDiaryMAUI.Helpers;
using LazyStockDiaryMAUI.Models;

namespace LazyStockDiaryMAUI.Services
{
	public class RestService
	{
        private HTTPbase _httpClient;
        private Uri _restHost;

        private Dictionary<string, string> prepareParameters(Dictionary<string, string>? keyValues = null)
        {
            if (keyValues == null)
            {
                keyValues = new Dictionary<string, string>();
            }
            var parameters = keyValues;
            return parameters;
        }

        public RestService()
		{
            _httpClient = new HTTPbase();
#if DEBUG
            _restHost = new Uri("http://10.0.2.2:5181/");
#else
            restHost = new Uri("");
#endif
        }

        public async Task<List<SearchSymbol>> Search(string query)
        {
            var uri = _restHost.Append("search");
            var symbols = await _httpClient.Get<List<SearchSymbol>>(uri, prepareParameters(new Dictionary<string, string>
            {
                { "query", query.ToLower() },
            }));
            return symbols;
        }
    }
}

