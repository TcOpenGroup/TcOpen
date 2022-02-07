using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoInspectors;

namespace TcoInspectorsTests
{
    public partial class TcoInspectorTests
    {
        public IsInspector Inspector { get { return ((dynamic)this)._sut; } }
    }
}
