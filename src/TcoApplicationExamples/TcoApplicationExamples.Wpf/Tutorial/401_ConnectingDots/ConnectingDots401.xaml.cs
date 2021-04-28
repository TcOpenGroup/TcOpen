using System.Windows.Controls;
using Vortex.Presentation.Wpf;
using System.Linq;
using TcoCore;

namespace PlcAppExamples
{
    public partial class ConnectingDots401 : UserControl
    {
        public ConnectingDots401()
        {
            InitializeComponent();
        }

        private void OnRendered(object sender, System.Windows.SizeChangedEventArgs e)
        {
            var diagViewModel = sender
                 .As<RenderableContentControl>()
                 .Content
                 .As<Panel>()
                 .Children[0]
                 .As<ContentControl>()
                 .Content
                 .As<ContentControl>()
                 .DataContext
                 .As<TcoContextDiagnosticsViewModel>();

            if (SubscirbeOnce)
            {
                SubscirbeOnce = false;
                Entry.PlcAppExamples.MAIN_PRG._TcOpenTutorial.Station001._sequence._currentStep.ID.Subscribe((a, b) => diagViewModel.UpdateMessagesCommand.Execute(null));
            }
        }
        public bool SubscirbeOnce { get; set; } = true;
    }

    public static class Ext
    {
        public static T As<T>(this object @object) where T : class => (@object as T);
    }
}
