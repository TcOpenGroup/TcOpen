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
              
        public PlainTcoMessage ShallowClone()
        {
            return (PlainTcoMessage)this.MemberwiseClone();
        }
    }
}
