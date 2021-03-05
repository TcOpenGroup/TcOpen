using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcoCore
{
    public partial class TcoMessage : INotifyPropertyChanged
    {        
        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            _context = parent.GetParent<TcoContext>();

            // TODO: This is temporary should be made in in VortexBase library
            Task.Run(() =>
            {
                while(true)
                { 
                    System.Threading.Thread.Sleep(2000);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsActive)));
                }

            });
        }

        private ITcoContext _context;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsActive
        {
            get
            {
                return _context?._startCycleCount.Cyclic <= this.Cycle.Cyclic;
            }
        }

        public PlainTcoMessage Message
        {
            get
            {
                var plain = this.CreatePlainerType();
                this.FlushOnlineToPlain(plain);
                return plain;
            }
        }
    }
}
