﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherApp.ViewModel.ValueConverters;
public class BoolToRainConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isRaining = (bool)value;

        if (isRaining)
            return "Currently raining";

        return "Not raining";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string isRaining = (string)value;

        if (isRaining == "Currently raining")
            return true;

        return false;
    }
}

