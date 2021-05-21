using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;
using Vortex.Connector.Identity;
using Vortex.Connector.ValueTypes;

namespace TcoCore
{
    public partial class TcoObject : IVortexIdentity, IsTcoObject
    {
        /// <summary>
        /// Get identity of this this <see cref="TcoObject"/>.
        /// </summary>
        public OnlinerULInt Identity => this._Identity;

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            this.Connector.IdentityProvider.AddIdentity(this);       
            this._context = parent.GetParent<TcoContext>();
        }

        List<IValueTag> refreshTags { get; set; }

        TcoContext _context;
       
        /// <summary>
        /// Performs refresh of the messages of this <see cref="TcoObject"/> and all its child object.
        /// </summary>
        /// <returns>Enumerable of messages as POCO object.</returns>
        public IEnumerable<PlainTcoMessage> GetActiveMessages()
        {
            if (refreshTags == null)
            {
                refreshTags = new List<IValueTag>();
                refreshTags.Add(_context._startCycleCount);
                refreshTags.AddRange(GetObjectMessages().Select(p => p.Cycle));                            
            }

            // We must check that the connector did start R/W operations loop, due to possible dead lock at start-up
            // Reported to Inxton team as FOXTROTH #564

            if (this.GetConnector().RwCycleCount > 1)
            {
                this.GetConnector().ReadBatch(refreshTags);
            }

            return GetObjectMessages().Where(p => p.IsActive).Select(p => p.PlainMessage);          
        }
        
        private IEnumerable<TcoMessage> GetObjectMessages()
        {
            return this.GetDescendants<TcoMessage>();
        }
        
    }
}
