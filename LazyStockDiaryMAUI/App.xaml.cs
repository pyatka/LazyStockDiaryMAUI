using LazyStockDiaryMAUI.Services;

namespace LazyStockDiaryMAUI;

public partial class App : Application
{
	public RestService RestServiceManager;
	public App()
	{
		InitializeComponent();

		RestServiceManager = new RestService();

		MainPage = new AppShell();
	}
}

