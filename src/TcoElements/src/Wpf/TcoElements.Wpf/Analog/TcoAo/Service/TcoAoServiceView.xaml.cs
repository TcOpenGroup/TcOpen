using System.ComponentModel;
using System.Windows.Controls;

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
    }
}
