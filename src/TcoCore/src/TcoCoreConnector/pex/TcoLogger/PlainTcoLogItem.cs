using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoCore
{
    public partial class PlainTcoLogItem
    {              
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
        private string raw;

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

        public string Raw
        {
            get => raw;

            set
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
            return $"{this.TimeStamp} : '{this.Text}' | {this.CategoryAsEnum} ({this.ParentsObjectSymbol})";
        }        
    }
}
