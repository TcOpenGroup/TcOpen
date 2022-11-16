using System;
using System.ComponentModel;

namespace TcOpen.Inxton.Instructor
{
    public class InstructionItem : INotifyPropertyChanged
    {

        string key;

        /// <summary>
        /// Gets or sets the key of this instruction item.
        /// </summary>
        public string Key
        {
            get
            {
                return key;
            }
            set
            {
                if (key == value)
                {
                    return;
                }

                key = value;
                NotifyPropertyChange(nameof(Key));
            }
        }

        string description;

        /// <summary>
        /// Gets or sets arbitrary description of this instruction.
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (description == value)
                {
                    return;
                }

                description = value;
                NotifyPropertyChange(nameof(Description));
            }
        }

        string contentSource;

        /// <summary>
        /// Content source (path to image) of this instruction item.
        /// </summary>
        public string ContentSource
        {
            get
            {
                return contentSource;
            }
            set
            {
                if (contentSource == value)
                {
                    return;
                }

                contentSource = value;
                NotifyPropertyChange(nameof(ContentSource));
            }
        }
        /// <summary>
        /// Step HMI message
        /// </summary>
        public string Remarks { get; set; }

        private enumInstructionItemStatus status;

        public enumInstructionItemStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status == value)
                {
                    return;
                }

                status = value;
                NotifyPropertyChange(nameof(Status));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChange(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
