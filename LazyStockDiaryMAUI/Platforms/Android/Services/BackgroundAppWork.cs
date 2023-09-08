using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using LazyStockDiaryMAUI.Models;
using LazyStockDiaryMAUI.Services;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace LazyStockDiaryMAUI.Platforms.Android.Services
{
    [Service]
    public class BackgroundAppWork : Service, IBackgroundService
    {
        private DatabaseService databaseService;
        private RestService restService;

        public BackgroundAppWork()
        {
            databaseService = new DatabaseService();
            restService = new RestService();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            if (intent.Action == "UPDATE_SYMBOL")
            {
                Symbol symbol = JsonConvert.DeserializeObject<Symbol>(intent.GetStringExtra("symbol"));
                updateSymbolAction(symbol);
            }
            //else if (intent.Action == "STOP_SERVICE")
            //{
            //    StopForeground(true);
            //    StopSelfResult(startId);
            //}

            return StartCommandResult.NotSticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        private async void updateSymbolAction(Symbol symbol)
        {
            var serverSymbol = await restService.GetSymbol(symbol.Code, symbol.Exchange);
            if (symbol.EodLastUpdate == null || serverSymbol.EodLastUpdate > symbol.EodLastUpdate)
            {
                await databaseService.UpdateSymbolData(serverSymbol, symbol.Id);
            }
        }

        public void UpdateSymbol(Symbol symbol)
        {
            Intent startService = new Intent(MainActivity.ActivityCurrent, typeof(BackgroundAppWork));
            startService.PutExtra("symbol", JsonConvert.SerializeObject(symbol));
            startService.SetAction("UPDATE_SYMBOL");
            MainActivity.ActivityCurrent.StartService(startService);
        }
    }
}

