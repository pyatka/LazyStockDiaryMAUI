namespace LazyStockDiaryMAUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(SymbolSearchPage), typeof(SymbolSearchPage));
        Routing.RegisterRoute(nameof(BuySymbolPage), typeof(BuySymbolPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(SymbolDetailsPage), typeof(SymbolDetailsPage));
        Routing.RegisterRoute(nameof(SellSymbolPage), typeof(SellSymbolPage));
    }
}