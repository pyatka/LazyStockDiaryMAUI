namespace LazyStockDiaryMAUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(SymbolSearchPage), typeof(SymbolSearchPage));
        Routing.RegisterRoute(nameof(BuySymbolPage), typeof(BuySymbolPage));
    }
}