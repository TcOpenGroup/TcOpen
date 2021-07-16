using System;

namespace TcOpen.Inxton.Data
{
    public interface IBrowsableDataObject
    {
        dynamic _recordId { get; set; }
        DateTime _Created { get; set; }
        string _EntityId { get; set; }
        DateTime _Modified { get; set; }
    }
}