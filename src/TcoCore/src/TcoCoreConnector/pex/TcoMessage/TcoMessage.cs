namespace TcoCore
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using Vortex.Connector;
    using Vortex.Presentation;

    public partial class TcoMessage
    {
        private IsTcoContext _context;

        private IsTcoObject _parentObject;

        private readonly PlainTcoMessage _plain = new PlainTcoMessage();       

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            _context = parent.GetParent<IsTcoContext>();
            _context?.AddMessage(this);
            _parentObject = parent.GetParent<IsTcoObject>();
        }

        /// <summary>
        /// Gets whether this <see cref="TcoMessage"/> is active. The message is active when the <see cref="TcoMessage.Cycle"/> value is equal to <see cref="TcoContext.LastStartCycleCount"/>
        /// </summary>
        public bool IsActive
        {
            get
            {
                if (_context != null)
                {                    
                    return this.Cycle.LastValue >= _context.LastStartCycleCount;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the message in plain .net type system (aka POCO object).
        /// </summary>
        public PlainTcoMessage PlainMessage
        {
            get
            {                
                this.FlushOnlineToPlain(_plain);
                _plain.ParentsObjectSymbol = this._parentObject?.Symbol;
                _plain.ParentsHumanReadable = this._parentObject?.HumanReadable;
                var identity = this.Connector.IdentityProvider.GetVortexerByIdentity(_plain.Identity);
                _plain.Text = this.Text.Translator.Translate(StringInterpolator.Interpolate(_plain.Text, identity));
                _plain.Source = _plain.ParentsObjectSymbol;
                _plain.Location = _plain.ParentsHumanReadable;
                return _plain;
            }
        }
    }
}
