using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcoCore
{
    public partial class TcoMessage : INotifyPropertyChanged
    {        
        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            _context = parent.GetParent<IsTcoContext>();
            _context.AddMessage(this);
            _parentObject = parent.GetParent<TcoObject>();
        }

        private TcoObject _parentObject;

        private IsTcoContext _context;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsActive
        {
            get
            {
                if(_context != null)
                {                                        
                    return _context.LastStartCycleCount <= this.Cycle.LastValue;
                }

                return false;
            }
        }

        private readonly PlainTcoMessage _plain = new PlainTcoMessage();
        public PlainTcoMessage PlainMessage
        {
            get
            {
                this.FlushOnlineToPlain(_plain);
                _plain.ParentsObjectSymbol = this._parentObject?.Symbol;
                _plain.ParentsHumanReadable = this._parentObject?.HumanReadable;
                var identity = this.Connector.IdentityProvider.GetVortexerByIdentity(_plain.Identity);
                _plain.Text = this.Text.Translator.Translate(StringInterpolator.Interpolate(_plain.Text, identity));
                _plain.Source = identity.Symbol;
                _plain.Location = identity.HumanReadable;
                return _plain;                   
            }
        }
    }

    public partial class PlainTcoMessage
    {
        string parentsObjectSymbol;
        public string ParentsObjectSymbol
        {
            get => parentsObjectSymbol; internal set
            {
                if (parentsObjectSymbol == value)
                {
                    return;
                }

                parentsObjectSymbol = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParentsObjectSymbol)));
            }
        }

        string parentsHumanReadable;
        public string ParentsHumanReadable
        {
            get => parentsHumanReadable; set
            {
                if (parentsHumanReadable == value)
                {
                    return;
                }

                parentsHumanReadable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParentsHumanReadable)));
            }
        }
    }
}
