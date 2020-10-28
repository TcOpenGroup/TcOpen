using System;
using System.Linq;
using System.Threading;

namespace TcOpen
{
    public partial class fbCylinder
    {
        public int TimeToReachPosition { get; set; } = 500;

        internal override bool Simulate()
        {
            if (this.outToHomePos.Cyclic)
            {
                this.inAtWorkPos.Synchron = false;
                Thread.Sleep(TimeToReachPosition);
                this.inAtHomePos.Synchron = true;
            }
                      
            if (this.outToWorkPos.Cyclic)
            {
                this.inAtHomePos.Synchron = false;
                Thread.Sleep(TimeToReachPosition);
                this.inAtWorkPos.Synchron = true;
            }
                     
            return true;
        }
    }
}
