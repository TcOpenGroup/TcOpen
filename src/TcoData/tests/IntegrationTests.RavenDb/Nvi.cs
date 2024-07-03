using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Abstractions.Data;

namespace IntegrationTests.RavenDb
{
    public class Nvi : IBrowsableDataObject
    {
        public string Id { get; set; }

        public dynamic _recordId { get; set; }

        public DateTime _Created { get; set; }

        public string _EntityId { get; set; }

        public DateTime _Modified { get; set; }
    }
}
