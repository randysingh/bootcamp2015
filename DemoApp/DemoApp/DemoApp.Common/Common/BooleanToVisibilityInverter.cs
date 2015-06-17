using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace DemoApp.Common.Common
{
    public class BooleanToVisibilityInverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !((value is bool && (bool)value)) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
