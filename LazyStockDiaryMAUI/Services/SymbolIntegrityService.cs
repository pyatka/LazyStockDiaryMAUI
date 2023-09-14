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

		public bool SymbolAddOperation(OperationInfo operationInfo, Operation operation)
		{
			operation.SymbolId = operationInfo.Symbol.Id.Value;
			if (!operationInfo.Date.HasValue)
			{
                operationInfo.Date = DateTime.Now;
            }
			operation.Date = operationInfo.Date.Value;
			_db.PutOperation(operation);
			return true;
		}

        public async Task<bool> SellSymbol(OperationInfo info)
		{
            var symbolExists = await SymbolExists(info.Symbol);
			if (symbolExists)
			{
				if (info.Quantity > info.Symbol.Quantity)
				{
					info.Quantity = info.Symbol.Quantity;
				}

				Operation operation = Operation.CreateOperation(OperationType.Sell);
				operation.Price = info.Price;
				operation.Quantity = info.Quantity;
				SymbolAddOperation(info, operation);
			}

			return true;
		}


        public async Task<Symbol> BuySymbol(OperationInfo operationInfo)
		{
			Operation operation = Operation.CreateOperation(OperationType.Buy);
			operation.UpdateInfo(operationInfo);

            var symbolExists = await SymbolExists(operationInfo.Symbol);
            if (symbolExists)
			{
				Symbol existedSymbol = await _db.GetSymbol(operationInfo.Symbol.Code, operationInfo.Symbol.Exchange);
                operationInfo.Symbol = existedSymbol;
                SymbolAddOperation(operationInfo, operation);

				var newQuantity = existedSymbol.Quantity + operation.Quantity;
				var newPrice = (existedSymbol.Price * existedSymbol.Quantity + operation.Price * operation.Quantity) / newQuantity;

				existedSymbol.Price = newPrice;
				existedSymbol.Quantity = newQuantity;

				if(operationInfo.Date < existedSymbol.FirstBuyDate)
				{
                    existedSymbol.FirstBuyDate = operationInfo.Date;
                    existedSymbol.DividendLastUpdate = null;
                }

				await _db.UpdateSymbol(existedSymbol);
				return existedSymbol;
			} else
			{
                operationInfo.Symbol.FirstBuyDate = operationInfo.Date;
                operationInfo.Symbol.DividendLastUpdate = null;
                operationInfo.Symbol.EodLastUpdate = null;
				operationInfo.Symbol.Id = null;
                operationInfo.Symbol.Id = await _db.RegisterSymbol(operationInfo.Symbol);
                SymbolAddOperation(operationInfo, operation);
				return operationInfo.Symbol;
            }
		}
	}
}