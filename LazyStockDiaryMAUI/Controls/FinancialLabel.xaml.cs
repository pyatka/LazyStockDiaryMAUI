using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LazyStockDiaryMAUI.Controls;

public partial class FinancialLabel : Grid
{
    public double? Value { get
        {
            return (double?)GetValue(ValueProperty);
        }
        set
        {
            SetValue(ValueProperty, value);
        }
    }

    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(
            "Value",
            typeof(double?),
            typeof(FinancialLabel),
            null, BindingMode.OneWay, propertyChanged: (o, prevValue, newValue) =>
            {
                (o as FinancialLabel).LabelChanged((double?)prevValue, (double?)newValue);
            }
            );

    [TypeConverter(typeof(FontSizeConverter))]
    public double FontSize
    {
        get
        {
            return (double)GetValue(FontSizeProperty);
        }
        set
        {
            SetValue(FontSizeProperty, value);
        }
    }

    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(
            "FontSize",
            typeof(double),
            typeof(FinancialLabel),
            null
            );

    private void LabelChanged(double? prevValue, double? newValue)
    {
        if(newValue < 0)
        {
            ValueLabel.TextColor = Color.FromRgb(136, 8, 8);
        } else if(newValue > 0)
        {
            ValueLabel.TextColor = Color.FromRgb(79, 121, 66);
        } else
        {
            ValueLabel.TextColor = Color.FromRgb(169, 169, 169);
        }
    }

    public FinancialLabel()
	{
		InitializeComponent();
	}
}
