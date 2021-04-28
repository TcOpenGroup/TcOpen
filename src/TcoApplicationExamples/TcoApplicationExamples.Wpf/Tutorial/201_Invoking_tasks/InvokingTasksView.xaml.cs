using PlcAppExamples;
using System.Windows.Controls;
using Vortex.Presentation.Wpf;

namespace PlcAppExamples
{
    public partial class InvokingTasksView : UserControl
    {
        public InvokingTasksView()
        {
            InitializeComponent();
        }
    }

    public class InvokingTasksViewModel : RenderableViewModel
    {

        public InvokingTasks InvokingTasks { get; set; }
        public override object Model { get => InvokingTasks; set => InvokingTasks = value as InvokingTasks; }
    }
}
