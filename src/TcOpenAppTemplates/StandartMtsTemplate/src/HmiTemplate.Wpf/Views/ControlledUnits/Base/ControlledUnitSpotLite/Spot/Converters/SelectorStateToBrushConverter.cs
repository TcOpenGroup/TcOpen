using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace MainPlc
{
    public class SelectorStateToBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            eCUMode stateNumber = (eCUMode)Enum.ToObject(typeof(eCUMode), value);

            switch (stateNumber)
            {
                case eCUMode.NoMode:
                    return Application.Current.Resources["MtsDarkGray"];
                case eCUMode.GroundMode:
                    return Application.Current.Resources["MtsLightBlue"];
                case eCUMode.GroundModeDone:
                    return Application.Current.Resources["MtsBlue"];
                case eCUMode.AutomatMode:
                    return Application.Current.Resources["MtsGreen"];
                case eCUMode.ManualMode:
                    return Application.Current.Resources["MtsYellow"];
                default:
                    return Application.Current.Resources["MtsDarkGray"];
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

    public class SelectorStateToForegroundBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            eCUMode stateNumber = (eCUMode)Enum.ToObject(typeof(eCUMode), value);

            switch (stateNumber)
            {
                case eCUMode.NoMode:
                    return Application.Current.Resources["OnMtsDarkGray"];
                case eCUMode.GroundMode:
                    return Application.Current.Resources["OnMtsLightBlue"];
                case eCUMode.GroundModeDone:
                    return Application.Current.Resources["OnMtsBlue"];
                case eCUMode.AutomatMode:
                    return Application.Current.Resources["OnMtsGreen"];
                case eCUMode.ManualMode:
                    return Application.Current.Resources["OnMtsYellow"];
                default:
                    return Application.Current.Resources["OnMtsDarkGray"];
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

    public class SelectorStateToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stateNumber = (ushort)value;

            if (stateNumber == (int)eCUMode.ManualMode)
                return Visibility.Collapsed;

            else
                return Visibility.Visible;

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

    public class SelectorStateToOpacityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stateNumber = (ushort)value;

            if (stateNumber == (int)eCUMode.ManualMode)
                return 1;

            else
                return 0.5;

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

    public class ControledUnitStateToBackgroundBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //ControledUnitState state = (ControledUnitState)Enum.ToObject(typeof(ControledUnitState), value);
            //switch (state)
            //{
            //    case ControledUnitState.None:
            //        return Application.Current.Resources["MtsDarkGray"];
            //    case ControledUnitState.HasWarning:
            //        return Application.Current.Resources["Warning"];
            //    case ControledUnitState.HasError:
            //        return Application.Current.Resources["Error"];
            //    default:
            //        return Application.Current.Resources["MtsDarkGray"];
            //}

            return Application.Current.Resources["MtsDarkGray"];

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

    public class SpotControledUnitStateToBackgroundBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //ControledUnitState state = (ControledUnitState)Enum.ToObject(typeof(ControledUnitState), value);
            //switch (state)
            //{
            //    case ControledUnitState.None:
            //        return new SolidColorBrush(Colors.Transparent);
            //    case ControledUnitState.HasWarning:
            //        return Application.Current.Resources["Warning"];
            //    case ControledUnitState.HasError:
            //        return Application.Current.Resources["Error"];
            //    default:
            //        return new SolidColorBrush(Colors.Transparent);
            //}

            return Application.Current.Resources["MtsDarkGray"];

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
    public class ControledUnitStateToForegroundBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //ControledUnitState state = (ControledUnitState)Enum.ToObject(typeof(ControledUnitState), value);
            //switch (state)
            //{
            //    case ControledUnitState.None:
            //        return Application.Current.Resources["OnMtsDarkGray"];
            //    case ControledUnitState.HasWarning:
            //        return Application.Current.Resources["OnWarning"];
            //    case ControledUnitState.HasError:
            //        return Application.Current.Resources["OnError"];
            //    default:
            //        return Application.Current.Resources["OnMtsDarkGray"];
            //}

            return Application.Current.Resources["MtsDarkGray"];

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

    public class SequencerStateToBackgroundBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //SeqencerStepState state = (SeqencerStepState)Enum.ToObject(typeof(SeqencerStepState), value);
            //switch (state)
            //{
            //    case SeqencerStepState.None:
            //        return Application.Current.Resources["Surface"];
            //    case SeqencerStepState.HasWarning:
            //        return Application.Current.Resources["Warning"];
            //    case SeqencerStepState.HasError:
            //        return Application.Current.Resources["Error"];
            //    default:
            //        return Application.Current.Resources["Surface"];
            //}

            return Application.Current.Resources["Surface"];

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

    public class SequencerStateToBorderBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //SeqencerStepState state = (SeqencerStepState)Enum.ToObject(typeof(SeqencerStepState), value);
            //switch (state)
            //{
            //    case SeqencerStepState.None:
            //        return Application.Current.Resources["OnSurface"];
            //    case SeqencerStepState.HasWarning:
            //        return Application.Current.Resources["Warning"];
            //    case SeqencerStepState.HasError:
            //        return Application.Current.Resources["Error"];
            //    default:
            //        return Application.Current.Resources["Surface"];
            //}

            return Application.Current.Resources["Surface"];

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

    public class SequencerStateToStringConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

           // SeqencerStepState state = (SeqencerStepState)Enum.ToObject(typeof(SeqencerStepState), value);

           //var retVal= Enum.GetName(typeof(SeqencerStepState), state);

           // if (retVal == nameof(SeqencerStepState.None))
           // {
           //     retVal = string.Empty;
           // }
           // return retVal;

            return Application.Current.Resources["Surface"];

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
