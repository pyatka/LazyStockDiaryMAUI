using LazyStockDiaryMAUI.Services;

namespace LazyStockDiaryMAUI;

public partial class App : Application
{
	public RestService RestServiceManager;
	public DataExchangeService DataExchangeServiceManager;
	public App()
	{
		InitializeComponent();

		RestServiceManager = new RestService();
		DataExchangeServiceManager = new DataExchangeService();

        MainPage = new AppShell();
	}
}

