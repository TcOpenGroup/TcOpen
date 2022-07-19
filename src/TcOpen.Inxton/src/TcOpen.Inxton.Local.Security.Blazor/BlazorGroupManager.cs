using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security.Blazor
{
    public class BlazorGroupManager
    {
        private IRepository<GroupData> groupRepo;

        public BlazorGroupManager(IRepository<GroupData> groupRepo)
        {
            this.groupRepo = groupRepo;
        }

        public void Create(string name)
        {
            groupRepo.Create(name, new GroupData(name));
        }

        public void Delete(string name)
        {
            groupRepo.Delete(name);
        }

        public void AddRole(string group, string role)
        {
            GroupData data = groupRepo.Read(group);
            data.Roles.Add(role);
            groupRepo.Update(group, data);
        }

        public void AddRoles(string group, IEnumerable<string> roles)
        {
            GroupData data = groupRepo.Read(group);
            foreach (var role in roles)
            {
                data.Roles.Add(role);
            }
            groupRepo.Update(group, data);
        }

        public void RemoveRoles(string group, IEnumerable<string> roles)
        {
            GroupData data = groupRepo.Read(group);
            foreach (var role in roles)
            {
                data.Roles.Remove(role);
            }
            groupRepo.Update(group, data);
        }

        public List<string> GetRoles(string group)
        {
            if (group == null || !groupRepo.Exists(group))
                return null;
            GroupData data = groupRepo.Read(group);
            return new List<string>(data.Roles);
        }

        public string GetRolesString(string group)
        {
            if (group == null || !groupRepo.Exists(group))
                return null;
            GroupData data = groupRepo.Read(group);
            return String.Join(",", data.Roles);
        }

        public List<GroupData> GetAllGroup()
        {
            return (List<GroupData>)groupRepo.GetRecords();
        }
    }
}