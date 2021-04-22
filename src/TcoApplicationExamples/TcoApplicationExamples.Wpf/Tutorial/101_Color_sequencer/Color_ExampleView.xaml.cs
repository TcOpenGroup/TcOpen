using System.Windows.Controls;
using Vortex.Presentation.Wpf;

namespace PlcAppExamples
{
    public partial class Color_ExampleView : UserControl
    {
        public Color_ExampleView()
        {
            InitializeComponent();
        }
    }
    public class Color_ExampleViewModel : RenderableViewModel
    {
        public Color_ExampleViewModel()
        {

        }

        public Color_Example Color_Example { get; set; }

        public override object Model { get => Color_Example; set => Color_Example = value as Color_Example; }
    }
}
