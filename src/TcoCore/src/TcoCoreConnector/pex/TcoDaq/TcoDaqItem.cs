using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcoCore
{
    public partial class TcoDaqItem
    {       
        /// <summary>
        /// Gets the last known log entry content in plain .net type system (aka POCO object) with object retieved by identity.
        /// </summary>
        public PlainTcoDaqItem LastKnownPlain
        {
            get
            {
                var plain = this.CreatePlainerType();
                plain.CopyCyclicToPlain(this);                                       
                return plain;
            }
        }        
    }
}
