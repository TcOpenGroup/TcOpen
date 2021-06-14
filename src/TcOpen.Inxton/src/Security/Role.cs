using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Security
{
    public class Role
    {
        public string Name { get; private set; }
        public string DefaultGroup { get; private set; }

        public Role(string Name, string DefaultGroup)
        {
            this.Name = Name;
            this.DefaultGroup = DefaultGroup;
        }

        public override string ToString() => Name;
    }
}
