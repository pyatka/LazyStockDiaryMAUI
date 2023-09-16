using System;
using SQLite;

namespace LazyStockDiaryMAUI.Models
{
	public enum OperationType
	{
		Buy = 0,
		Sell = 1,
		Split = 2
	}

	public class Operation
	{
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }

        [Indexed]
        public DateTime Date { get; set; }
		public int? Quantity { get; set; }
		public double? Price { get; set; }
        [Indexed]
        public int Type { get; set; }

		[Indexed]
		public int SymbolId { get; set; }

		public double? HistoricalPrice { get; set; }

		public static Operation CreateOperation(OperationType type)
		{
            Operation operation = new Operation();
            operation.Date = DateTime.Now;
			operation.Type = ((int)type);
			return operation;
        }

		public void ConsumeHistoricalData(Symbol symbol)
		{
            HistoricalPrice = symbol.Price;
        }
    }
}