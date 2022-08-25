using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TcoIo.Converters;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoIo
{

    public partial class SyncUnitTask_AB2F5079SyncUnitTaskView : UserControl, INotifyPropertyChanged
    {

        private bool syncUnitHasError;

        public bool SyncUnitHasError
        {
            get { return syncUnitHasError; }
            set
            {
                syncUnitHasError = value;
                NotifyPropertyChanged(nameof(SyncUnitHasError));
            }
        }

        private Brush syncUnitBackgroundColor;

        public Brush SyncUnitBackgroundColor
        {
            get { return syncUnitBackgroundColor; }
            set
            {
                syncUnitBackgroundColor = value;
                NotifyPropertyChanged(nameof(SyncUnitBackgroundColor));
            }
        }


        private Brush syncUnitForegroundColor;

        public Brush SyncUnitForegroundColor
        {
            get { return syncUnitForegroundColor; }
            set
            {
                syncUnitForegroundColor = value;
                NotifyPropertyChanged(nameof(SyncUnitForegroundColor));
            }
        }


        public SyncUnitTask_AB2F5079SyncUnitTaskView()
        {
            InitializeComponent();
            DataContextChanged += SyncUnitTask_AB2F5079SyncUnitTaskView_DataContextChanged;
        }

        private void SyncUnitTask_AB2F5079SyncUnitTaskView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IVortexObject dataContext = this.DataContext as IVortexObject;
            if (dataContext != null)
            {
                ISyncUnitTask_AB2F5079 syncUnit = dataContext as ISyncUnitTask_AB2F5079;
                if (syncUnit != null)
                {
                    InfoData_8649EEEB infoData = syncUnit.InfoData as InfoData_8649EEEB;

                    IValueTag state = infoData.State as IValueTag;
                    state.Subscribe((s, a) => UpdateSyncUnitState(s, a));

                    ValueChangedEventArgs args = new ValueChangedEventArgs(state);
                    UpdateSyncUnitState(state, args);
                }
            }
        }

        private void UpdateSyncUnitState(IValueTag sender, ValueChangedEventArgs args)
        {
            TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
            SyncUnitTask_AB2F5079 dt = this.DataContext as SyncUnitTask_AB2F5079;
            if (dt != null)
            {
                    SyncUnitHasError = dt.InfoData.ObjectId.Synchron != 0 && dt.InfoData.SlaveCount.Synchron > 0 && dt.InfoData.State.Synchron != 8;
                    SyncUnitBackgroundColor = new SyncUnitErrorToBackground().Convert(SyncUnitHasError, null, null, null) as Brush;
                    SyncUnitForegroundColor = new SyncUnitErrorToForeground().Convert(SyncUnitHasError, null, null, null) as Brush;
                    stackPanel.Background = SyncUnitBackgroundColor;
                    groupBoxHeader.Foreground = SyncUnitForegroundColor;
                    WcState.Foreground = SyncUnitForegroundColor;
                    InfoData.Background = SyncUnitBackgroundColor;
                    InfoData.Foreground = SyncUnitForegroundColor;
                }
            });
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
