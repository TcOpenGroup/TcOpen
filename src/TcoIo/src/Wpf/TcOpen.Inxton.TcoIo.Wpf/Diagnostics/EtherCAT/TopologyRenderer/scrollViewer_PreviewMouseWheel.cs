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
        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (GroupedView == false)
            {
                if (Keyboard.Modifiers == ModifierKeys.Control)
                {
                    if (e.Delta > 0)
                    {
                        zoom = zoom * (1.0 + e.Delta / 1200.0);
                    }
                    else if (e.Delta < 0)
                    {
                        zoom = zoom / (1.0 - e.Delta / 1200.0);
                    }
                    if (zoom > 8.0)
                        zoom = 8.0;
                    if (zoom < 0.125)
                        zoom = 0.125;
                    (grid.LayoutTransform as ScaleTransform).ScaleX = zoom;
                    (grid.LayoutTransform as ScaleTransform).ScaleY = zoom;
                    e.Handled = true;
                }
                else if (Keyboard.Modifiers == ModifierKeys.Shift)
                {
                    if (e.Delta > 0)
                    {
                        scrollViewer.LineLeft();
                    }
                    else if (e.Delta < 0)
                    {
                        scrollViewer.LineRight();
                    }
                    e.Handled = true;
                }
            }
            else
            {
                if (this.grid != null && this.grid.Children != null && this.grid.Children.Count > 0)
                {
                    Grid child_0 = this.grid.Children[0] as Grid;
                    if (child_0 != null && child_0.Children != null && child_0.Children.Count > 0)
                    {
                        UniformGrid child_0_0 = child_0.Children[0] as UniformGrid;
                        UniformGrid child_0_1 = child_0.Children[1] as UniformGrid;

                        if (
                            child_0_1 != null
                            && child_0_1.Children != null
                            && child_0_1.Children.Count > 0
                        )
                        {
                            EtcGroupedView child_0_1_0 = child_0_1.Children[0] as EtcGroupedView;
                            if (child_0_1_0 != null)
                            {
                                ScrollViewer groupScrollViewer =
                                    child_0_1_0.groupViewScrollViewer as ScrollViewer;
                                if (groupScrollViewer != null)
                                {
                                    if (e.Delta > 0)
                                    {
                                        groupScrollViewer.LineUp();
                                    }
                                    else if (e.Delta < 0)
                                    {
                                        groupScrollViewer.LineDown();
                                    }
                                    e.Handled = true;
                                }
                            }
                        }
                        else if (
                            child_0_0 != null
                            && child_0_0.Children != null
                            && child_0_0.Children.Count > 0
                        )
                        {
                            EtcGroupedView child_0_0_0 = child_0_0.Children[0] as EtcGroupedView;
                            if (child_0_0_0 != null)
                            {
                                ScrollViewer groupScrollViewer =
                                    child_0_0_0.groupViewScrollViewer as ScrollViewer;
                                if (groupScrollViewer != null)
                                {
                                    if (e.Delta > 0)
                                    {
                                        groupScrollViewer.LineUp();
                                    }
                                    else if (e.Delta < 0)
                                    {
                                        groupScrollViewer.LineDown();
                                    }
                                    e.Handled = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
