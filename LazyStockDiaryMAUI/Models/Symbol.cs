using System;
namespace LazyStockDiaryMAUI.Models
{
	public class Symbol
	{
        public string Code { get; set; }
        public string Exchange { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string? ISIN { get; set; }

        public DateTime? PreviousCloseDate { get; set; }

        public double? Open { get; set; }
        public double? Close { get; set; }
        public double? High { get; set; }
        public double? Low { get; set; }
        public int? Volume { get; set; }

        public double? PreviousClose { get; set; }

        public double? ChangeAbsolute { get; set; }
        public double? ChangePercent { get; set; }

        public DateTime? DividendLastUpdate { get; set; }
        public DateTime? EodLastUpdate { get; set; }
    }
}

