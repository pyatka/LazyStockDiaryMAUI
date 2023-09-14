using System;
using System.Web;
using System.Windows.Input;
using LazyStockDiaryMAUI.Commands;
using LazyStockDiaryMAUI.Models;

namespace LazyStockDiaryMAUI.ViewModels
{
	public class SellSymbolViewModel : BaseViewModel, IQueryAttributable
    {
        public OperationInfo OperationInfo { get; set; }
        public ICommand SellSymbol { get; set; }

        public SellSymbolViewModel()
		{
            SellSymbol = new SellSymbolCommand();
            OperationInfo = new OperationInfo();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            string dataKey = HttpUtility.UrlDecode(query["dataKey"].ToString());
            OperationInfo.Symbol = ((App)Application.Current).DataExchangeServiceManager.Pop(dataKey) as Symbol;
            OnPropertyChanged(nameof(OperationInfo));
        }
    }
}
