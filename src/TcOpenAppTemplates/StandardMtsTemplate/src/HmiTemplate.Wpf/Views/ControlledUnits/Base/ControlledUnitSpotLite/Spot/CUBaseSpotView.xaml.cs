using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace MainPlc
{
    /// <summary>
    /// Interaction logic for fbPneumaticPistonManualView.xaml
    /// </summary>
    public partial class CUBaseSpotView : UserControl
    {
        public CUBaseSpotView()
        {
            InitializeComponent();
            DataContextChanged += a;
            
        }

       

        private void a(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(this.DataContext is CUBaseSpotViewModel))
            {
                if (this.DataContext is CUBase)
                {
                    this.DataContext = new CUBaseSpotViewModel() { Model = this.DataContext };
                }
            }

            
            Console.WriteLine($"Data context: {this.DataContext.GetType().ToString()}");

            var bb = this.DataContext as RenderableViewModel;

            if (bb != null)
            {
                Console.WriteLine($"Data context model: {bb.Model.GetType()}");
            }

            //this.DataContext = FixDataContext<CUBaseSpotViewModel, CUBase>(this.DataContext);
        }


        private object FixDataContext<VM,M>(object currentContext) where VM : RenderableViewModel, new() where M : IVortexObject
        {
            if(!(currentContext is VM))
            {
                if(currentContext is M)
                {
                    return new VM() { Model = currentContext };
                }
            }

            return currentContext;
        }
    }
}
