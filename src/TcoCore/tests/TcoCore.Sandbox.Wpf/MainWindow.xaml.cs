using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TcoCoreExamples;

namespace TcoCore.Sandbox.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = TcoCoreExamples.Entry.PlcTcoCoreExamples;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Entry.PlcTcoCoreExamples.MAIN._station001._sequence._stepModeControler._State.Cyclic == (short)TcoCore.eSequencerMode.StepMode)
            {
                Entry.PlcTcoCoreExamples.MAIN._station001._sequence._stepModeControler._State.Cyclic = (short)TcoCore.eSequencerMode.CyclicMode;
            }
            else
            {
                Entry.PlcTcoCoreExamples.MAIN._station001._sequence._stepModeControler._State.Cyclic = (short)TcoCore.eSequencerMode.StepMode;
            }
        }

        private void StepBackward_Click(object sender, RoutedEventArgs e)
        {
            Entry.PlcTcoCoreExamples.MAIN._station001._sequence._stepModeControler._stepBackward._invokeRequest.Cyclic = true;
        }

        private void StepForward_Click(object sender, RoutedEventArgs e)
        {
            Entry.PlcTcoCoreExamples.MAIN._station001._sequence._stepModeControler._stepForward._invokeRequest.Cyclic = true;
        }

        private void StepIn_Click(object sender, RoutedEventArgs e)
        {
            Entry.PlcTcoCoreExamples.MAIN._station001._sequence._stepModeControler._stepIn._invokeRequest.Cyclic = true;
        }
    }
}
