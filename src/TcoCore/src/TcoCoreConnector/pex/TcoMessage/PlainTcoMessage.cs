using System.ComponentModel;

namespace TcoCore
{

    public partial class PlainTcoMessage 
    {
        /// <summary>
        /// Gets source object of the message retrieved from the identity of the object that produced this message (typically the symbol of the PLC program).
        /// </summary>
        public string Source { get; internal set; }

        /// <summary>
        /// Gets location of the object retrieved from the identity of the object that produced this message (typically human readable path of the object).
        /// </summary>
        public string Location { get; internal set; }

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
            get
            {
                return (short)(this.Category % 100);
            }
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

        /// <summary>
        /// Gets <see cref="Vortex.Connector.IVortexElement.HumanReadable"/> of the parent <see cref="TcoObject"/> that own this message.
        /// </summary>
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

        /// <summary>
        /// Get this message in string format.
        /// </summary>
        /// <returns>Formatted message</returns>
        public override string ToString()
        {
            return $"{this.TimeStamp} : '{this.Text}' | {this.CategoryAsEnum} ({this.Source})";
        }
    }
}
