using Vortex.Connector;

namespace TcoData
{
    public class ValueItemDescriptor
    {
        public ValueItemDescriptor() { }

        public ValueItemDescriptor(IValueTag valueTag)
        {
            this.HumanReadable = valueTag.HumanReadable;
            this.Symbol = valueTag.Symbol;
        }

        public string HumanReadable { get; set; }
        public string Symbol { get; set; }
    }
}
