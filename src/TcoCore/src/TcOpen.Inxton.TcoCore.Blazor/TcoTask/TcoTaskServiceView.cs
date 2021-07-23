using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.VortexElementExtensions;
using TcOpen.Inxton.TcoCore.Blazor;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoCore
{
    public partial class TcoTaskServiceView 
    {
        public string ButtonState { get; set; } = "btn btn-secondary";

        public string ButtonCaption { get { return Vortex.GetNameOrSymbol(); } }

        private void TaskStateChanged(IValueTag sender, ValueChangedEventArgs args)
        {
            ButtonState = Vortex.StateToButtonClass();
        }

        public bool IsDisabled
        {
            get
            {
                return !this.Vortex._enabled.Cyclic;
            }
        }

        protected override void OnInitialized()
        {
            Vortex._taskState.Subscribe(TaskStateChanged);
            UpdateValuesOnChange(Vortex);
        }
    }
}
