using System.Linq;
using Vortex.Presentation.Wpf;

namespace MainPlc
{
    public class TcoTaskedServiceComponentsViewModel : RenderableViewModel
    {
        public TcoTaskedServiceComponentsViewModel()
        {

        }

        TcoTaskedService component;
        public TcoTaskedService Component
        {
            get => component;
            private set
            {
                if (component == value)
                {
                    return;
                }

                SetProperty(ref component, value);
            }
        }

        public object Serviceable { get { return Component.GetParent().GetChildren().Where(p => p is CUComponentsBase).FirstOrDefault(); } }

        public override object Model { get => Component; set => Component = (TcoTaskedService)value; }
    }
}
