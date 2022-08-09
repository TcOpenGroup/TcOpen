using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vortex.Connector;
using Vortex.Connector.ValueTypes.Online;

namespace Sandbox.TcoIo.Wpf.Simulation
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SetAllToOPstate : UserControl
    {
        public SetAllToOPstate()
        {
            InitializeComponent();
        }


        private void SetToOpState_Click(object sender, RoutedEventArgs e)
        {
            UpdateSymbol();
            SetToOpState(this.DataContext as IVortexObject);
        }

        private void SetToOpState(IVortexObject obj)
        {
            if (obj != null)
            {
                IVortexObject InfoData = obj.GetKids().Where(i => i.AttributeName.Contains("InfoData")).FirstOrDefault() as IVortexObject;

                if (InfoData != null)
                {
                    IVortexElement State = InfoData.GetKids().Where(i => i.AttributeName.Contains("State")).FirstOrDefault() as IVortexElement;
                    if (State != null)
                    {
                        try
                        {
                            (State as IOnlineUInt).Cyclic = 8;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }

                foreach (IVortexObject child in obj.GetChildren())
                {
                    SetToOpState(child);
                }
            }
        }

        private void UpdateSymbol()
        {
            IVortexObject vo = this.DataContext as IVortexObject;
            if (vo != null)
            {
                tbSymbol.Text = vo.Symbol.ToString();
            }
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateSymbol();
        }
    }
}
