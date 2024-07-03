namespace TcoCore
{
    using System;
    using System.Linq;
    using Vortex.Connector;
    using Vortex.Localizations.Abstractions;

    public partial class TcoMessage
    {
        private IsTcoContext _context;

        private IsTcoObject _parentObject;

        /// <summary>
        /// Gets or sets context for orphaned messages that do not have a context.
        /// <note type="important">
        /// Orphaned object messages should not exist in the TcOpen application.
        /// There must be only one context in the PLC application and this single context can be set to this property.
        /// </note>
        /// </summary>
        [IgnoreReflection]
        [Obsolete(
            "Used only for backward compatibility. TcOpen application should not have orphaned messages when structured properly."
        )]
        public static IsTcoContext OrphanedMessageContext { get; set; }

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            _context = parent.GetParent<IsTcoContext>();
            _context?.AddMessage(this);
            _parentObject = parent.GetParent<IsTcoObject>();
        }

        private IsTcoContext GetContext()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return _context != null ? _context : OrphanedMessageContext;
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Gets whether this <see cref="TcoMessage"/> is active. The message is active when the <see cref="TcoMessage.Cycle"/> value is equal to <see cref="TcoContext.LastStartCycleCount"/>
        /// </summary>
        public bool IsActive
        {
            get
            {
                var retval = false;
                _context = GetContext();
                if (_context != null)
                {
                    retval =
                        this.Cycle.LastValue >= _context.LastStartCycleCount
                        || this.Cycle.LastValue
                            >= (
                                _context.LastStartCycleCount
                                - (ulong)(this.Connector.ReadWriteCycleDelay * 2)
                            )
                        || this.Pinned.LastValue;
                }

                return retval;
            }
        }

        private volatile object mutex = new object();

        private IVortexObject _indentityPersistence;

        [IgnoreReflection]
        private IVortexObject IndentityPersistence
        {
            get
            {
                lock (mutex)
                {
                    if (_indentityPersistence == null)
                    {
                        _indentityPersistence =
                            this.Connector.IdentityProvider.GetVortexerByIdentity(
                                this.Identity.Synchron
                            ) as IVortexObject;
                    }
                }
                return _indentityPersistence;
            }
        }

        private ITranslator _translatorPersistence;

        [IgnoreReflection]
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
        [IgnoreReflection]
        public PlainTcoMessage PlainMessage
        {
            get
            {
                var plain = this.CreatePlainerType();
                this.FlushOnlineToPlain(plain);
                plain.ParentsObjectSymbol = this._parentObject?.Symbol;
                plain.ParentsHumanReadable = this._parentObject?.HumanReadable;
                plain.Raw = plain.Text;
                if (plain.ExpectDequeing)
                {
                    var parent =
                        this.GetConnector().IdentityProvider.GetVortexerByIdentity(plain.Identity)
                        as IVortexObject;
                    plain.Text = Translate(plain.Text, parent);
                }
                else
                {
                    if (plain.Text != null)
                    {
                        plain.Text = TranslatorPersistence.Translate(
                            StringInterpolator.Interpolate(plain.Text, IndentityPersistence)
                        );
                    }
                }
                plain.Source = plain.ParentsObjectSymbol;
                plain.Location = plain.ParentsHumanReadable;
                plain.SetOnlinerMessage(this);
                return plain;
            }
        }

        /// <summary>
        /// Gets the last known message content in plain .net type system (aka POCO object) with object retieved by identity.
        /// </summary>
        [IgnoreReflection]
        public PlainTcoMessage LastKnownPlain
        {
            get
            {
                var plain = this.CreatePlainerType();
                plain.CopyCyclicToPlain(this);
                var parent =
                    this.GetConnector().IdentityProvider.GetVortexerByIdentity(plain.Identity)
                    as IVortexObject;
                plain.ParentsObjectSymbol = parent?.Symbol;
                plain.ParentsHumanReadable = parent?.HumanReadable;
                plain.Raw = plain.Text;
                plain.Text = Translate(plain.Text, parent);
                plain.Source = plain.ParentsObjectSymbol;
                plain.Location = plain.ParentsHumanReadable;
                return plain;
            }
        }

        private string Translate(string text, IVortexObject sender)
        {
            if (sender != null && sender.GetValueTags().FirstOrDefault() != null)
            {
                return sender
                    .GetValueTags()
                    .FirstOrDefault()
                    .Translator.Translate(StringInterpolator.Interpolate(text, sender));
            }

            return text;
        }
    }
}
