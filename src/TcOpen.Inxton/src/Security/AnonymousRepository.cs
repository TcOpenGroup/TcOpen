using System.Collections.Generic;
using System.Linq;
using TcOpen.Inxton.Data;

namespace TcOpen.Inxton.Local.Security
{
    //Used just as a filler, because when you open user mgmt when userrepo in security manager is null, you'll get an exception.
    internal class AnonymousRepository : IRepository<UserData>
    {
        public long Count => 0;

        public IQueryable<UserData> Queryable => GetRecords().AsQueryable();

        public OnCreateDelegate<UserData> OnCreate { get; set; }
        public OnReadDelegate OnRead { get; set; }
        public OnUpdateDelegate<UserData> OnUpdate { get; set; }
        public OnDeleteDelegate OnDelete { get; set; }
        public OnCreateDoneDelegate<UserData> OnCreateDone { get; set; }
        public OnReadDoneDelegate<UserData> OnReadDone { get; set; }
        public OnUpdateDoneDelegate<UserData> OnUpdateDone { get; set; }
        public OnDeleteDoneDelegate OnDeleteDone { get; set; }
        public OnCreateFailedDelegate<UserData> OnCreateFailed { get; set; }
        public OnReadFailedDelegate OnReadFailed { get; set; }
        public OnUpdateFailedDelegate<UserData> OnUpdateFailed { get; set; }
        public OnDeleteFailedDelegate OnDeleteFailed { get; set; }
        public ValidateDataDelegate<UserData> OnRecordUpdateValidation { get; set; }

        public void Create(string identifier, UserData data)
        {
        }

        public void Delete(string identifier)
        {
        }

        public bool Exists(string identifier) => false;

        public long FilteredCount(string id) => 0;

        public IEnumerable<UserData> GetRecords(string identifier = "*", int limit = 100, int skip = 0, eSearchMode searchMode = eSearchMode.Exact) => new List<UserData>();

        public UserData Read(string identifier) => new UserData();

        public void Update(string identifier, UserData data)
        {

        }
    }
}
