namespace MainPlc
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using TcoCore;
    using TcOpen.Inxton;
    using TcOpen.Inxton.Input;
    using Vortex.Connector;


    public class CUBaseMiniDiagnosticsViewModel : TcoDiagnosticsViewModel
    {
        public CUBaseMiniDiagnosticsViewModel() : base()
        {
            this.AutoUpdate = true;
            this.DiagnosticsDepth = 15;
            this.MinMessageCategoryFilter = eMessageCategory.Notification;
        }
    }
}
