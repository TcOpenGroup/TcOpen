using System.Collections.Generic;
using System.ComponentModel;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoIo
{
    public class EtcUngroupedViewData : INotifyPropertyChanged
    {

        private IVortexObject dataContext;

        public IVortexObject DataContext
        {
            get { return dataContext; }
            set
            {
                if (value != null)
                {
                    dataContext = value;
                    NotifyPropertyChanged(nameof(DataContext));
                }
            }
        }

        private bool groupedView;
        public bool GroupedView
        {
            get{ return groupedView;}
            set 
            { 
                groupedView = value;
                NotifyPropertyChanged(nameof(GroupedView));
            }
        }

        private string firstTopologyElementName;
        public string FirstTopologyElementName
        {
            get { return this.firstTopologyElementName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.firstTopologyElementName = value;
                    NotifyPropertyChanged(nameof(FirstTopologyElementName));
                }
            }
        }

        private string lastTopologyElementName;
        public string LastTopologyElementName
        {
            get { return this.lastTopologyElementName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.lastTopologyElementName = value;
                    NotifyPropertyChanged(nameof(LastTopologyElementName));
                }
            }
        }

        private bool excludeSlavesConnectedToJunctionBox;

        public bool ExcludeSlavesConnectedToJunctionBox
        {
            get { return excludeSlavesConnectedToJunctionBox; }
            set
            {
                excludeSlavesConnectedToJunctionBox = value;
                NotifyPropertyChanged(nameof(ExcludeSlavesConnectedToJunctionBox));
            }
        }

        public EtcUngroupedViewData(IVortexObject dataContext, bool groupedView, string firstTopologyElementName, string lastTopologyElementName, bool excludeSlavesConnectedToJunctionBox)
        {
            DataContext = dataContext;
            GroupedView = groupedView;
            FirstTopologyElementName = firstTopologyElementName;
            LastTopologyElementName = lastTopologyElementName;
            ExcludeSlavesConnectedToJunctionBox = excludeSlavesConnectedToJunctionBox;
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

