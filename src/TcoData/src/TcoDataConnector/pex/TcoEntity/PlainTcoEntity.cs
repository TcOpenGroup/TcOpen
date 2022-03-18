using System.Collections.Generic;
using TcOpen.Inxton.Data;

namespace TcoData
{
    public partial class PlainTcoEntity : IBrowsableDataObject, IPlainTcoEntity
    {
        public dynamic _recordId { get; set; }

        List<ValueChangeItem> changes = new List<ValueChangeItem>();
        public List<ValueChangeItem> Changes
        {
            get
            {
                return changes;
            }
            set
            {
                changes = value;
            }
        }
    }

}
