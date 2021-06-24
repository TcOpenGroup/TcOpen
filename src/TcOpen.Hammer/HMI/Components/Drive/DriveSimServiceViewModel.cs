using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation.Wpf;

namespace PlcHammer
{
    public class DriveSimServiceViewModel : RenderableViewModel
    {
        public DriveSimServiceViewModel()
        {
           
        }


        public PlainDriveSim GetLogPayload()
        {
            var plain = this.Component.CreatePlainerType();
            this.Component.FlushOnlineToPlain(plain);
            return plain;
        }

        public DriveSim Component { get; private set; }

        public override object Model { get { return Component; } set { Component = value as DriveSim; Component._moveAbsoluteTask.LogPayloadDecoration = () => GetLogPayload(); } }
    }
}
