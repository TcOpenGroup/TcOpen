using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Configure
{
    public class MongoDbSettings
    {
        public string MongoUri { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }

}
