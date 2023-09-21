using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TcoCore;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping
{
        public class PlainTcoMessageExtended : PlainTcoMessage
        {
            public DateTime? TimeStampAcknowledged { get; set; }
        }
    }
