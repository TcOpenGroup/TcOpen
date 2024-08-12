using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoInspectors
{
    public partial class TcoAnalogueInspector : IsInspector
    {
        public eInspectorResult ResultAsEnum
        {
            get { return (eInspectorResult)this._data.Result.Synchron; }
        }

        public IsInspectorData InspectorData => this._data;
    }
}
