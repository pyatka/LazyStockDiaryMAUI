using LazyStockDiaryMAUI.Models;
using LazyStockDiaryMAUI.ViewModels;

namespace LazyStockDiaryMAUI;

public partial class SymbolDetailsPage : ContentPage
{
	public SymbolDetailsPage()
	{
		InitializeComponent();
	}

    void SellTapped(Object sender, EventArgs e)
    {
		(BindingContext as SymbolDetailsViewModel).SellSymbol();
    }
}
