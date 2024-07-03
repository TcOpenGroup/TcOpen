using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace TcoCore
{
    public class DialogTypeToIconConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (eDialogType)Enum.Parse(typeof(eDialogType), value.ToString());

            switch (b)
            {
                case eDialogType.Undefined:
                    return MaterialDesignThemes.Wpf.PackIconKind.QuestionMark;
                case eDialogType.Info:
                    return MaterialDesignThemes.Wpf.PackIconKind.Information;
                case eDialogType.Question:
                    return MaterialDesignThemes.Wpf.PackIconKind.QuestionMark;
                case eDialogType.Warning:
                    return MaterialDesignThemes.Wpf.PackIconKind.Warning;
                case eDialogType.Error:
                    return MaterialDesignThemes.Wpf.PackIconKind.Error;
                default:
                    return MaterialDesignThemes.Wpf.PackIconKind.QuestionMark;
            }
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
