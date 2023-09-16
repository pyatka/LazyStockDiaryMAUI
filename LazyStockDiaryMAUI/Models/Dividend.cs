using System;
using SQLite;

namespace LazyStockDiaryMAUI.Models
{
    public class Dividend
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public string Code { get; set; }
        [Indexed]
        public string Exchange { get; set; }

        public DateTime? Date { get; set; }
        public DateTime? DeclarationDate { get; set; }
        [Indexed]
        public DateTime? RecordDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Period { get; set; }
        public double Value { get; set; }
        public double UnadjustedValue { get; set; }
        public string Currency { get; set; }

        public int? Quantity { get; set; }
    }
}

