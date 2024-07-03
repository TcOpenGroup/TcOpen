using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using TcOpen.Inxton.TcoCore.Wpf;
using TcOpen.Inxton.TcoIo.Wpf.Diagnostics.EtherCAT;
using Vortex.Connector;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TopologyRenderer : UserControl
    {
        private void RenderCompleteTopology()
        {
            if (dt != null && !alreadyRendered)
            {
                TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
                {
                    Rendering = true;
                    if (Generating == null)
                    {
                        Generating = new Generating();
                        Generating.Owner = Application.Current.MainWindow;
                    }
                    Generating.Show();
                    FirstTopologyElementReached = false;
                    LastTopologyElementReached = false;
                    groupedViewItems = new List<GroupedViewItemObject>();

                    FindObjectsAndSubscribeToInfoDataState(dt);

                    PrepareHardware(dt);

                    FilterOutExcludedHardware();

                    Grid wiring = PrepareWiring() as Grid;
                    this.grid.Children.Clear();
                    this.grid.Children.Add(Render(wiring) as Grid);
                    Rendering = false;
                });
            }
        }
    }
}
