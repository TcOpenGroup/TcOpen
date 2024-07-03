using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TcOpen.Inxton.Wpf;

namespace TcoElements
{
    /// <summary>
    /// Interaction logic for TcoAiServiceView.xaml
    /// </summary>
    public partial class TcoAoServiceView : UserControl
    {
        public TcoAoServiceView()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new TcoAo();
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
                    TcoAo
                >();
            }
        }
    }
}
