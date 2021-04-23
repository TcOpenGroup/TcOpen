using System;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using Vortex.Presentation.Wpf;

namespace PlcAppExamples
{
    public partial class ConnectSequencesView : UserControl
    {
        public ConnectSequencesView()
        {
            InitializeComponent();
        }
    }
    public class ConnectSequencesViewModel : RenderableViewModel
    {
        public ConnectSequencesViewModel()
        {

        }

        public ConnectSequences ConnectSequences { get; set; }

        public override object Model { get => ConnectSequences; set => ConnectSequences = value as ConnectSequences; }
    }

 

}
