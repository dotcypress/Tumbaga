#region

using System;
using Windows.UI;
using Windows.UI.Xaml.Data;

#endregion

namespace Tumbaga.Converters
{
    public class ComplementColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Color)
            {
                var color = (Color) value;
                return new Color
                {
                    R = (byte) (255 - color.R),
                    G = (byte) (255 - color.G),
                    B = (byte) (255 - color.B),
                    A = color.A
                };
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
