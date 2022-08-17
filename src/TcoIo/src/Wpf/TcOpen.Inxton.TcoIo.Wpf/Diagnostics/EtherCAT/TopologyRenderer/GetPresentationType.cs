using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Vortex.Connector;
using Vortex.Presentation.Wpf;
using System.Collections.ObjectModel;
using TcoIo.Topology;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Data;
using TcoIo.Converters;
using System.Windows.Input;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TopologyRenderer : UserControl
    {
        protected string GetPresentationType(IVortexObject obj)
        {
            string presentationType = "";

            Type[] interfaces = obj.GetType().GetInterfaces();
            foreach (Type item in interfaces)
            {
                if (item.Name.Contains("EtcMasterBase"))
                {
                    presentationType = "TopologyDevice";
                    break;
                }
                else if (item.Name.Contains("EtcSlaveBoxBase"))
                {
                    presentationType = "TopologyBoxM90";
                    break;
                }
                else if (item.Name.Contains("EtcSlaveTerminalBase"))
                {
                    presentationType = "TopologyTerminalM90";
                    break;
                }
                else if (item.Name.Contains("EtcSlaveEndTerminalBase"))
                {
                    presentationType = "TopologyEndTerminalM90";
                    break;
                }
            }
            return presentationType;
        }
    }
}
