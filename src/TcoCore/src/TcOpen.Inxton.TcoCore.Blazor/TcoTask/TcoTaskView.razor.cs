using Microsoft.AspNetCore.Components;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using TcOpen.Inxton.VortexElementExtensions;
using TcOpen.Inxton.TcoCore.Blazor;

namespace TcoCore
{
    public partial class TcoTaskView
    {


        protected override void OnInitialized()
        {
            UpdateValuesOnChange(ViewModel.Component);            
            ViewModel.Component._taskState.Subscribe(TaskStateChanged);
            ButtonState = ViewModel.Component.StateToButtonClass();
        }        

        public string ButtonState { get; set; } = "btn btn-secondary";

        public string ButtonCaption { get { return ViewModel.Component.GetNameOrSymbol(); } }

        public bool IsTaskRunning { get; set; }

        private void TaskStateChanged(IValueTag sender, ValueChangedEventArgs args)
        {
            ButtonState = ViewModel.Component.StateToButtonClass();
            IsTaskRunning = ViewModel.Component.GetTaskState();
        }
    }
}
