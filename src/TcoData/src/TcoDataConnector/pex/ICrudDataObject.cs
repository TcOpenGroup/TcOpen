using System.Collections.Generic;
using Vortex.Connector.ValueTypes;

namespace TcoData
{
    public interface ICrudDataObject
    {
        OnlinerDateTime _Created { get; }
        OnlinerString _EntityId { get; }
        OnlinerDateTime _Modified { get; }
        ValueChangeTracker ChangeTracker { get; }
        List<ValueChangeItem> Changes { get; set; }
    }
}
