using System.Collections.Generic;
using System.ComponentModel;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    public class EtcGroupedViewData : INotifyPropertyChanged
    {
        private string groupName;

        public string GroupName
        {
            get { return groupName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    groupName = value;
                    NotifyPropertyChanged(nameof(GroupName));
                }
            }
        }

        private List<GroupedViewItemObject> groupedViewItems;

        public List<GroupedViewItemObject> GroupedViewItems
        {
            get
            {
                return this.groupedViewItems
                    ?? (this.groupedViewItems = new List<GroupedViewItemObject>());
            }
            set
            {
                if (value != null)
                {
                    this.groupedViewItems = value;
                    NotifyPropertyChanged(nameof(GroupedViewItems));
                }
            }
        }

        private ushort infoDataState;

        public ushort InfoDataState
        {
            get { return infoDataState; }
            set
            {
                infoDataState = value;
                NotifyPropertyChanged(nameof(InfoDataState));
            }
        }

        private bool isInErrorState;

        public bool IsInErrorState
        {
            get { return isInErrorState; }
            set
            {
                isInErrorState = value;
                NotifyPropertyChanged(nameof(IsInErrorState));
            }
        }

        public EtcGroupedViewData(
            string _groupName,
            List<GroupedViewItemObject> _groupedViewItems,
            ushort _infoDataState,
            bool _isInErrorState
        )
        {
            GroupName = _groupName;
            GroupedViewItems = _groupedViewItems;
            InfoDataState = _infoDataState;
            IsInErrorState = _isInErrorState;
        }

        public void UpdateData(
            string _groupName,
            List<GroupedViewItemObject> _groupedViewItems,
            ushort _infoDataState,
            bool _isInErrorState
        )
        {
            GroupName = _groupName;
            GroupedViewItems = _groupedViewItems;
            InfoDataState = _infoDataState;
            IsInErrorState = _isInErrorState;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
