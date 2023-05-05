using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using TcoCore.Wpf;
using TcOpen.Inxton.Wpf;
using Vortex.Presentation.Wpf;

namespace TcoElements
{
    /// <summary>
    /// Interaction logic for TcoCarouselServiceView.xaml
    /// </summary>
    public partial class TcoCarouselView
        : UserControl
    {
        public TcoCarouselView()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new TcoCarousel();
            }

            InitializeComponent();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == DataContextProperty)
            {
                this.DataContext = this.DataContext.ViewModelizeDataContext<GenericViewModel, TcoCarousel>();
            }
        }
    }



 

    public class TableIsInPositionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var turnAllowed = (bool)value;
            return turnAllowed ? TcoColors.Notification : TcoColors.Warning;
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

  
       
    public class CarouselTurnSignConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var rotationDirection = (eTcoCarouselDirection)((short)value);

            switch (rotationDirection)
            {
                case eTcoCarouselDirection.Cw:
                    return "\u21B7";
                case eTcoCarouselDirection.Ccw:
                    return "\u21B6";
                default:
                    return "";
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
