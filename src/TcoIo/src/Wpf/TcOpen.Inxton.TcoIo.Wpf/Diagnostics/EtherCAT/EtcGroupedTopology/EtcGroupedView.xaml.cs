using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TcoIo.Converters;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoIo
{
    public partial class EtcGroupedView : UserControl
    {
        private Window detailWindow;

        public Window DetailWindow
        {
            get { return detailWindow; }
            set { detailWindow = value; }
        }

        private Grid detailGrid;

        public Grid DetailGrid
        {
            get { return detailGrid; }
            set { detailGrid = value; }
        }

        private TopologyRenderer detailTopologyRenderer;

        public TopologyRenderer DetailTopologyRenderer
        {
            get { return detailTopologyRenderer; }
            set { detailTopologyRenderer = value; }
        }

        private FrameworkElement detailUserView;

        public FrameworkElement DetailUserView
        {
            get { return detailUserView; }
            set { detailUserView = value; }
        }

        public EtcGroupedView()
        {
            InitializeComponent();
        }
    }
}
