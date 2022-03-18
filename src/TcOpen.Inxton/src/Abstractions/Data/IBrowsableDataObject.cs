using System;

namespace TcOpen.Inxton.Data
{
    public interface IBrowsableDataObject
    {
        dynamic _recordId { get; set; }
      
        string _EntityId { get; set; }        
    }
}