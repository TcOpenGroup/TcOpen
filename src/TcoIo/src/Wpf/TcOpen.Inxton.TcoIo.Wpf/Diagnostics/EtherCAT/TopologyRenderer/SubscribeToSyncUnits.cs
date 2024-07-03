using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using Vortex.Connector.ValueTypes;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TopologyRenderer : UserControl
    {
        public void SubscribeToSyncUnits(IVortexObject device)
        {
            if (device != null)
            {
                IVortexElement su =
                    device
                        .GetKids()
                        .Where(i => i.AttributeName.Contains("SyncUnits"))
                        .FirstOrDefault() as IVortexElement;
                if (su != null)
                {
                    IEnumerable<IVortexElement> SyncUnits =
                        (su as IVortexObject).GetKids() as IEnumerable<IVortexElement>;
                    foreach (IVortexElement SyncUnit in SyncUnits)
                    {
                        IEnumerable<IVortexElement> SyncUnitTasks =
                            (SyncUnit as IVortexObject).GetKids() as IEnumerable<IVortexElement>;
                        foreach (IVortexElement SyncUnitTask in SyncUnitTasks)
                        {
                            IVortexObject InfoData =
                                (SyncUnitTask as IVortexObject)
                                    .GetKids()
                                    .Where(i => i.AttributeName.Contains("InfoData"))
                                    .FirstOrDefault() as IVortexObject;
                            if (InfoData != null)
                            {
                                IVortexElement State =
                                    InfoData
                                        .GetKids()
                                        .Where(i => i.AttributeName.Contains("State"))
                                        .FirstOrDefault() as IVortexElement;

                                if (State != null)
                                {
                                    IValueTag StateTag = State as IValueTag;
                                    StateTag.Subscribe(
                                        (sender, arg) => UpdateSyncUnitsState(device)
                                    );
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
