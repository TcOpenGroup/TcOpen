using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security.Blazor
{
    public class BlazorRoleGroupManager
    {
        private IRepository<GroupData> groupRepo;
        public List<Role> inAppRoleCollection { get; set; } = new List<Role>();

        public BlazorRoleGroupManager(IRepository<GroupData> groupRepo)
        {
            this.groupRepo = groupRepo;
        }

        public void CreateRole(Role role)
        {
            this.inAppRoleCollection.Add(role);
        }

        public Task<IdentityResult> CreateGroupAsync(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            try
            {
                var data = new GroupData(name);
                data._Created = DateTime.Now;
                groupRepo.Create(name, data);
            }
            catch (DuplicateIdException)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = $"Group with name {name} already exists." }));
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteGroupAsync(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            groupRepo.Delete(name);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> AddRoleToGroupAsync(string group, string role)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            try
            {
                GroupData data = groupRepo.Read(group);
                if (data != null)
                {
                    data.Roles.Add(role);
                    data._Modified = DateTime.Now;
                }
                else
                {
                    return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." }));
                }

                groupRepo.Update(group, data);
            }
            catch (UnableToLocateRecordId)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." }));
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> AddRolesToGroupAsync(string group, IEnumerable<string> roles)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));
            if (roles == null)
                throw new ArgumentNullException(nameof(roles));

            try
            {
                GroupData data = groupRepo.Read(group);
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
                    return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." }));
                }

                groupRepo.Update(group, data);
            }
            catch (UnableToLocateRecordId)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." }));
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> RemoveRolesFromGroupAsync(string group, IEnumerable<string> roles)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));
            if (roles == null)
                throw new ArgumentNullException(nameof(roles));

            try
            {
                GroupData data = groupRepo.Read(group);
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
                    return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." }));
                }

                groupRepo.Update(group, data);
            }
            catch (UnableToLocateRecordId)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Description = $"Group with name {group} doesn't exists." }));
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public List<string> GetRolesFromGroup(string group)
        {
            if (group == null)
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
            if (group == null)
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
            List<GroupData> data = (List<GroupData>)groupRepo.GetRecords();

            return data;
        }
    }
}