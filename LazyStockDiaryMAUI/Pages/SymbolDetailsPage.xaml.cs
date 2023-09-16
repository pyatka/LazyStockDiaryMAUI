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

    async void PageAppearing(System.Object sender, System.EventArgs e)
    {
        await (BindingContext as SymbolDetailsViewModel).UpdateOperationsList();
        await (BindingContext as SymbolDetailsViewModel).UpdateDividendsList();
    }
}
