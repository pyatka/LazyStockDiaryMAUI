using System;
using LazyStockDiaryMAUI.Models;

namespace LazyStockDiaryMAUI.Controls
{
	public class OperationTemplateSelector : DataTemplateSelector
	{
        public DataTemplate BuyOperationTemplate { get; set; }
        public DataTemplate SellOperationTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if((item as Operation).Type == ((int)OperationType.Buy))
            {
                return BuyOperationTemplate;
            } else
            {
                return SellOperationTemplate;
            }
        }
    }
}

