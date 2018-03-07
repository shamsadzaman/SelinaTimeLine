using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SelinaTimeLine.Converters
{
    [MarkupExtensionReturnType(typeof(InverseBoolConverter))]
    public class InverseBoolConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return !b;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}