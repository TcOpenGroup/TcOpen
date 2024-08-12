using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TcOpen.Inxton.Wpf;
using Vortex.Presentation.Wpf;

namespace TcoElements
{
    /// <summary>
    /// Interaction logic for TcoDiServiceView.xaml
    /// </summary>
    public partial class TcoDiServiceView : UserControl
    {
        public TcoDiServiceView()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new TcoDi();
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
                    TcoDi
                >();
            }
        }
    }
}
