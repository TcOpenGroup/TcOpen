using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace TcoApplicationExamples.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Clicking on links in markdown will open browser.
            CommandBindings.Add(new CommandBinding(
            NavigationCommands.GoToPage,
            (sender, e) =>
            {
                var proc = new Process();
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = (string)e.Parameter;

                proc.Start();
            }));
        }
    }
}
