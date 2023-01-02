#nullable disable
using FastTool.Utils;
using System;
using System.Globalization;
using System.Numerics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace FastTool.WPF.Converters;

public class BoolToVisibility : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool val)
        {
            if (val)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        throw new Exception();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}

public class InverseBoolToVisibility : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool val)
        {
            if (val)
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        throw new Exception();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}

public class SliderValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (double)value < 16 ? (double)value : double.PositiveInfinity;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}

public class ToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value == null ? null : value.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}

public class ErrorToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (Error)value switch
        {
            Error.None => Brushes.Black,
            Error.IdentityName => Brushes.DarkRed,
            Error.ReservedName => Brushes.IndianRed,
            _ => throw new NotImplementedException(),
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}
