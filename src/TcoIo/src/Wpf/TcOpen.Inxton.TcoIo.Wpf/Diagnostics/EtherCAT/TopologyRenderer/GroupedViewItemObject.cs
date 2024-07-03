using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoIo
{
    public class GroupedViewItemObject : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.name = value;
                    NotifyPropertyChanged(nameof(Name));
                }
            }
        }

        private bool inInErrorState;

        public bool IsInErrorState
        {
            get { return inInErrorState; }
            set
            {
                inInErrorState = value;
                NotifyPropertyChanged(nameof(IsInErrorState));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public GroupedViewItemObject(string name, bool isInErrorState)
        {
            Name = name;
            IsInErrorState = isInErrorState;
        }

        public GroupedViewItemObject(string name)
        {
            Name = name;
            IsInErrorState = false;
        }
    }
}
