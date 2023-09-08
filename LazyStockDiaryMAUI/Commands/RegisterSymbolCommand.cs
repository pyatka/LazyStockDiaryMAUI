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
            var symbol = parameter as RegisterSymbolData;
            if(symbol.Quantity > 0)
            {
                var r = await ((App)Application.Current).RestServiceManager.RegisterSymbol(symbol);
            }
        }
    }
}

