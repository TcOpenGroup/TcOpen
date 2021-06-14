using System.Collections.Generic;
using Vortex.Connector;

namespace TcoData
{
    public partial class TcoEntity : ICrudDataObject
    {
        public ValueChangeTracker ChangeTracker { get; private set; }

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            ChangeTracker = new ValueChangeTracker(this);
        }

        partial void PexConstructorParameterless()
        {
            ChangeTracker = new ValueChangeTracker(this);
        }

        public List<ValueChangeItem> Changes { get; set; }
    }

}
