using System;
using System.Globalization;
using Xamarin.Forms;

namespace MobileWeather.Converters
{
    public class PathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Device.RuntimePlatform == Device.UWP ? $"Assets/{value}" : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
