using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoInspectors
{
    public partial class TcoInspectionGroup
    {
        public eOverallResult ResultAsEnum { get { return (eOverallResult)this._result.Result.Synchron; } }
       
    }
}
