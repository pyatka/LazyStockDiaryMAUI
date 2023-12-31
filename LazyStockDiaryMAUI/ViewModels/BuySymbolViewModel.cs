﻿using System;
using System.Web;
using System.Windows.Input;
using LazyStockDiaryMAUI.Commands;
using LazyStockDiaryMAUI.Models;

namespace LazyStockDiaryMAUI.ViewModels
{
	public class BuySymbolViewModel : BaseViewModel, IQueryAttributable
    {
        public OperationInfo OperationInfo { get; set; }
        public ICommand BuySymbol { get; set; }

        public BuySymbolViewModel()
        {
            OperationInfo = new OperationInfo();
            BuySymbol = new BuySymbolCommand();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            string dataKey = HttpUtility.UrlDecode(query["dataKey"].ToString());
            SearchSymbol searchSymbol = ((App)Application.Current).DataExchangeServiceManager.Pop(dataKey) as SearchSymbol;
            OperationInfo.Symbol = searchSymbol.ToSymbol();
            OnPropertyChanged(nameof(OperationInfo));
        }
    }
}