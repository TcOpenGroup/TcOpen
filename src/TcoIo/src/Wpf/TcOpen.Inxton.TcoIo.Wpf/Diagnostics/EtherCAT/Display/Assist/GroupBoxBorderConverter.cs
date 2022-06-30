using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Assist
{
    public class GroupBoxBorderConverter : MarkupExtension, IValueConverter
    {
        private static GroupBoxBorderConverter _instance;
        public static GroupBoxBorderConverter Instance => _instance ?? (_instance = new GroupBoxBorderConverter());
        public object BackgroundBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return Brushes.Transparent;
            if (BackgroundBrush != null)
                return BackgroundBrush;
            
            BackgroundBrush = Application.Current.TryFindResource("Background");
            return BackgroundBrush ?? Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
