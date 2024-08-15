using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Security;
using System.Linq;
using System.Threading;
using TcOpen.Inxton.Local.Security.Properties;
using System.Text;
using System.Security.Cryptography;
using TcOpen.Inxton.Local.Security;

namespace TcOpen.Inxton.Local.Security
{
    public class RoleGroupManager
    {
        private IRepository<GroupData> groupRepo;

        public List<Role> inAppRoleCollection { get; set; } = new List<Role>();

        public RoleGroupManager(IRepository<GroupData> groupRepo)
        {
            this.groupRepo = groupRepo;
            CreateDefaultRoleAndGroup();
        }

        private void CreateDefaultRoleAndGroup()
        {
            CreateRole(new Role("Administrator"));

            if (!(GetAllGroup()).Select(x => x.Name).Contains("AdminGroup"))
            {
                CreateGroup("AdminGroup");
                AddRoleToGroup("AdminGroup", "Administrator");
            }
        }

        public IdentityResult CreateRole(Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            this.inAppRoleCollection.Add(role);
            return IdentityResult.Success;
        }

        public IdentityResult CreateGroup(string name)
        {
            if (name == null || name == "")
                throw new ArgumentNullException(nameof(name));

            try
            {
                var data = new GroupData(name);
                data._Created = DateTime.Now;
                groupRepo.Create(name, data);
            }
            catch (DuplicateIdException)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Group with name {name} already exists." });
            }

            TcoAppDomain.Current.Logger.Information($"New Group '{name}' created. {{@sender}}", new { Name = name });

            return IdentityResult.Success;
        }

        public IdentityResult DeleteGroup(string name)
        {
            if (name == null || name == "")
                throw new ArgumentNullException(nameof(name));

            groupRepo.Delete(name);

            TcoAppDomain.Current.Logger.Information($"Group '{name}' deleted. {{@sender}}", new { Name = name });

            return IdentityResult.Success;
        }

        public IdentityResult AddRoleToGroup(string group, string role)
        {
            if (group == null || group == "")
                throw new ArgumentNullException(nameof(group));
            if (role == null || role == "")
                throw new ArgumentNullException(nameof(role));

            try
            {
                GroupData data = null;
                data = groupRepo.Read(group);
                if (data != null)
                {
                    data.Roles.Add(role);
                    data._Modified = DateTime.Now;
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." });
                }

                groupRepo.Update(group, data);
            }
            catch (UnableToLocateRecordId)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." });
            }

            return IdentityResult.Success;
        }

        public IdentityResult AddRolesToGroup(string group, IEnumerable<string> roles)
        {
            if (group == null || group == "")
                throw new ArgumentNullException(nameof(group));
            if (roles == null)
                throw new ArgumentNullException(nameof(roles));

            try
            {
                GroupData data = null;
                data = groupRepo.Read(group);
                if (data != null)
                {
                    foreach (var role in roles)
                    {
                        data.Roles.Add(role);
                    }
                    data._Modified = DateTime.Now;
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." });
                }

                groupRepo.Update(group, data);
            }
            catch (UnableToLocateRecordId)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." });
            }

            TcoAppDomain.Current.Logger.Information($"Group '{group}' assign '{String.Join(",", roles)}'. {{@sender}}", new { Name = group, Roles = String.Join(",", roles) });

            return IdentityResult.Success;
        }

        public IdentityResult RemoveRolesFromGroup(string group, IEnumerable<string> roles)
        {
            if (group == null || group == "")
                throw new ArgumentNullException(nameof(group));
            if (roles == null)
                throw new ArgumentNullException(nameof(roles));

            try
            {
                GroupData data = null;
                data = groupRepo.Read(group);
                if (data != null)
                {
                    foreach (var role in roles)
                    {
                        data.Roles.Remove(role);
                    }
                    data._Modified = DateTime.Now;
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." });
                }

                TcoAppDomain.Current.Logger.Information($"Group '{group}' remove '{String.Join(",", roles)}'. {{@sender}}", new { Name = group, Roles = String.Join(",", roles) });

                groupRepo.Update(group, data);
            }
            catch (UnableToLocateRecordId)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." });
            }

            return IdentityResult.Success;
        }

        public List<string> GetRolesFromGroup(string group)
        {
            if (group == null || group == "")
                return null;

            GroupData data = null;

            try
            {
                if (!groupRepo.Exists(group))
                {
                    return null;
                }
                data = groupRepo.Read(group);
            }
            catch (UnableToLocateRecordId)
            {
                return null;
            }

            return new List<string>(data.Roles);
        }

        public string GetRolesFromGroupString(string group)
        {
            if (group == null || group == "")
                return null;

            GroupData data = null;

            try
            {
                if (!groupRepo.Exists(group))
                {
                    return null;
                }
                data = groupRepo.Read(group);
            }
            catch (UnableToLocateRecordId)
            {
                return null;
            }

            return String.Join(",", data.Roles);
        }

        public List<GroupData> GetAllGroup()
        {
            List<GroupData> data = null;
            data = groupRepo.Queryable.Where(p => true).ToList();
            return data;
        }
    }
}