using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace TcoRexrothPress.Converters
{
    public class TcoSmartFunctionKitVisibilityDefaultConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            eTcoSmartFunctionKitCommand_v_4_x_x command = (eTcoSmartFunctionKitCommand_v_4_x_x)Enum.ToObject(typeof(eTcoSmartFunctionKitCommand_v_4_x_x), value);

            switch (command)
            {
                case eTcoSmartFunctionKitCommand_v_4_x_x.SetSystemVariable:
                case eTcoSmartFunctionKitCommand_v_4_x_x.LockParticipant:
                case eTcoSmartFunctionKitCommand_v_4_x_x.ReadSystemVariable:
                    return Visibility.Visible;
                case eTcoSmartFunctionKitCommand_v_4_x_x.StartProgram:
                case eTcoSmartFunctionKitCommand_v_4_x_x.Positioning:
                case eTcoSmartFunctionKitCommand_v_4_x_x.Jog:
                case eTcoSmartFunctionKitCommand_v_4_x_x.SetProgramActive:
                case eTcoSmartFunctionKitCommand_v_4_x_x.Tare:
                case eTcoSmartFunctionKitCommand_v_4_x_x.ClearError:
                case eTcoSmartFunctionKitCommand_v_4_x_x.StopMovement:
                case eTcoSmartFunctionKitCommand_v_4_x_x.RestartDrive:
                case eTcoSmartFunctionKitCommand_v_4_x_x.StartHoming:
                case eTcoSmartFunctionKitCommand_v_4_x_x.SetReference:
                    return Visibility.Collapsed;
                default:
                    return Visibility.Collapsed;
                    break;
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
    public class TcoSmartFunctionKitVisibilityStartProgramConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            eTcoSmartFunctionKitCommand_v_4_x_x command = (eTcoSmartFunctionKitCommand_v_4_x_x)Enum.ToObject(typeof(eTcoSmartFunctionKitCommand_v_4_x_x), value);

            switch (command)
            {
                case eTcoSmartFunctionKitCommand_v_4_x_x.StartProgram:
                    return Visibility.Visible;
                case eTcoSmartFunctionKitCommand_v_4_x_x.Positioning:
                case eTcoSmartFunctionKitCommand_v_4_x_x.Jog:
                case eTcoSmartFunctionKitCommand_v_4_x_x.SetProgramActive:
                case eTcoSmartFunctionKitCommand_v_4_x_x.Tare:
                case eTcoSmartFunctionKitCommand_v_4_x_x.ClearError:
                case eTcoSmartFunctionKitCommand_v_4_x_x.StopMovement:
                case eTcoSmartFunctionKitCommand_v_4_x_x.RestartDrive:
                case eTcoSmartFunctionKitCommand_v_4_x_x.StartHoming:
                case eTcoSmartFunctionKitCommand_v_4_x_x.SetReference:
                case eTcoSmartFunctionKitCommand_v_4_x_x.SetSystemVariable:
                case eTcoSmartFunctionKitCommand_v_4_x_x.LockParticipant:
                case eTcoSmartFunctionKitCommand_v_4_x_x.ReadSystemVariable:
                    return Visibility.Collapsed;
                default:
                    return Visibility.Collapsed;
                    break;
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

    public class TcoSmartFunctionKitVisibilityJogTaraPositioningConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            eTcoSmartFunctionKitCommand_v_4_x_x command = (eTcoSmartFunctionKitCommand_v_4_x_x)Enum.ToObject(typeof(eTcoSmartFunctionKitCommand_v_4_x_x), value);

            switch (command)
            {
                case eTcoSmartFunctionKitCommand_v_4_x_x.StartProgram:
                case eTcoSmartFunctionKitCommand_v_4_x_x.ClearError:
                case eTcoSmartFunctionKitCommand_v_4_x_x.StopMovement:
                case eTcoSmartFunctionKitCommand_v_4_x_x.RestartDrive:
                case eTcoSmartFunctionKitCommand_v_4_x_x.StartHoming:
                case eTcoSmartFunctionKitCommand_v_4_x_x.SetSystemVariable:
                case eTcoSmartFunctionKitCommand_v_4_x_x.LockParticipant:
                case eTcoSmartFunctionKitCommand_v_4_x_x.ReadSystemVariable:
                case eTcoSmartFunctionKitCommand_v_4_x_x.SetProgramActive:
                case eTcoSmartFunctionKitCommand_v_4_x_x.SetReference:
                    return Visibility.Collapsed;
                case eTcoSmartFunctionKitCommand_v_4_x_x.Tare:
                case eTcoSmartFunctionKitCommand_v_4_x_x.Positioning:
                case eTcoSmartFunctionKitCommand_v_4_x_x.Jog:
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

    public class TcoSmartFunctionKitVisibilitySetProgramConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            eTcoSmartFunctionKitCommand_v_4_x_x command = (eTcoSmartFunctionKitCommand_v_4_x_x)Enum.ToObject(typeof(eTcoSmartFunctionKitCommand_v_4_x_x), value);

            switch (command)
            {
                case eTcoSmartFunctionKitCommand_v_4_x_x.StartProgram:
                case eTcoSmartFunctionKitCommand_v_4_x_x.ClearError:
                case eTcoSmartFunctionKitCommand_v_4_x_x.StopMovement:
                case eTcoSmartFunctionKitCommand_v_4_x_x.RestartDrive:
                case eTcoSmartFunctionKitCommand_v_4_x_x.StartHoming:
                case eTcoSmartFunctionKitCommand_v_4_x_x.SetSystemVariable:
                case eTcoSmartFunctionKitCommand_v_4_x_x.LockParticipant:
                case eTcoSmartFunctionKitCommand_v_4_x_x.ReadSystemVariable:
                case eTcoSmartFunctionKitCommand_v_4_x_x.Tare:
                case eTcoSmartFunctionKitCommand_v_4_x_x.SetReference:
                case eTcoSmartFunctionKitCommand_v_4_x_x.Positioning:
                case eTcoSmartFunctionKitCommand_v_4_x_x.Jog:
                    return Visibility.Collapsed;
                case eTcoSmartFunctionKitCommand_v_4_x_x.SetProgramActive:
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

    
}
