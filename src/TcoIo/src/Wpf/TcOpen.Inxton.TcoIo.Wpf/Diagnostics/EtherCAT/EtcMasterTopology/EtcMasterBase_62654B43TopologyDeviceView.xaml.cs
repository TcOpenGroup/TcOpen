using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TcoIo.Converters;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoIo
{
    public partial class EtcMasterBase_62654B43TopologyDeviceView : UserControl
    {
        private IVortexObject device;

        public EtcMasterBase_62654B43TopologyDeviceView()
        {
            InitializeComponent();
            DataContextChanged += EtcMasterBase_62654B43TopologyDeviceView_DataContextChanged;
        }

        private void EtcMasterBase_62654B43TopologyDeviceView_DataContextChanged(
            object sender,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (
                this.DataContext != null
                && (this.DataContext as EtcMasterBase_62654B43TopologyDeviceViewModel) != null
            )
            {
                device =
                    (this.DataContext as EtcMasterBase_62654B43TopologyDeviceViewModel).Device
                    as IVortexObject;
                SubscribeToSyncUnits(device);
                UpdateSyncUnitsState(device);
            }
        }

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

        public void UpdateSyncUnitsState(IVortexObject device)
        {
            bool someSyncUnitHasError = SyncUnitsStateCalc(device);
            TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                Brush DeviceBackgroundColor =
                    new SyncUnitErrorToBackground().Convert(someSyncUnitHasError, null, null, null)
                    as Brush;
                Brush DeviceForegroundColor =
                    new SyncUnitErrorToForeground().Convert(someSyncUnitHasError, null, null, null)
                    as Brush;
                border.Background = DeviceBackgroundColor;
                userControl.Foreground = DeviceForegroundColor;
            });
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

        private void OpenDeviceDetails(object sender, System.Windows.RoutedEventArgs e)
        {
            Window window = new Window();
            Grid grid = new Grid();
            TcoEthercatMasterDeviceTopologyView device = new TcoEthercatMasterDeviceTopologyView();
            device.DataContext = this.DataContext;
            grid.Children.Add(device);
            window.Content = grid;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.MaxHeight = 1200;
            window.Show();
        }
    }
}
