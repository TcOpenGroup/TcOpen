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
using Vortex.Connector.ValueTypes.Online;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TopologyRenderer : UserControl
    {
        public void UpdateSyncUnitsState(IVortexObject device)
        {
            bool someSyncUnitHasError = SyncUnitsStateCalc(device);
        }

        public bool SyncUnitsStateCalc(IVortexObject vortexObject)
        {
            bool noSyncUnitHasError = false;
            if (vortexObject != null)
            {
                IVortexElement su =
                    vortexObject
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
                                IVortexElement ObjectId =
                                    InfoData
                                        .GetKids()
                                        .Where(i => i.AttributeName.Contains("ObjectId"))
                                        .FirstOrDefault() as IVortexElement;
                                IVortexElement State =
                                    InfoData
                                        .GetKids()
                                        .Where(i => i.AttributeName.Contains("State"))
                                        .FirstOrDefault() as IVortexElement;
                                IVortexElement SlaveCount =
                                    InfoData
                                        .GetKids()
                                        .Where(i => i.AttributeName.Contains("SlaveCount"))
                                        .FirstOrDefault() as IVortexElement;

                                if (ObjectId != null && State != null && SlaveCount != null)
                                {
                                    OnlinerDWord ObjectIdTag = ObjectId as OnlinerDWord;
                                    OnlinerUInt StateTag = State as OnlinerUInt;
                                    OnlinerUInt SlaveCountTag = SlaveCount as OnlinerUInt;
                                    if (
                                        ObjectIdTag.Synchron != 0
                                        && SlaveCountTag.Synchron > 0
                                        && StateTag.Synchron != 8
                                    )
                                    {
                                        noSyncUnitHasError = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (noSyncUnitHasError)
                            break;
                    }
                }
            }
            return noSyncUnitHasError;
        }
    }
}
