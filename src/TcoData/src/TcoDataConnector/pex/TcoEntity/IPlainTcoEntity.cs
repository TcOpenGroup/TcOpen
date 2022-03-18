using System.Collections.Generic;

namespace TcoData
{
    public interface IPlainTcoEntity
    {
        List<ValueChangeItem> Changes { get; set; }
    }
}
