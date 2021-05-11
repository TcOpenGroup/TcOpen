using System.ComponentModel;
using System.Windows.Controls;

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
    }
}
