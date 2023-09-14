using System;
using System.Windows.Input;
using LazyStockDiaryMAUI.Models;

namespace LazyStockDiaryMAUI.Commands
{
	public class SellSymbolCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public async void Execute(object parameter)
        {
            var operationInfo = parameter as OperationInfo;
            await ((App)Application.Current).SymbolIntegrityServiceManager.SellSymbol(operationInfo);
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}

