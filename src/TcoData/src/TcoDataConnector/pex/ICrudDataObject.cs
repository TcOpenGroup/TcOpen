using System.Collections.Generic;
using Vortex.Connector.ValueTypes;

namespace TcoData
{
    public interface ICrudDataObject
    {       
        OnlinerString _EntityId { get; }        
        ValueChangeTracker ChangeTracker { get; }
        List<ValueChangeItem> Changes { get; set; }
    }
}
