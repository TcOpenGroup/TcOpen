using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCore;
using Vortex.Presentation;
using Vortex.Presentation.Blazor.Interfaces;

namespace TcoCore
{

    public class TcoTaskViewModel : RenderableViewModelBase
    {
        public TcoTaskViewModel()
        {
            
        }


        public bool IsDisabled
        {
            get
            {
                return !this.Component._enabled.Cyclic;    
            }
        }

        public TcoTask Component { get; private set; }

        public override object Model { get => this.Component; set { this.Component = value as TcoTask; this.Component._enabled.Subscribe((a, b) => this.OnPropertyChanged(nameof(IsDisabled))); } }


    }



}
