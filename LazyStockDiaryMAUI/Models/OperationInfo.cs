using System;
namespace LazyStockDiaryMAUI.Models
{
	public class OperationInfo
	{
        public Symbol Symbol { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public DateTime? Date { get; set; }

        public OperationInfo()
        {
            Date = DateTime.Now;
        }
    }
}

