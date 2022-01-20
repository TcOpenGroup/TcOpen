using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoInspectors
{
    public partial class TcoDigitalInspector
    {
        public eInspectorResult ResultAsEnum { get { return (eInspectorResult)this._data.Result.Synchron; } }
    }
}
