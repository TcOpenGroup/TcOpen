using System;
using System.Collections.Generic;
using System.Linq;

namespace TcOpen.Inxton.Data
{
    public delegate void OnCreateDelegate<T>(string id, T data);
    public delegate void OnReadDelegate(string id);
    public delegate void OnUpdateDelegate<T>(string id, T data);
    public delegate void OnDeleteDelegate(string id);

    public delegate void OnCreateDoneDelegate<T>(string id, T data);
    public delegate void OnReadDoneDelegate<T>(string id, T data);
    public delegate void OnUpdateDoneDelegate<T>(string id, T data);
    public delegate void OnDeleteDoneDelegate(string id);

    public delegate void OnCreateFailedDelegate<T>(string id, T data, Exception ex);
    public delegate void OnReadFailedDelegate(string id, Exception ex);
    public delegate void OnUpdateFailedDelegate<T>(string id, T data, Exception ex);
    public delegate void OnDeleteFailedDelegate(string id, Exception ex);

    public delegate IEnumerable<DataItemValidation> ValidateDataDelegate<T>(T data);

    public interface IRepository
    {
        long Count { get; }
        void Create(string identifier, object data);
        void Delete(string identifier);
        bool Exists(string identifier);
        long FilteredCount(string id, eSearchMode searchMode = eSearchMode.Exact);
        dynamic Read(string identifier);
        void Update(string identifier, object data);
    }

    public interface IRepository<T>
        where T : IBrowsableDataObject
    {
        long Count { get; }
        IQueryable<T> Queryable { get; }
        void Create(string identifier, T data);
        void Delete(string identifier);
        bool Exists(string identifier);
        long FilteredCount(string id, eSearchMode searchMode = eSearchMode.Exact);
        IEnumerable<T> GetRecords(
            string identifier = "*",
            int limit = 100,
            int skip = 0,
            eSearchMode searchMode = eSearchMode.Exact
        );
        T Read(string identifier);
        void Update(string identifier, T data);
        OnCreateDelegate<T> OnCreate { get; set; }
        OnReadDelegate OnRead { get; set; }
        OnUpdateDelegate<T> OnUpdate { get; set; }
        OnDeleteDelegate OnDelete { get; set; }
        OnCreateDoneDelegate<T> OnCreateDone { get; set; }
        OnReadDoneDelegate<T> OnReadDone { get; set; }
        OnUpdateDoneDelegate<T> OnUpdateDone { get; set; }
        OnDeleteDoneDelegate OnDeleteDone { get; set; }
        OnCreateFailedDelegate<T> OnCreateFailed { get; set; }
        OnReadFailedDelegate OnReadFailed { get; set; }
        OnUpdateFailedDelegate<T> OnUpdateFailed { get; set; }
        OnDeleteFailedDelegate OnDeleteFailed { get; set; }
        ValidateDataDelegate<T> OnRecordUpdateValidation { get; set; }
    }
}
