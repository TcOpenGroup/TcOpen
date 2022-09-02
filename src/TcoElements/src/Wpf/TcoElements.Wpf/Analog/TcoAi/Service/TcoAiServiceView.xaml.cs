using System.ComponentModel;
using System.Windows.Controls;

namespace TcoElements
{
    /// <summary>
    /// Interaction logic for TcoAiServiceView.xaml
    /// </summary>
    public partial class TcoAiServiceView : UserControl
    {
        public TcoAiServiceView()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataContext = new TcoAi();
            }

            InitializeComponent();

        }
    }
}
