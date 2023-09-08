using System;
using System.Collections.ObjectModel;
using LazyStockDiaryMAUI.Models;
using static Android.Content.ClipData;

namespace LazyStockDiaryMAUI.ViewModels
{
	public class SymbolSearchViewModel : BaseViewModel
	{
		public string SearchQuery { get; set; }
		public ObservableCollection<SearchSymbol> SearchItemsSource { get; set; }

		public SymbolSearchViewModel()
		{
			SearchItemsSource = new ObservableCollection<SearchSymbol>();
        }

		public async void SearchListItemTapped(SearchSymbol searchSymbol)
		{
            string key = ((App)Application.Current).DataExchangeServiceManager.Put(searchSymbol);
            await Shell.Current.GoToAsync($"{nameof(BuySymbolPage)}?dataKey={key}");
        }

        public async void SearchQueryCompleted()
		{
			if(SearchQuery != null)
			{
				SearchItemsSource.Clear();
                var items = await ((App)Application.Current).RestServiceManager.Search(SearchQuery);
				items.ForEach(i => SearchItemsSource.Add(i));
				OnPropertyChanged(nameof(SearchItemsSource));
            }
        }
    }
}

