using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;

namespace TcOpen.Inxton.Local.Security.Blazor
{
    public class BlazorGroupManager
    {
        private IRepository<GroupData> groupRepo;

        public BlazorGroupManager(IRepository<GroupData> groupRepo)
        {
            this.groupRepo = groupRepo;
        }

        public Task<IdentityResult> CreateAsync(string name)
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

        public Task<IdentityResult> DeleteAsync(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            groupRepo.Delete(name);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> AddRoleAsync(string group, string role)
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

        public Task<IdentityResult> AddRolesAsync(string group, IEnumerable<string> roles)
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

        public Task<IdentityResult> RemoveRolesAsync(string group, IEnumerable<string> roles)
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

        public List<string> GetRoles(string group)
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

        public string GetRolesString(string group)
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