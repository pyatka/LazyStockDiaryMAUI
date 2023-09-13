using System.Timers;
using LazyStockDiaryMAUI.Models;
using LazyStockDiaryMAUI.Platforms;
using LazyStockDiaryMAUI.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace LazyStockDiaryMAUI;

public partial class MainPage : ContentPage
{
	int count = 0;

    public MainPage()
	{
		InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Focus();
        (BindingContext as MainViewModel).UpdateSymbols();
    }

    async void AddSymbolClicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(SymbolSearchPage)}");
    }

    public void SymbolTapped(System.Object sender, ItemTappedEventArgs e)
    {
        (BindingContext as MainViewModel).SymbolTapped(e.Item as Symbol);
    }
}