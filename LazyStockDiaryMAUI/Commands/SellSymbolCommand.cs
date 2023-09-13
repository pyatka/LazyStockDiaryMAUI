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
            var symbol = parameter as Symbol;
            
        }
    }
}

