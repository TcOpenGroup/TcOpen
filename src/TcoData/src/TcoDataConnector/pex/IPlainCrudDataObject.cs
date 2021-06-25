using System.Collections.Generic;

namespace TcoData
{
    public interface IPlainCrudDataObject
    {
        List<ValueChangeItem> Changes { get; set; }
         
    }
}
