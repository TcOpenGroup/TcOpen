using System.ComponentModel;
using System.Windows.Controls;

namespace TcoElements
{
    /// <summary>
    /// Interaction logic for TcoDiServiceView.xaml
    /// </summary>
    public partial class TcoDoServiceView : UserControl
    {
        public TcoDoServiceView()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new TcoDi();
            }

            InitializeComponent();

        }
    }
}
