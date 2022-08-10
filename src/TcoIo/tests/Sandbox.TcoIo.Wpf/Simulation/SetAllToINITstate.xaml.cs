using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Vortex.Connector;
using Vortex.Connector.ValueTypes.Online;

namespace Sandbox.TcoIo.Wpf.Simulation
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SetAllToINITstate : UserControl
    {
        public SetAllToINITstate()
        {
            InitializeComponent();
        }


        private void SetToINITState_Click(object sender, RoutedEventArgs e)
        {
            UpdateSymbol();
            SetToInitState(this.DataContext as IVortexObject);
        }

        private void SetToInitState(IVortexObject obj)
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
                            (State as IOnlineUInt).Cyclic = 1;
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }

                foreach (IVortexObject child in obj.GetChildren())
                {
                    SetToInitState(child);
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
