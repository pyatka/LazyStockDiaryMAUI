using LazyStockDiaryMAUI.Platforms;
using LazyStockDiaryMAUI.Services;

namespace LazyStockDiaryMAUI;

public partial class App : Application
{
	public RestService RestServiceManager;
	public DataExchangeService DataExchangeServiceManager;
	public DatabaseService DatabaseServiceManager;

	public App()
	{
		InitializeComponent();

		RestServiceManager = new RestService();
		DataExchangeServiceManager = new DataExchangeService();
		DatabaseServiceManager = new DatabaseService();

        MainPage = new AppShell();
	}
}

