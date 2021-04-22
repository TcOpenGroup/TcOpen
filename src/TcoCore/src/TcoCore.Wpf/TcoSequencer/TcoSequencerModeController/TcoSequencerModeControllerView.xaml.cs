using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Vortex.Presentation.Wpf.Converters;

namespace TcoCore
{
    /// <summary>
    /// Interaction logic for TcoTask.xaml
    /// </summary>
    public partial class TcoSequencerModeControllerView : UserControl
    {
        public TcoSequencerModeControllerView()
        {
            InitializeComponent();
        }
    }

    public class SequenceModeVisibleInConverter : BaseConverter
    {

        public override object ToConvert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            eSequencerMode VisibleInMode = eSequencerMode.Invalid;
            Enum.TryParse((string)parameter, true, out VisibleInMode);
            
            var enumValue = (eSequencerMode)Enum.ToObject(typeof(eSequencerMode), value);
            return enumValue == VisibleInMode ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
