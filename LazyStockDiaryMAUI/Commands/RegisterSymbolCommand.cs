using System;
using System.Windows.Input;
using LazyStockDiaryMAUI.Models;

namespace LazyStockDiaryMAUI.Commands
{
    public class RegisterSymbolCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public async void Execute(object parameter)
        {
            var symbol = parameter as Symbol;
            if(symbol.Quantity > 0)
            {
                var serverRegistredSymbol = await ((App)Application.Current).RestServiceManager.RegisterSymbol(symbol);
                serverRegistredSymbol.Price = symbol.Price;
                serverRegistredSymbol.Quantity = symbol.Quantity;
                serverRegistredSymbol.OperationDate = symbol.OperationDate;
                await ((App)Application.Current).SymbolIntegrityServiceManager.BuySymbol(serverRegistredSymbol);
            }
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}