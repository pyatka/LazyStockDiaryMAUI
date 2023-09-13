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

		public async Task<List<Operation>> GetSymbolOperations(Symbol symbol)
		{
			return await _db.GetSymbolOperations(symbol.Id.Value);
		}

		public async Task<int> UpdateSymbol(Symbol symbol)
		{
			return await _db.UpdateSymbol(symbol);
		}

		public async Task UpdateSymbolDividend(Symbol symbol, List<Dividend> dividends)
		{
			await _db.UpdateSymbolDividend(symbol, dividends);
		}

        public Task<bool> SymbolExists(Symbol symbol)
		{
			return _db.SymbolExists(symbol.Code, symbol.Exchange);
		}

		public bool SymbolAddOperation(Symbol symbol, Operation operation)
		{
			operation.SymbolId = symbol.Id.Value;
			if (!symbol.OperationDate.HasValue)
			{
				symbol.OperationDate = DateTime.Now;
            }
			operation.Date = symbol.OperationDate.Value;
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

				if(symbol.OperationDate < symbol.FirstBuyDate)
				{
                    symbol.FirstBuyDate = symbol.OperationDate;
					symbol.DividendLastUpdate = null;
                }

				await _db.UpdateSymbol(existedSymbol);
				return existedSymbol;
			} else
			{
				symbol.FirstBuyDate = symbol.OperationDate;
                symbol.DividendLastUpdate = null;
                symbol.EodLastUpdate = null;
                symbol.Id = await _db.RegisterSymbol(symbol);
                SymbolAddOperation(symbol, operation);
				return symbol;
            }
		}
	}
}

