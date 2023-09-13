using System;
using System.Collections.ObjectModel;
using System.Web;
using System.Windows.Input;
using LazyStockDiaryMAUI.Commands;
using LazyStockDiaryMAUI.Models;

namespace LazyStockDiaryMAUI.ViewModels
{
	public class SymbolDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        public Symbol Symbol { get; set; }
        public ObservableCollection<Operation> Operations { get; set; }
        public ICommand SellSymbol { get; set; }

        public SymbolDetailsViewModel()
		{
            SellSymbol = new SellSymbolCommand();
            Operations = new ObservableCollection<Operation>();
		}

        public async void UpdateOperationsList()
        {
            Operations.Clear();
            var operations = await ((App)Application.Current).SymbolIntegrityServiceManager
                                            .GetSymbolOperations(Symbol);
            operations.ForEach(o => Operations.Add(o));
            OnPropertyChanged(nameof(Operations));
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            string dataKey = HttpUtility.UrlDecode(query["dataKey"].ToString());
            Symbol = ((App)Application.Current).DataExchangeServiceManager.Pop(dataKey) as Symbol;
            UpdateOperationsList();
            OnPropertyChanged(nameof(Symbol));
        }
    }
}