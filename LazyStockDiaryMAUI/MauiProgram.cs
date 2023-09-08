using LazyStockDiaryMAUI.Platforms;
using LazyStockDiaryMAUI.Platforms.Android.Services;
using Microsoft.Extensions.Logging;

namespace LazyStockDiaryMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
#if ANDROID
        builder.Services.AddTransient<IBackgroundService, BackgroundAppWork>();
#endif

        return builder.Build();
	}
}

