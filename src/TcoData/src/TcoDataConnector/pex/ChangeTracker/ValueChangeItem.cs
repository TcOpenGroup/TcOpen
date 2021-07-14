using System;

namespace TcoData
{
    public class ValueChangeItem
    {
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public ValueItemDescriptor ValueTag { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
}
