using System;
using SQLite;

namespace LazyStockDiaryMAUI.Models
{
    public class Symbol
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }

        [Indexed]
        public string Code { get; set; }
        [Indexed]
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

        [Indexed]
        public DateTime? DividendLastUpdate { get; set; }
        [Indexed]
        public DateTime? EodLastUpdate { get; set; }

        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public DateTime? OperationDate { get; set; }

        public DateTime? FirstBuyDate { get; set; }

        [Ignore]
        public double PotentialRevenue {
            get
            {
                return Quantity.Value * Price.Value;
            }
        }

        public void UpdateValues(Symbol dataSymbol)
        {
            PreviousClose = dataSymbol.PreviousClose;
            PreviousCloseDate = dataSymbol.PreviousCloseDate;

            Open = dataSymbol.Open;
            Close = dataSymbol.Close;
            High = dataSymbol.High;
            Low = dataSymbol.Low;
            Volume = dataSymbol.Volume;

            ChangeAbsolute = dataSymbol.ChangeAbsolute;
            ChangePercent = dataSymbol.ChangePercent;
        }
    }
}

