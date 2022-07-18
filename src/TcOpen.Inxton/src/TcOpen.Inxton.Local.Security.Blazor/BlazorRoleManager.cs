using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Local.Security.Blazor.Users;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security.Blazor
{
    public class BlazorRoleManager
    {
        public List<Role> InAppRoleCollection { get; set; } = new List<Role>();

        //public List<string> InAppGroupCollection { get; set; } = new List<string>();

        
        //public string GetGroupRoleString(string groupString)
        //{
        //    var roles = InAppRoleCollection.Where(x => x.DefaultGroup == groupString).Select(x => x.Name);
        //    return String.Join(",", roles);
        //}
        public void CreateRole(Role role)
        {
            this.InAppRoleCollection.Add(role);
            //if (!InAppGroupCollection.Contains(role.DefaultGroup)) this.InAppGroupCollection.Add(role.DefaultGroup);
        }
    }
}
