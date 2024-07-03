using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcoCore
{
    public partial class TcoLogItem
    {
        /// <summary>
        /// Gets the last known log entry content in plain .net type system (aka POCO object) with object retieved by identity.
        /// </summary>
        public PlainTcoLogItem LastKnownPlain
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
