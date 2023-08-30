using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TcoCore.TcoDiagnosticsAlternative.LoggingToDb;

namespace TcoCore
{
    public class TcoObjectDiagnosticsAlternativeView : TcoDiagnosticsAlternativeView { }

    public class TcoObjectDiagnosticsAlternativeViewModel : TcoDiagnosticsAlternativeViewModel
    {
        public TcoObjectDiagnosticsAlternativeViewModel() : base() { }

        public TcoObjectDiagnosticsAlternativeViewModel(IMongoLogger logger) : base(logger) { }
    }
}
