using System;
namespace LazyStockDiaryMAUI.Controls
{
	public class SymbolSearchHandler : SearchHandler
    {
        protected override async void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
                return;
            }
            else
            {
                ItemsSource = await ((App)Application.Current).RestServiceManager.Search(newValue);
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            await Task.Delay(1000);

            //string key = ((App)Application.Current).DataExchangeService.Put(item);
            ////ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;
            //// The following route works because route names are unique in this application.
            //await Shell.Current.GoToAsync($"{nameof(BuyTickerView)}?dataKey={key}");
        }
    }
}