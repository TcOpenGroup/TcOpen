namespace TcoCore
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Vortex.Connector;
    using Vortex.Localizations.Abstractions;
    using Vortex.Presentation;

    public partial class TcoMessage
    {
        private IsTcoContext _context;

        private IsTcoObject _parentObject;

        private readonly PlainTcoMessage _plain = new PlainTcoMessage();


        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            _context = parent.GetParent<IsTcoContext>();
            _context = _context == null ? new TcoContext() : _context;
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
                var retval = false;
                if (_context != null)
                {
                    retval = this.Cycle.LastValue >= _context.LastStartCycleCount;
                }

                return retval;
            }
        }


        private volatile object mutex = new object();

        private IVortexObject _indentityPersistence;
        private IVortexObject IndentityPersistence
        {
            get
            {
                lock (mutex)
                {
                    if (_indentityPersistence == null)
                    {
                        _indentityPersistence = this.Connector.IdentityProvider.GetVortexerByIdentity(this.Identity.Synchron) as IVortexObject;
                    }
                }
                return _indentityPersistence;
            }
        }

        private ITranslator _translatorPersistence;
        private ITranslator TranslatorPersistence
        {
            get
            {
                lock (mutex)
                {
                    if (_translatorPersistence == null)
                    {
                        _translatorPersistence = this.Text.Translator;
                       
                        if (IndentityPersistence != null)
                        {
                            try
                            {
                                dynamic vt = IndentityPersistence.GetValueTags().FirstOrDefault();

                                if (vt != null)
                                {
                                    _translatorPersistence = vt.Translator;
                                }
                            }
                            catch (Exception)
                            {

                                // Swallow
                            }
                            
                        }
                    }
                }

                return _translatorPersistence;
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
                _plain.Raw = _plain.Text;
                if(_plain.ExpectDequeing)
                { 
                    var parent = this.GetConnector().IdentityProvider.GetVortexerByIdentity(_plain.Identity) as IVortexObject;
                    _plain.Text = Translate(_plain.Text, parent);
                }
                else
                {
                    TranslatorPersistence.Translate(StringInterpolator.Interpolate(_plain.Text, IndentityPersistence));
                }
                _plain.Source = _plain.ParentsObjectSymbol;
                _plain.Location = _plain.ParentsHumanReadable;
                return _plain;
            }
        }
        
        /// <summary>
        /// Gets the last known message content in plain .net type system (aka POCO object) with object retieved by identity.
        /// </summary>
        public PlainTcoMessage LastPlainMessageNoTranslation
        {
            get
            {
                _plain.CopyCyclicToPlain(this);
                var parent = this.GetConnector().IdentityProvider.GetVortexerByIdentity(_plain.Identity) as IVortexObject;
                _plain.ParentsObjectSymbol = parent?.Symbol;
                _plain.ParentsHumanReadable = parent?.HumanReadable;
                _plain.Raw = _plain.Text;
                _plain.Text = Translate(_plain.Text, parent);                
                _plain.Source = _plain.ParentsObjectSymbol;
                _plain.Location = _plain.ParentsHumanReadable;
                return _plain;
            }
        }

        private string Translate(string text, IVortexObject sender)
        {
            return sender.GetValueTags().FirstOrDefault().Translator.Translate(StringInterpolator.Interpolate(text, sender));            
        }
    }
}
