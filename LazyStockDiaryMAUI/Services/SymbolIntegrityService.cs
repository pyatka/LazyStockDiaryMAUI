using System;
using LazyStockDiaryMAUI.Models;

namespace LazyStockDiaryMAUI.Services
{
	public class SymbolIntegrityService
	{
		private DatabaseService _db;

		public SymbolIntegrityService()
		{
			_db = ((App)Application.Current).DatabaseServiceManager;
        }

		public Task<bool> SymbolExists(Symbol symbol)
		{
			return _db.SymbolExists(symbol.Code, symbol.Exchange);
		}

		public bool SymbolAddOperation(Symbol symbol, Operation operation)
		{
			operation.SymbolId = symbol.Id.Value;
			_db.PutOperation(operation);
			return true;
		}

		public async Task<Symbol> BuySymbol(Symbol symbol)
		{
			Operation operation = Operation.CreateOperation(OperationType.Buy);
			operation.Price = symbol.Price;
			operation.Quantity = symbol.Quantity;

            var symbolExists = await SymbolExists(symbol);
            if (symbolExists)
			{
				Symbol existedSymbol = await _db.GetSymbol(symbol.Code, symbol.Exchange);
				SymbolAddOperation(existedSymbol, operation);

				var newQuantity = existedSymbol.Quantity + operation.Quantity;
				var newPrice = (existedSymbol.Price * existedSymbol.Quantity + operation.Price * operation.Quantity) / newQuantity;

				existedSymbol.Price = newPrice;
				existedSymbol.Quantity = newQuantity;

				await _db.UpdateSymbolData(existedSymbol);
				return existedSymbol;
			} else
			{
				symbol.Id = await _db.RegisterSymbol(symbol);
                SymbolAddOperation(symbol, operation);
				return symbol;
            }
		}
	}
}

