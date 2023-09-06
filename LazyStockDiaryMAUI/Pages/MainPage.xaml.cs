namespace LazyStockDiaryMAUI;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

    async void AddSymbolClicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(SymbolSearchPage)}");
    }
}


