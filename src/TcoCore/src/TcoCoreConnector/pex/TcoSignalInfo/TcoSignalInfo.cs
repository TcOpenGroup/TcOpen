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
                        if (this?.Connector is Vortex.Connector.DummyConnector)
                        {
                            return null;
                        }
                        
                        // This may affect performance!
                        if (this.Connector != null)
                        {                           
                                var plc_signal_symbol = this.SymbolPath.Synchron;
                                if (!string.IsNullOrEmpty(plc_signal_symbol))
                                {
                                    //Search changed to StartsWith because `Tc3_JsonXml.FB_JsonReadWriteDatatype`
                                    //in TcoSignalInfo  is unable to retrieve complete path in some instances.
                                    // This can be a bug in `Tc3_JsonXml.FB_JsonReadWriteDatatype` that is outside our reach.
                                    signal = this?.Connector?.OnlineTags?.Where(p => p.Symbol.StartsWith(plc_signal_symbol)).FirstOrDefault();
                                }                           
                        }
                    }
                }

                return signal; 
            } 
        } 
            
        
    }
}
