using System;
using LazyStockDiaryMAUI.Models;
using static Android.Icu.Text.IDNA;

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

		public async Task<DateTime?> GetSymbolFirstBuyDate(Symbol symbol)
		{
			return await _db.GetSymbolFirstBuyDate(symbol);
		}

		public async void UpdateDividendQuantity(Symbol symbol,
													int quantity,
											  DateTime? startDate = null,
											  DateTime? endDate = null)
		{
			if (!startDate.HasValue)
			{
				startDate = new DateTime();
			}

			if (!endDate.HasValue)
			{
				endDate = DateTime.Now;
			}

			var dividends = await _db.GetSymbolDividends(symbol.Code, symbol.Exchange, startDate.Value, endDate.Value);

			if(dividends.Count > 0)
			{
				foreach(Dividend d in dividends)
				{
					d.Quantity = quantity;
				}
				await _db.UpdateSymbolDividend(symbol, dividends, false);
			}
		}

		public async void UpdateSymbolHistoryPrice(Symbol symbol)
		{
			var operations = await GetSymbolOperations(symbol);
			operations.Reverse();

			int? calculatedQuantity = null;
			double? calculatedPrice = null;
			DateTime? previousDate = new DateTime();

            foreach (Operation operation in operations)
			{
				if(!calculatedQuantity.HasValue)
				{
					calculatedQuantity = operation.Quantity;
					calculatedPrice = operation.Price;
				} else
				{
                    UpdateDividendQuantity(symbol, calculatedQuantity.Value, previousDate, operation.Date);

                    if (operation.Type == (int)OperationType.Buy)
					{
						var newQuantity = calculatedQuantity + operation.Quantity;
						calculatedPrice = (calculatedQuantity * calculatedPrice + operation.Quantity * operation.Price) / newQuantity;
						calculatedQuantity = newQuantity;
                    } else if(operation.Type == ((int)OperationType.Sell))
					{
						calculatedQuantity -= operation.Quantity;
                    }

                    if (calculatedQuantity < 0)
                    {
                        calculatedQuantity = 0;
                    }

                    if (calculatedPrice < 0)
                    {
                        calculatedPrice = 0;
                    }
                }
                previousDate = operation.Date;
            }

            UpdateDividendQuantity(symbol, calculatedQuantity.Value, previousDate);

            symbol.Price = calculatedPrice;
			symbol.Quantity = calculatedQuantity;
			await _db.UpdateSymbol(symbol);
		}

		public bool SymbolAddOperation(OperationInfo operationInfo, OperationType type)
		{
            Operation operation = Operation.CreateOperation(type);
            operation.SymbolId = operationInfo.Symbol.Id.Value;
			operation.ConsumeHistoricalData(operationInfo.Symbol);
            operation.Price = operationInfo.Price;
            operation.Quantity = operationInfo.Quantity;

            if (!operationInfo.Date.HasValue)
			{
                operationInfo.Date = DateTime.Now;
            }
			operation.Date = operationInfo.Date.Value;

			_db.PutOperation(operation);

			UpdateSymbolHistoryPrice(operationInfo.Symbol);
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

				SymbolAddOperation(info, OperationType.Sell);

				info.Symbol.Quantity -= info.Quantity;
                await _db.UpdateSymbol(info.Symbol);
            }

			return true;
		}


        public async Task<bool> BuySymbol(OperationInfo operationInfo)
		{
            var symbolExists = await SymbolExists(operationInfo.Symbol);
            if (symbolExists)
			{
				Symbol existedSymbol = await _db.GetSymbol(operationInfo.Symbol.Code, operationInfo.Symbol.Exchange);

				var newQuantity = existedSymbol.Quantity + operationInfo.Quantity;
				var newPrice = (existedSymbol.Price * existedSymbol.Quantity + operationInfo.Price * operationInfo.Quantity) / newQuantity;

				existedSymbol.Price = newPrice;
				existedSymbol.Quantity = newQuantity;
                existedSymbol.DividendLastUpdate = null;

                operationInfo.Symbol = existedSymbol;
                SymbolAddOperation(operationInfo, OperationType.Buy);
			} else
			{
                operationInfo.Symbol.DividendLastUpdate = null;
                operationInfo.Symbol.EodLastUpdate = null;
				operationInfo.Symbol.Id = null;
				operationInfo.Symbol.Price = operationInfo.Price;
				operationInfo.Symbol.Quantity = operationInfo.Quantity;
                operationInfo.Symbol.Id = await _db.RegisterSymbol(operationInfo.Symbol);
                SymbolAddOperation(operationInfo, OperationType.Buy);
            }
            return true;
        }
	}
}