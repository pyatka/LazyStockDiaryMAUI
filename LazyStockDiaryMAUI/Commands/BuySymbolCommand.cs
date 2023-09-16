using System;
using System.Windows.Input;
using LazyStockDiaryMAUI.Models;

namespace LazyStockDiaryMAUI.Commands
{
    public class BuySymbolCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public async void Execute(object parameter)
        {
            var operationInfo = parameter as OperationInfo;
            if (operationInfo.Quantity > 0)
            {
                var serverRegistredSymbol = await ((App)Application.Current).RestServiceManager.RegisterSymbol(operationInfo.Symbol);
                operationInfo.Symbol = serverRegistredSymbol;
                await ((App)Application.Current).SymbolIntegrityServiceManager.BuySymbol(operationInfo);
            }

            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}