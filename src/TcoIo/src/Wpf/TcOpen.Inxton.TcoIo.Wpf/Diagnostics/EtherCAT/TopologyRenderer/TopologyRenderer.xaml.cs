using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using TcOpen.Inxton.TcoCore.Wpf;
using TcOpen.Inxton.TcoIo.Wpf.Diagnostics.EtherCAT;
using Vortex.Connector;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TopologyRenderer : UserControl
    {
        private int row,
            maxrow,
            column,
            maxcolumn = 0;
        private double Pos_X,
            MaxPos_X,
            Pos_Y,
            MaxPos_Y = 0.0;
        TopologyObject previousTopologyObject;
        ObservableCollection<TopologyObject> topologyObjects;
        static double strokeThicknessDef = 10.0;
        static string _name,
            _conection,
            _boxtype,
            _physics;
        public double zoom = 1.0;
        public ushort SummaryInfoDataState = 0;
        private IVortexObject dt;
        public List<GroupedViewItemObject> groupedViewItems = new List<GroupedViewItemObject>();
        public bool FirstTopologyElementReached,
            LastTopologyElementReached,
            syncUnitError = false;
        private int FirstElementRow,
            LastElementRow,
            FirstElementColumn,
            LastElementColumn = -1;
        private IVortexObject firstTopologyElement,
            lastTopologyElement;
        private System.Timers.Timer visibilityCheckTimer;
        private static System.Timers.Timer generatingWindowHideTimer;
        private bool isVisible,
            alreadyRendered = false;
        private static Generating generating;
        private static bool rendering = false;
        public static bool Rendering
        {
            get { return rendering; }
            set
            {
                rendering = value;
                if (!rendering)
                {
                    SetGeneratingWindowHideTimer();
                }
                else
                {
                    ResetGeneratingWindowHideTimer();
                }
            }
        }

        public static Generating Generating
        {
            get { return generating; }
            set { generating = value; }
        }

        public TopologyRenderer()
        {
            row = maxrow = column = maxcolumn = 0;
            Pos_X = Pos_Y = MaxPos_X = MaxPos_Y = 0;
            SummaryInfoDataState = 0;
            FirstElementRow = LastElementRow = FirstElementColumn = LastElementColumn = -1;
            firstTopologyElement = lastTopologyElement = null;
            previousTopologyObject = new TopologyObject();
            topologyObjects = new ObservableCollection<TopologyObject>();
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
            this.DataContextChanged += TopologyRenderer_DataContextChanged;
            InitializeComponent();
            SetVisibilityTimer();
            this.Unloaded += TopologyRenderer_Unloaded;
        }

        private void TopologyRenderer_Unloaded(object sender, RoutedEventArgs e)
        {
            if (Generating != null)
            {
                Generating.Hide();
            }
        }

        private void TopologyRenderer_DataContextChanged(
            object sender,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (this.DataContext as IVortexObject != null)
            {
                dt = this.DataContext as IVortexObject;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void SetVisibilityTimer()
        {
            if (visibilityCheckTimer == null)
            {
                visibilityCheckTimer = new System.Timers.Timer(1000);
                visibilityCheckTimer.Elapsed += VisibilityCheckTimer_Elapsed;
                visibilityCheckTimer.AutoReset = true;
                visibilityCheckTimer.Enabled = true;
            }
        }

        private static void SetGeneratingWindowHideTimer()
        {
            if (generatingWindowHideTimer == null)
            {
                generatingWindowHideTimer = new System.Timers.Timer(1000);
                generatingWindowHideTimer.Elapsed += GeneratingWindowHideTimer_Elapsed;
                ;
                generatingWindowHideTimer.AutoReset = false;
                generatingWindowHideTimer.Enabled = true;
            }
            generatingWindowHideTimer.Start();
        }

        private static void ResetGeneratingWindowHideTimer()
        {
            if (generatingWindowHideTimer != null)
            {
                generatingWindowHideTimer.Stop();
            }
        }

        private static void GeneratingWindowHideTimer_Elapsed(
            object sender,
            System.Timers.ElapsedEventArgs e
        )
        {
            TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                Generating.Hide();
            });
        }

        private void VisibilityCheckTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                if (IsVisible)
                {
                    if (!alreadyRendered)
                    {
                        RenderCompleteTopology();
                        alreadyRendered = true;
                    }
                }
                isVisible = IsVisible;
            });
        }
    }
}
