using System;
using System.Collections.ObjectModel;
using LazyStockDiaryMAUI.Models;
using LazyStockDiaryMAUI.Platforms;

namespace LazyStockDiaryMAUI.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
        public ObservableCollection<Symbol> Symbols { get; set; }

		public MainViewModel()
		{
			Symbols = new ObservableCollection<Symbol>();
		}

        public async void UpdateSymbols()
		{
			Symbols.Clear();
			var symbols = await ((App)Application.Current).DatabaseServiceManager.GetSymbols();
            var service = ((App)Application.Current).MainPage.Handler.MauiContext.Services.GetService<IBackgroundService>();
            foreach(Symbol s in symbols)
			{
				Symbols.Add(s);
				service.UpdateSymbol(s);
			}
            OnPropertyChanged(nameof(Symbols));
        }
	}
}