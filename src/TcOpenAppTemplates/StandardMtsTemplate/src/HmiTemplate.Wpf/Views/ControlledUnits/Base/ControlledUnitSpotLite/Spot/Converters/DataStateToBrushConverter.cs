using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace MainPlc
{
    public class DataStateToBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            TcoInspectors.eOverallResult stateNumber = (TcoInspectors.eOverallResult)Enum.ToObject(typeof(TcoInspectors.eOverallResult), value);

            switch (stateNumber)
            {
                case TcoInspectors.eOverallResult.NoAction:
                    return Application.Current.Resources["MtsGray"];
                case TcoInspectors.eOverallResult.InProgress:
                    return Application.Current.Resources["MtsLightBlue"];
                case TcoInspectors.eOverallResult.Passed:
                    return Application.Current.Resources["MtsGreen"];
                case TcoInspectors.eOverallResult.Failed:
                    return Application.Current.Resources["MtsRed"];
                default:
                    return Application.Current.Resources["MtsGray"];
            }
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
    public class DataStateToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            TcoInspectors.eOverallResult stateNumber = (TcoInspectors.eOverallResult)Enum.ToObject(typeof(TcoInspectors.eOverallResult), value);


            switch (stateNumber)
            {
                case TcoInspectors.eOverallResult.NoAction:
                    return Visibility.Collapsed;
                case TcoInspectors.eOverallResult.InProgress:
                    return Visibility.Collapsed;
                case TcoInspectors.eOverallResult.Passed:
                    return Visibility.Collapsed;
                case TcoInspectors.eOverallResult.Failed:
                    return Visibility.Visible;
                default:
                    return Visibility.Collapsed;
            }
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

    public class DataStateToIconKindConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            TcoInspectors.eOverallResult stateNumber = (TcoInspectors.eOverallResult)Enum.ToObject(typeof(TcoInspectors.eOverallResult), value);


            switch (stateNumber)
            {
                case TcoInspectors.eOverallResult.NoAction:
                    return MaterialDesignThemes.Wpf.PackIconKind.None;
                case TcoInspectors.eOverallResult.InProgress:
                    return MaterialDesignThemes.Wpf.PackIconKind.FileCheck;
                case TcoInspectors.eOverallResult.Passed:
                    return MaterialDesignThemes.Wpf.PackIconKind.FileCheck;
                case TcoInspectors.eOverallResult.Failed:
                    return MaterialDesignThemes.Wpf.PackIconKind.FileExcel;
                default:
                    return MaterialDesignThemes.Wpf.PackIconKind.None;
            }
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
