using System.Collections.Generic;
using System.ComponentModel;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    public class EtcGroupedDataContext : INotifyPropertyChanged
    {
        private EtcGroupedViewData groupedViewData;

        public EtcGroupedViewData GroupedViewData
        {
            get { return groupedViewData; }
            set
            {
                if (value != null)
                {
                    groupedViewData = value;
                    NotifyPropertyChanged(nameof(GroupedViewData));
                }
            }
        }
        private EtcUngroupedViewData ungroupedViewData;

        public EtcUngroupedViewData UngroupedViewData
        {
            get { return ungroupedViewData; }
            set
            {
                if (value != null)
                {
                    ungroupedViewData = value;
                    NotifyPropertyChanged(nameof(UngroupedViewData));
                }
            }
        }

        public EtcGroupedDataContext(
            EtcGroupedViewData groupedViewData,
            EtcUngroupedViewData ungroupedViewData
        )
        {
            GroupedViewData = groupedViewData;
            UngroupedViewData = ungroupedViewData;
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
