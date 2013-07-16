#region

using System;
using Windows.UI.Xaml.Data;

#endregion

namespace Tumbaga.Converters
{
    public class StringCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string)
            {
                var valueString = value.ToString();
                return parameter != null && parameter.ToString() == "lower"
                    ? valueString.ToLower()
                    : valueString.ToUpper();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
