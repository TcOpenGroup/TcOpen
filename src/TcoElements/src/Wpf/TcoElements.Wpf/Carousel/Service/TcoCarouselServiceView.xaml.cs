using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TcOpen.Inxton.Wpf;
using Vortex.Presentation.Wpf;

namespace TcoElements
{
    /// <summary>
    /// Interaction logic for TcoCarouselServiceView.xaml
    /// </summary>
    public partial class TcoCarouselServiceView : UserControl
    {
        public TcoCarouselServiceView()
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
                this.DataContext = this.DataContext.ViewModelizeDataContext<
                    GenericViewModel,
                    TcoCarousel
                >();
            }
        }
    }
}
