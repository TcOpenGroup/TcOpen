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
        public void SubscribeToInfoDataState(
            IVortexObject dt,
            IVortexObject firstElement,
            IVortexObject lastElement,
            bool excludeSlavesConnectedToJunctionBox
        )
        {
            bool isMaster = false;
            bool isSlave = false;

            CheckIfIsEtcMasterOrEtcSlave(lastElement, out isMaster, out isSlave);

            if (excludeSlavesConnectedToJunctionBox)
            {
                if (isMaster)
                {
                    //Subscribing to all individual SyncUnit InfoData.State variables
                    if (lastElement != null)
                    {
                        SubscribeToSyncUnits(lastElement);
                        FirstTopologyElementReached = true;
                    }
                }
                else if (isSlave)
                {
                    //Subscribing to individual InfoData.State variables
                    if (!FirstTopologyElementReached)
                    {
                        IVortexObject InfoData =
                            lastElement
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
                                GroupedViewItemObject item = new GroupedViewItemObject(
                                    lastElement.AttributeName.ToString(),
                                    (StateTag as OnlinerUInt).Synchron != 8
                                );
                                groupedViewItems.Add(item);
                                StateTag.Subscribe(
                                    (sender, arg) => UpdateInfoDataStates(sender, arg)
                                );
                            }
                        }
                        string connectionPartnerName = lastElement
                            .GetType()
                            .GetProperty("AttributePreviousPort")
                            .GetValue(lastElement)
                            .ToString();
                        connectionPartnerName = connectionPartnerName.Substring(
                            0,
                            connectionPartnerName.LastIndexOf(" :")
                        );
                        IVortexObject vortexObject = null;
                        IVortexObject connectionPartner = FindTopologyElement(
                            connectionPartnerName,
                            dt,
                            ref vortexObject
                        );

                        if (
                            ValidateFrameworkElement
                                .Name(lastElement.AttributeName)
                                .Equals(ValidateFrameworkElement.Name(firstElement.AttributeName))
                        )
                        {
                            FirstTopologyElementReached = true;
                            groupedViewItems.Reverse();
                        }
                        if (!FirstTopologyElementReached && connectionPartner != null)
                        {
                            SubscribeToInfoDataState(
                                dt,
                                firstElement,
                                connectionPartner,
                                excludeSlavesConnectedToJunctionBox
                            );
                        }
                    }
                }
            }
            else
            {
                //If value of the first topology element is not defined, first slave from the data context given is going to be used as a first topology element
                if (String.IsNullOrEmpty(FirstTopologyElementName))
                {
                    FirstTopologyElementReached = true;
                }
                //First topology element found
                else if (
                    FirstTopologyElementName.Equals(ValidateFrameworkElement.Name(dt.AttributeName))
                )
                {
                    FirstTopologyElementReached = true;
                }

                if (isMaster)
                {
                    //Subscribing to all individual SyncUnit InfoData.State variables
                    if (firstElement != null)
                    {
                        SubscribeToSyncUnits(firstElement);
                        FirstTopologyElementReached = true;
                    }
                }
                else if (isSlave)
                {
                    //Subscribing to individual InfoData.State variables
                    if (FirstTopologyElementReached && !LastTopologyElementReached)
                    {
                        IVortexObject InfoData =
                            dt.GetKids()
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
                                GroupedViewItemObject item = new GroupedViewItemObject(
                                    dt.AttributeName.ToString(),
                                    (StateTag as OnlinerUInt).Synchron != 8
                                );
                                groupedViewItems.Add(item);
                                StateTag.Subscribe(
                                    (sender, arg) => UpdateInfoDataStates(sender, arg)
                                );
                            }
                        }
                    }
                    //If value of the last topology element is not defined, last child from the data context given is going to be used as a last topology element
                    if (
                        !String.IsNullOrEmpty(LastTopologyElementName)
                        && LastTopologyElementName.Equals(
                            ValidateFrameworkElement.Name(dt.AttributeName)
                        )
                    )
                    {
                        LastTopologyElementReached = true;
                    }
                    if (!LastTopologyElementReached)
                    {
                        IEnumerable<IVortexObject> vortexObjects = dt.GetChildren()
                            .Where(i => i.GetType().GetProperty("AttributePhysics") != null);
                        foreach (IVortexObject child in vortexObjects)
                        {
                            SubscribeToInfoDataState(
                                child,
                                firstElement,
                                lastElement,
                                excludeSlavesConnectedToJunctionBox
                            );
                        }
                    }
                }
            }
        }
    }
}
