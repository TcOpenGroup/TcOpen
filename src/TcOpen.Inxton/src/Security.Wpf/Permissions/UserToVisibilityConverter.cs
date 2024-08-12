using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using TcOpen.Inxton.Local.Security;

namespace TcOpen.Inxton.Local.Security.Wpf
{
    public class UserToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || (value as AppIdentity) == null)
            {
                return Visibility.Collapsed;
            }

            return parameter
                .ToString()
                .Split('|')
                .Where(p => p != string.Empty)
                .Select(p => p.ToLower())
                .Intersect((value as AppIdentity).Roles.Select(role => role.ToLower()))
                .Any()
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        ) => throw new NotImplementedException();

        private static UserToVisibilityConverter _converter;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null)
                _converter = new UserToVisibilityConverter();

            return _converter;
        }
    }
}
