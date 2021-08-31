using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;

namespace TcOpen.Inxton.Local.Security.Blazor.Users
{
    public class RoleModel : IBrowsableDataObject
    {
        public string ConcurrencyStamp { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public dynamic _recordId { get; set ; }
        public DateTime _Created { get; set ; }
        public string _EntityId { get; set; }
        public DateTime _Modified { get ; set; }
    }
}
