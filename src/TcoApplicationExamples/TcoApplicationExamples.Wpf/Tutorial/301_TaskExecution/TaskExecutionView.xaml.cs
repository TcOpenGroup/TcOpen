using PlcAppExamples;
using System.Windows.Controls;
using Vortex.Presentation.Wpf;

namespace PlcAppExamples
{
    public partial class TaskExecutionView : UserControl
    {
        public TaskExecutionView()
        {
            InitializeComponent();
        }
    }

    public class TaskExecutionViewModel : RenderableViewModel
    {
        public TaskExecution TaskExecution { get; set; }
        public override object Model { get => TaskExecution; set => TaskExecution = value as TaskExecution; }
    }
}
