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
        public void UpdateInfoDataStates(IValueTag sender, ValueChangedEventArgs args)
        {
            SummaryInfoDataState = 0;
            FirstTopologyElementReached = false;
            LastTopologyElementReached = false;

            CalculateInfoDataStates(
                dt,
                ref SummaryInfoDataState,
                ref groupedViewItems,
                ref syncUnitError
            );
            TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                TopologyObject _topologyObject = new TopologyObject();
                int index = 0;
                foreach (TopologyObject topologyObject in topologyObjects)
                {
                    if (
                        ValidateFrameworkElement
                            .Name(topologyObject.Name)
                            .Equals(ValidateFrameworkElement.Name(GroupName))
                    )
                    {
                        _topologyObject = topologyObject;
                        index++;
                        break;
                    }
                }
                if (
                    _topologyObject != null
                    && _topologyObject.Hardware != null
                    && _topologyObject.Hardware.Children != null
                    && _topologyObject.Hardware.Children.Count > 0
                    && _topologyObject.Hardware.Children[0] != null
                )
                {
                    EtcGroupedView etcGroupedView =
                        _topologyObject.Hardware.Children[0] as EtcGroupedView;
                    if (etcGroupedView != null)
                    {
                        EtcGroupedDataContext groupContext =
                            etcGroupedView.DataContext as EtcGroupedDataContext;
                        if (groupContext != null)
                        {
                            EtcGroupedViewData context =
                                groupContext.GroupedViewData as EtcGroupedViewData;
                            if (context != null)
                            {
                                context.GroupedViewItems = groupedViewItems;
                                context.InfoDataState = SummaryInfoDataState;
                                context.IsInErrorState = syncUnitError || SummaryInfoDataState != 8;
                                topologyObjects.ElementAtOrDefault(index - 1).Hardware.DataContext =
                                    context;
                            }
                        }
                    }
                }
            });
        }

        private void CalculateInfoDataStates(
            IVortexObject vortexObject,
            ref ushort SummaryInfoDataState,
            ref List<GroupedViewItemObject> groupedViewItems,
            ref bool syncUnitError
        )
        {
            if (vortexObject != null)
            {
                Type[] interfaces = vortexObject.GetType().GetInterfaces();

                bool isMaster = false;
                bool isSlave = false;

                CheckIfIsEtcMasterOrEtcSlave(vortexObject, out isMaster, out isSlave);

                //If value of the first topology element is not defined, first slave from the data context given is going to be used as a first topology element
                if (String.IsNullOrEmpty(FirstTopologyElementName))
                {
                    FirstTopologyElementReached = true;
                }
                //First topology element found
                else if (
                    FirstTopologyElementName.Equals(
                        ValidateFrameworkElement.Name(vortexObject.AttributeName)
                    )
                )
                {
                    FirstTopologyElementReached = true;
                }

                if (GroupedView == true)
                {
                    if (isMaster)
                    {
                        syncUnitError = SyncUnitsStateCalc(vortexObject);
                    }
                    else if (isSlave)
                    {
                        //Evaluate individual InfoData.State variable and product SummaryInfoDataState
                        if (FirstTopologyElementReached && !LastTopologyElementReached)
                        {
                            IVortexObject InfoData =
                                vortexObject
                                    .GetKids()
                                    .Where(i => i.AttributeName.Contains("InfoData"))
                                    .FirstOrDefault() as IVortexObject;

                            if (InfoData != null)
                            {
                                IVortexElement State = InfoData
                                    .GetKids()
                                    .Where(i => i.AttributeName.Contains("State"))
                                    .FirstOrDefault();

                                if (State != null)
                                {
                                    ushort state = (State as IOnlineUInt).Synchron;
                                    foreach (
                                        GroupedViewItemObject groupedViewItem in groupedViewItems
                                    )
                                    {
                                        if (vortexObject.AttributeName.Equals(groupedViewItem.Name))
                                        {
                                            SummaryInfoDataState = (ushort)(
                                                SummaryInfoDataState | state
                                            );
                                            groupedViewItem.IsInErrorState = state != 8;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //If value of the last topology element is not defined, last child from the data context given is going to be used as a last topology element
                if (
                    !String.IsNullOrEmpty(LastTopologyElementName)
                    && LastTopologyElementName.Equals(
                        ValidateFrameworkElement.Name(vortexObject.AttributeName)
                    )
                )
                {
                    LastTopologyElementReached = true;
                }
                foreach (IVortexObject child in vortexObject.GetChildren())
                {
                    CheckIfIsEtcMasterOrEtcSlave(child, out isMaster, out isSlave);
                    if (isSlave)
                    {
                        CalculateInfoDataStates(
                            child,
                            ref SummaryInfoDataState,
                            ref groupedViewItems,
                            ref syncUnitError
                        );
                    }
                }
            }
        }
    }
}
