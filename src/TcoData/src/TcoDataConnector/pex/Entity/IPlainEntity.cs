using System.Collections.Generic;

namespace TcoData
{
    public interface IPlainEntity
    {
        List<ValueChangeItem> Changes { get; set; }
    }
}
