using System;
using Java.Util.Concurrent;
using LazyStockDiaryMAUI.Extentions;
using LazyStockDiaryMAUI.Helpers;
using LazyStockDiaryMAUI.Models;
using static Android.App.DownloadManager;

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

        public async Task<List<Dividend>> GetSymbolDividend(Symbol symbol,
                                                             DateTime startDate)
        {
            var uri = _restHost.Append("dividend");
            var dividends = await _httpClient.Get<List<Dividend>>(uri, prepareParameters(new Dictionary<string, string>
            {
                { "code", symbol.Code },
                { "exchange", symbol.Exchange },
                { "startDate", startDate.ToString() },
            }));
            return dividends;
        } 

        public async Task<Symbol> GetSymbol(string code, string exchange)
        {
            var uri = _restHost.Append("symbol");
            var symbol = await _httpClient.Get<Symbol>(uri, prepareParameters(new Dictionary<string, string>
            {
                { "code", code },
                { "exchange", exchange },
            }));
            return symbol;
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

        public async Task<Symbol> RegisterSymbol(Symbol data)
        {
            var uri = _restHost.Append("symbol");
            var symbol = await _httpClient.Post<Symbol>(uri, data);
            return symbol;
        }
    }
}

