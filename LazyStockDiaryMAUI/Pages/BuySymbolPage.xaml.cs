using System.Web;
using LazyStockDiaryMAUI.Models;

namespace LazyStockDiaryMAUI;

public partial class BuySymbolPage : ContentPage
{
    public SearchSymbol SearchSymbol { get; set; }
	public BuySymbolPage()
	{
		InitializeComponent();
	}
}
