using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Security.Wpf.Permissions
{
    public class UserToVisibilityConverter : MarkupExtension, IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { 
            if(parameter == null || (value as VortexIdentity) == null)
            {
                return Visibility.Collapsed;
            }

            return parameter.ToString()
                    .Split('|')
                    .Where(p => p != string.Empty)
                    .Select(p => p.ToLower())
                    .Intersect((value as VortexIdentity).Roles.Select(role => role.ToLower()))
                    .Any() ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

        private static UserToVisibilityConverter _converter;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null)
                _converter = new UserToVisibilityConverter();

            return _converter;
        }

    }
}