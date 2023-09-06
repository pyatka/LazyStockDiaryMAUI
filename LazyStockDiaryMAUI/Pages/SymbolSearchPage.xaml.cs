using LazyStockDiaryMAUI.Controls;

namespace LazyStockDiaryMAUI;

public partial class SymbolSearchPage : ContentPage
{
	public SymbolSearchPage()
	{
		InitializeComponent();
        Loaded += PageLoaded;
	}

    private void PageLoaded(object sender, EventArgs e)
    {
        symbolSearchHandler.Focus();
    }
}
