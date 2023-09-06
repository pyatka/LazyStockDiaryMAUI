using LazyStockDiaryMAUI.Models;
using LazyStockDiaryMAUI.ViewModels;

namespace LazyStockDiaryMAUI;

public partial class SymbolSearchPage : ContentPage
{
	public SymbolSearchPage()
	{
		InitializeComponent();
    }

    public void SearchQueryCompleted(System.Object sender, System.EventArgs e)
    {
		((SymbolSearchViewModel)BindingContext).SearchQueryCompleted();
    }

    void SearchListItemTapped(System.Object sender, Microsoft.Maui.Controls.ItemTappedEventArgs e)
    {
        ((SymbolSearchViewModel)BindingContext).SearchListItemTapped(e.Item as SearchSymbol);
    }
}
