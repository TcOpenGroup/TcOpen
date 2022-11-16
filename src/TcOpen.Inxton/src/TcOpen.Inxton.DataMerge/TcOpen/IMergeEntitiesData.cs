using System;
using System.Collections.Generic;
using System.Linq;
using TcOpen.Inxton.Data.Merge.Base;

namespace TcOpen.Inxton.Data.Merge

{
    public interface IMergeEntitiesData<T> : IMergeEntityData<T> where T : IBrowsableDataObject
    {


        T Source { get; set; }
        T Target { get; set; }

        IRepository<T> SourceRepository { get; set; }
        IRepository<T> TargetRepository { get; set; }

        IQueryable<T> GetEntities(IRepository<T> repository);
      

        IEnumerable<Type> RequiredTypes { get; }

        IEnumerable<string> RequiredProperties { get; }
        Func<object, bool> Exclusion { get; }


    }
}

