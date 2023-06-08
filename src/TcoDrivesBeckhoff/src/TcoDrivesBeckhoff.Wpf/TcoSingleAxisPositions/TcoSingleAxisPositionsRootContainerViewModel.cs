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
    public class TcoSingleAxisPositionsRootContainerViewModel : RenderableViewModel
    {
        public TcoSingleAxisPositionsRootContainerViewModel()
        {

        }

        public TcoSingleAxisPositionsRootContainer Component { get; private set; }

        public ObservableCollection<TcoSingleAxisMoveParam> Positions { get { return Extensions.ToObservableCollection<TcoSingleAxisMoveParam>(((IVortexObject)Model).GetChildren().OfType<TcoSingleAxisMoveParam>()); } }
        public TcoSingleAxisMoveParam SelectedItem { get; set; }


        public override object Model { get => this.Component; set { this.Component = value as TcoSingleAxisPositionsRootContainer; } }
    }


}
