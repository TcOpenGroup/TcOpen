using Microsoft.AspNetCore.Components;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using TcOpen.Inxton.VortexElementExtensions;
using TcOpen.Inxton.TcoCore.Blazor;

namespace TcoCore
{
    public partial class TcoTaskControlView
    {
        [Parameter]
        public IVortexObject Vortex { get; set; }

        TcoTaskControlViewModel ViewModel { get; set; } = new TcoTaskControlViewModel();

        protected override void OnInitialized()
        {
            ViewModel.Model = Vortex;
            UpdateValuesOnChange(ViewModel.Component);            
            ViewModel.Component._taskState.Subscribe(TaskStateChanged);
            ButtonState = ViewModel.Component.StateToButtonClass();
        }        

        public string ButtonState { get; set; } = "btn btn-secondary";

        public string ButtonCaption { get { return Vortex.GetNameOrSymbol(); } }

        private void TaskStateChanged(IValueTag sender, ValueChangedEventArgs args)
        {
            ButtonState = ViewModel.Component.StateToButtonClass();            
        }
    }
}
