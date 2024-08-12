using System.ComponentModel;

namespace TcoCore
{
    public partial class PlainTcoMessage
    {
        internal TcoMessage OnlinerMessage;

        internal void SetOnlinerMessage(TcoMessage message)
        {
            OnlinerMessage = message;
        }

        string source;

        /// <summary>
        /// Gets source object of the message retrieved from the identity of the object that produced this message (typically the symbol of the PLC program).
        /// </summary>
        public string Source
        {
            get => source;
            internal set
            {
                if (source == value)
                {
                    return;
                }

                source = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Source)));
            }
        }

        string location;

        /// <summary>
        /// Gets location of the object retrieved from the identity of the object that produced this message (typically human readable path of the object).
        /// </summary>
        public string Location
        {
            get => location;
            internal set
            {
                if (location == value)
                {
                    return;
                }

                location = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Location)));
            }
        }

        /// <summary>
        /// Gets category of this message.
        /// </summary>
        public eMessageCategory CategoryAsEnum
        {
            get
            {
                var category = this.Category - (this.Category % (short)100);
                return (eMessageCategory)category;
            }
        }

        /// <summary>
        /// Get subcategory of this message.
        /// </summary>
        public short SubCategory
        {
            get { return (short)(this.Category % 100); }
        }

        /// <summary>
        /// Gets memberwise clone of this <see cref="PlainTcoMessage"/>
        /// </summary>
        /// <returns></returns>
        public PlainTcoMessage ShallowClone()
        {
            return (PlainTcoMessage)this.MemberwiseClone();
        }

        string parentsObjectSymbol;

        /// <summary>
        /// Gets symbol of the parent <see cref="TcoObject"/> that own this message.
        /// </summary>
        public string ParentsObjectSymbol
        {
            get => parentsObjectSymbol;
            internal set
            {
                if (parentsObjectSymbol == value)
                {
                    return;
                }

                parentsObjectSymbol = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(ParentsObjectSymbol))
                );
            }
        }

        string parentsHumanReadable;
        private string raw;

        /// <summary>
        /// Gets <see cref="Vortex.Connector.IVortexElement.HumanReadable"/> of the parent <see cref="TcoObject"/> that own this message.
        /// </summary>
        public string ParentsHumanReadable
        {
            get => parentsHumanReadable;
            set
            {
                if (parentsHumanReadable == value)
                {
                    return;
                }

                parentsHumanReadable = value;
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(ParentsHumanReadable))
                );
            }
        }

        /// <summary>
        /// Gets or sets raw text of this message (no translation, no interpolation).
        /// </summary>
        public string Raw
        {
            get => raw;
            internal set
            {
                if (raw == value)
                {
                    return;
                }

                raw = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Raw)));
            }
        }

        /// <summary>
        /// Get this message in string format.
        /// </summary>
        /// <returns>Formatted message</returns>
        public override string ToString()
        {
            return $"{this.TimeStamp} : '{this.Text}' | {this.CategoryAsEnum} ({this.Source})";
        }

        public override int GetHashCode() => (Raw, ParentsObjectSymbol, Category).GetHashCode();

        public override bool Equals(object obj)
        {
            var p = obj as PlainTcoMessage;
            return p != null
                && p.Raw == Raw
                && p.ParentsObjectSymbol == ParentsObjectSymbol
                && p.Category == Category;
        }
    }
}
