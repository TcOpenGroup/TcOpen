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
        public void CheckIfIsEtcMasterOrEtcSlave(IVortexObject obj, out bool isMaster, out bool isSlave)
        {
            Type[] interfaces = obj.GetType().GetInterfaces();

            isMaster = false;
            isSlave = false;

            foreach (Type item in interfaces)
            {
                if (item.Name.Contains("EtcMasterBase"))
                {
                    isMaster = true;
                    break;
                }
                else if (item.Name.Contains("EtcSlaveBoxBase"))
                {
                    isSlave = true;
                    break;
                }
                else if (item.Name.Contains("EtcSlaveTerminalBase"))
                {
                    isSlave = true;
                    break;
                }
                else if (item.Name.Contains("EtcSlaveEndTerminalBase"))
                {
                    isSlave = true;
                    break;
                }
            }
        }
    }
}
