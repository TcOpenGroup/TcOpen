using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcOpen
{
    public partial class fbCylinder
    {
        public override bool Simulate()
        {   
            if (this.outToHomePos.Synchron)
            {
                this.inAtWorkPos.Synchron = false;
                Thread.Sleep(1000);
                this.inAtHomePos.Synchron = true;            
            }

            if (this.outToWorkPos.Synchron)
            {
                this.inAtHomePos.Synchron = false;
                Thread.Sleep(1000);
                this.inAtWorkPos.Synchron = true;        
            }

            return true;
        }
    }
}
