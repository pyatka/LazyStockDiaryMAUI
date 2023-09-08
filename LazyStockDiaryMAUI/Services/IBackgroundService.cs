using System;
using LazyStockDiaryMAUI.Models;

namespace LazyStockDiaryMAUI.Platforms
{
	public interface IBackgroundService
	{
        public void UpdateSymbol(Symbol symbol);
    }
}

