using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows;

namespace TcoAimTtiPowerSupply
{
    public class TcoQlSeriesGetCommandDescriptionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string command = Enum.GetName(typeof(eTcoQWlSeriesSupplyGetCommands_v_1_x_x), value);

            if (string.IsNullOrEmpty(command))
            {
                return string.Empty;
            }
            var selectedCmd = QlSeriesCommands.Commands.Where(p => p.Type == eQlCommandType.GetCommand).Where(q => q.Key == command).FirstOrDefault();

            return String.Format("\r\n Syntax:{0}\r\n\r\n {1}", selectedCmd.Syntax, selectedCmd.HelpDescription);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class TcoQlSeriesSetCommandDescriptionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string command = Enum.GetName(typeof(eTcoQWlSeriesSupplySetCommands_v_1_x_x), value);

            if (string.IsNullOrEmpty(command))
            {
                return string.Empty;
            }
            var selectedCmd = QlSeriesCommands.Commands.Where(p => p.Type == eQlCommandType.SetCommand).Where(q => q.Key == command).FirstOrDefault();

            return String.Format("\r\n Syntax:{0}\r\n\r\n {1}", selectedCmd.Syntax, selectedCmd.HelpDescription);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
    public class TcoQlSeriesGetNValueVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string command = Enum.GetName(typeof(eTcoQWlSeriesSupplyGetCommands_v_1_x_x), value);

            if (string.IsNullOrEmpty(command))
            {
                return Visibility.Collapsed;
            }
            var selectedCmd = QlSeriesCommands.Commands.Where(p => p.Type == eQlCommandType.GetCommand).Where(q => q.Key == command).FirstOrDefault();

            if (selectedCmd.Syntax.Contains("<N>"))
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
    public class TcoQlSeriesSetNValueVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string command = Enum.GetName(typeof(eTcoQWlSeriesSupplySetCommands_v_1_x_x), value);

            if (string.IsNullOrEmpty(command))
            {
                return Visibility.Collapsed;
            }

            var selectedCmd = QlSeriesCommands.Commands.Where(p => p.Type == eQlCommandType.SetCommand).Where(q => q.Key == command).FirstOrDefault();

            if (selectedCmd.Syntax.Contains("<N>"))
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class TcoQlSeriesSetNrfValueVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string command = Enum.GetName(typeof(eTcoQWlSeriesSupplySetCommands_v_1_x_x), value);

            if (string.IsNullOrEmpty(command))
            {
                return Visibility.Collapsed;
            }

            var selectedCmd = QlSeriesCommands.Commands.Where(p => p.Type == eQlCommandType.SetCommand).Where(q => q.Key == command).FirstOrDefault();

            if (selectedCmd.Syntax.Contains("<NRF>"))
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
    }
