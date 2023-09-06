using System;
namespace LazyStockDiaryMAUI.Models
{
	public class SearchSymbol
	{
        public string Code { get; set; }
        public string Exchange { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string? ISIN { get; set; }
        public double? PreviousClose { get; set; }
        public DateTime? PreviousCloseDate { get; set; }
    }
}

