using System;

namespace TcOpen.Abstractions.Data
{
    public interface IBrowsableDataObject
    {
        dynamic _recordId { get; set; }
        DateTime _Created { get; set; }
        string _Id { get; set; }
        DateTime _Modified { get; set; }
    }
}