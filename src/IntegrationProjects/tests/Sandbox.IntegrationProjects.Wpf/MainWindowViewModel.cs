using IntegrationProjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using System.Linq;
using Vortex.Presentation.Wpf;
using TcOpen.Inxton.Swift.Wpf;

namespace Sandbox.IntegrationProjects.Wpf
{    
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            SwiftRecordVm = new SwiftRecorderViewModel(IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._ST001);

            IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._ST001._serviceModeTask.Execute(null);
            System.Threading.Thread.Sleep(1000);
            IntegrationProjectsPlc.MAIN_TECHNOLOGY._technology._ST001._groundSequenceTask.Execute(null);            
        }

        public SwiftRecorderViewModel SwiftRecordVm { get; }

        public IntegrationProjectsTwinController IntegrationProjectsPlc { get; } = Entry.IntegrationProjectsPlc;       
    }
}
