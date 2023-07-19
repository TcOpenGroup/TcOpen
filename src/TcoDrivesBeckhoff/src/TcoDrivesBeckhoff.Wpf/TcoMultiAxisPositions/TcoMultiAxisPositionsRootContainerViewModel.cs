using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoDrivesBeckhoff
{
    public class TcoMultiAxisPositionsRootContainerViewModel : RenderableViewModel
    {
        public TcoMultiAxisPositionsRootContainerViewModel()
        {

        }

        public TcoMultiAxisPositionsRootContainer Component { get; private set; }

        public ObservableCollection<TcoMultiAxisMoveParam> Positions { get { return Extensions.ToObservableCollection<TcoMultiAxisMoveParam>(((IVortexObject)Model).GetChildren().OfType<TcoMultiAxisMoveParam>()); } }
        public TcoMultiAxisMoveParam SelectedItem { get; set; }


        public override object Model { get => this.Component; set { this.Component = value as TcoMultiAxisPositionsRootContainer; } }
    }


}
