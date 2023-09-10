using LazyStockDiaryMAUI.Platforms;
using LazyStockDiaryMAUI.Services;

namespace LazyStockDiaryMAUI;

public partial class App : Application
{
	public RestService RestServiceManager;
	public DataExchangeService DataExchangeServiceManager;
	public DatabaseService DatabaseServiceManager;
	public SymbolIntegrityService SymbolIntegrityServiceManager;

	public App()
	{
		InitializeComponent();

		RestServiceManager = new RestService();
		DataExchangeServiceManager = new DataExchangeService();
		DatabaseServiceManager = new DatabaseService();
		SymbolIntegrityServiceManager = new SymbolIntegrityService();

        MainPage = new AppShell();
	}
}

