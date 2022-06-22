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

namespace TcOpen.Inxton.TcoIo.Wpf
{
    public class TopologyRenderer : UserControl
    {

        public TopologyRenderer() : base()
        {
            this.DataContextChanged += TopologyRenderer_DataContextChanged;           
        }

        private void TopologyRenderer_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.Content = Render(this.DataContext as IVortexObject);
        }

        public object Render(IVortexObject obj, int row = 0, int column = 0, Grid mainGrid = null, UniformGrid cell = null)
        {

            if (obj != null)
            {
                mainGrid = mainGrid == null ? new Grid() : mainGrid;
                cell = cell == null ? new UniformGrid()  : cell;


                //var presentation = LazyRenderer.Get.CreatePresentation("TopologyBase-TopologyBox-TopologyTerminal", obj) as UIElement;
                var presentation = LazyRenderer.Get.CreatePresentation("TopologyBase", obj) as UIElement;

                if (presentation != null)
                {
                    cell.Children.Add(presentation);
                }

                mainGrid.Children.Add(cell);

                Grid.SetColumn(cell, column++);
                Grid.SetRow(cell, row++);

                mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                mainGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

                var physics = obj.GetType().GetProperty("Physics")?.GetValue(obj);


                foreach (var child in obj.GetChildren())
                {
                    // cell new

                    Render(child, row, column, mainGrid, new UniformGrid());
                }
            }
            return mainGrid;
        }        

    }

}
