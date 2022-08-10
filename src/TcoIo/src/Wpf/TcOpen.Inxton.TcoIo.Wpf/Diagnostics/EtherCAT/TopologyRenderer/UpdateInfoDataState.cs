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
using System.Linq;
using Vortex.Connector.ValueTypes.Online;
using Vortex.Connector.ValueTypes;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TopologyRenderer : UserControl
    {
        public void UpdateInfoDataState(IValueTag sender, ValueChangedEventArgs args)
        {
            InfoDataState = 0;

            InfoDataStateCalc(dt, ref InfoDataState);
        }


        private void InfoDataStateCalc(IVortexObject vortexObject, ref ushort InfoDataState)
        {
            if (vortexObject != null)
            {

            
                Type[] interfaces = vortexObject.GetType().GetInterfaces();

                bool isSlave = false;

                foreach (Type item in interfaces)
                {
                    if (item.Name.Contains("EtcSlaveBoxBase"))
                    {
                        isSlave = true;
                        break;
                    }
                    else if (item.Name.Contains("EtcSlaveTerminalBase"))
                    {
                        isSlave = true;
                        break;
                    }
                }
                if (isSlave)
                {
                    if (vortexObject != null)
                    {
                        IVortexObject InfoData = vortexObject.GetKids().Where(i => i.AttributeName.Contains("InfoData")).FirstOrDefault() as IVortexObject;

                        if (InfoData != null)
                        {
                            IVortexElement State = InfoData.GetKids().Where(i => i.AttributeName.Contains("State")).FirstOrDefault() as IVortexElement;

                            if (State != null)
                            {
                                InfoDataState = (ushort)(InfoDataState | (State as IOnlineUInt).Cyclic);
                            }
                        }
                    }
                }
                foreach (IVortexObject child in vortexObject.GetChildren())
                {
                    InfoDataStateCalc(child, ref InfoDataState);
                }
            }
        }
    }
}
