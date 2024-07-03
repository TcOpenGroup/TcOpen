using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
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
            get { return groupedView; }
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

        private Type userView;

        public Type UserView
        {
            get { return userView; }
            set
            {
                if (value != null)
                {
                    userView = value;
                    NotifyPropertyChanged(nameof(UserView));
                }
            }
        }

        public EtcUngroupedViewData(
            IVortexObject _dataContext,
            bool _groupedView,
            string _firstTopologyElementName,
            string _lastTopologyElementName,
            bool _excludeSlavesConnectedToJunctionBox,
            Type _userView
        )
        {
            DataContext = _dataContext;
            GroupedView = _groupedView;
            FirstTopologyElementName = _firstTopologyElementName;
            LastTopologyElementName = _lastTopologyElementName;
            ExcludeSlavesConnectedToJunctionBox = _excludeSlavesConnectedToJunctionBox;
            UserView = _userView;
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
