using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TcoIo.Converters;
using TcoIo.Topology;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TopologyRenderer : UserControl
    {
        public void CheckIfIsEtcMasterOrEtcSlave(
            IVortexObject obj,
            out bool isMaster,
            out bool isSlave
        )
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
