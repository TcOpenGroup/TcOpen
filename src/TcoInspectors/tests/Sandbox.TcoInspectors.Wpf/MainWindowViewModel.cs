using TcoInspectors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using System.Linq;
using TcoInspectorsTests;
using TcOpen.Inxton.Input;

namespace Sandbox.TcoInspectors.Wpf
{    
    public class MainWindowViewModel 
    {
        public MainWindowViewModel()
        {
            this.FromOnlineToShadowCommand = new RelayCommand(a => FromOnlineToShadow());
            this.FromShadowToOnlineCommand = new RelayCommand(a => FromShadowToOnline());
        }
        
        public TcoInspectorsTestsTwinController TcoInspectorsPlc { get; } = App.Plc;     
        

        public RelayCommand FromOnlineToShadowCommand { get; }
        public RelayCommand FromShadowToOnlineCommand { get; }

        void FromOnlineToShadow()
        {
            this.TcoInspectorsPlc.MAIN._exampleContext.Data.FlushOnlineToShadow();
        }

        void FromShadowToOnline()
        {
            this.TcoInspectorsPlc.MAIN._exampleContext.Data.FlushShadowToOnline();
        }
    }
}
