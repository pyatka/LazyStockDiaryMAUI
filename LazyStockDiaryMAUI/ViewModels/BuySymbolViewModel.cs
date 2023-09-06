using System;
using System.Web;
using LazyStockDiaryMAUI.Models;

namespace LazyStockDiaryMAUI.ViewModels
{
	public class BuySymbolViewModel : BaseViewModel, IQueryAttributable
    {
        public SearchSymbol SearchSymbol { get; set; }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            string dataKey = HttpUtility.UrlDecode(query["dataKey"].ToString());
            SearchSymbol = ((App)Application.Current).DataExchangeServiceManager.Pop(dataKey) as SearchSymbol;
            OnPropertyChanged(nameof(SearchSymbol));
        }
    }
}

