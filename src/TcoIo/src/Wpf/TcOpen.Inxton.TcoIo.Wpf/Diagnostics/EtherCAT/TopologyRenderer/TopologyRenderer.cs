using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Vortex.Connector;
using Vortex.Presentation.Wpf;
using TcoIo;
using System.Reflection;

namespace TcOpen.Inxton.TcoIo.Wpf
{
    public class TopologyRenderer : UserControl
    {
        static int row = 0;
        static int column = 0;

        public TopologyRenderer() : base()
        {
            this.DataContextChanged += TopologyRenderer_DataContextChanged;           
        }

        public string PresentationType { get; set; }

        public enum HardwareType
        {
            Undefined,
            EtcMasterBase,
            EtcSlaveBoxBase,
            EtcSlaveTerminalBase
        }

        private void TopologyRenderer_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.Content = Render(this.DataContext as IVortexObject, ref row, ref column);
        }


        public object Render(IVortexObject obj, ref int row , ref int column , Grid mainGrid = null, UniformGrid cell = null, HardwareType previousType = HardwareType.Undefined)
        {

            if (obj != null)
            {
                mainGrid = mainGrid == null ? new Grid() : mainGrid;
                cell = cell == null ? new UniformGrid()  : cell;

                HardwareType currentType = new HardwareType();

                Type[] interfaces = obj.GetType().GetInterfaces();
                foreach (Type item in interfaces)
                {
                    if (item.Name.Contains("EtcMasterBase"))
                    {
                        currentType = HardwareType.EtcMasterBase;
                        break;
                    }
                    else if (item.Name.Contains("EtcSlaveBoxBase"))
                    {
                        currentType = HardwareType.EtcSlaveBoxBase;
                        break;
                    }
                    else if (item.Name.Contains("EtcSlaveTerminalBase"))
                    {
                        currentType = HardwareType.EtcSlaveTerminalBase;
                        break;
                    }
                }

                var presentation = LazyRenderer.Get.CreatePresentation(PresentationType, obj) as UIElement;

                if (presentation != null && currentType != HardwareType.Undefined)
                {
                    cell.Children.Add(presentation);

                    mainGrid.Children.Add(cell);

                    if (currentType == HardwareType.EtcMasterBase /*&& (previousType == HardwareType.EtcSlaveBoxBase || previousType == HardwareType.EtcSlaveTerminalBase)*/)
                    {
                        column = 0;
                        row++;
                    }
                    else if (currentType == HardwareType.EtcSlaveBoxBase && previousType == HardwareType.EtcMasterBase)
                    {
                        column++;
                    }
                    else if (currentType == HardwareType.EtcSlaveBoxBase && previousType == HardwareType.EtcSlaveTerminalBase)
                    {
                        row++;
                    }
                    else if (currentType == HardwareType.EtcSlaveTerminalBase /*&& previousType == HardwareType.EtcSlaveTerminalBase*/)
                    {
                        column++;
                    }
                    Grid.SetColumn(cell, column);
                    Grid.SetRow(cell, row);
                    Grid.SetIsSharedSizeScope(mainGrid, true);
                    Grid.SetIsSharedSizeScope(cell, true);
                    cell.HorizontalAlignment = HorizontalAlignment.Stretch;
                    cell.VerticalAlignment = VerticalAlignment.Stretch;

                    mainGrid.ColumnDefinitions.Add(new ColumnDefinition()/* { Width = GridLength.Auto }*/);
                    mainGrid.RowDefinitions.Add(new RowDefinition() /*{ Height = GridLength.Auto }*/);

                    var physics = obj.GetType().GetProperty("Physics")?.GetValue(obj);
                }

                foreach (var child in obj.GetChildren())
                {
                    // cell new

                    Render(child, ref row, ref column, mainGrid, new UniformGrid(), currentType);
                }
            }
            return mainGrid;
        }        

    }

}
