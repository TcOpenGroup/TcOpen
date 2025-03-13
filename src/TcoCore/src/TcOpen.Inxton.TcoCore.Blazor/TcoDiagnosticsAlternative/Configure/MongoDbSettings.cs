using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Configure
{
    public class MongoDbSettings
    {
        [Required]
        public string MongoUri { get; set; }
        [Required]
        public string DatabaseName { get; set; }
        [Required]
        public string CollectionName { get; set; }
    }

}
