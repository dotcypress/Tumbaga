#region

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

#endregion

namespace Tumbaga.Converters
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var stringValue = value as string;
            if (stringValue != null)
            {
                var inverse = parameter != null && parameter.ToString() == "inverse";
                var isNullOrEmpty = string.IsNullOrWhiteSpace(stringValue);
                return inverse
                    ? (isNullOrEmpty ? Visibility.Visible : Visibility.Collapsed)
                    : (isNullOrEmpty ? Visibility.Collapsed : Visibility.Visible);
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
