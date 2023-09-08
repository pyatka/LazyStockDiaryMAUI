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

        public RegisterSymbolData ToRegisterSymbolData()
        {
            RegisterSymbolData result = new RegisterSymbolData();
            result.Code = Code;
            result.Exchange = Exchange;
            result.Name = Name;
            result.Type = Type;
            result.Country = Country;
            result.Currency = Currency;
            result.ISIN = ISIN;
            result.PreviousClose = PreviousClose;
            result.PreviousCloseDate = PreviousCloseDate;
            result.Date = DateTime.Now;
            return result;
        }
    }
}

