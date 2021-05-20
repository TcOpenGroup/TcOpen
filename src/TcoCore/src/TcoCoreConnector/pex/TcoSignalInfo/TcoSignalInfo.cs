using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcoCore
{
    public partial class TcoSignalInfo
    {        
        IVortexElement signal;
       
        [IgnoreReflection]
        public IVortexElement Signal 
        {
            get
            {
                lock (this)
                { 
                    if (signal == null)
                    {
                        if (this.Connector is Vortex.Connector.DummyConnector)
                        {
                            return null;
                        }

                        // This may affect performance!
                        if (this.Connector != null)
                        { 
                            var plc_signal_symbol = this.SymbolPath.Synchron;
                            if(!string.IsNullOrEmpty(plc_signal_symbol))
                            { 
                                signal = this?.Connector?.OnlineTags?.Where(p => p.Symbol == plc_signal_symbol).FirstOrDefault();
                            }
                        }
                    }
                }

                return signal; 
            } 
        } 
            
        
    }
}
