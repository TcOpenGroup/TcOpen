using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using TcoIo.Converters;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using Vortex.Connector.ValueTypes.Online;

namespace TcoIo
{
    /// <summary>
    /// Interaction logic for fbSampleComponentView.xaml
    /// </summary>
    public partial class InfoData_8649EEEBHWDiagView : UserControl
    {
        public InfoData_8649EEEBHWDiagView()
        {
            InitializeComponent();
        }

        private void InfoData_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            IVortexObject dataContext = this.DataContext as IVortexObject;
            if(dataContext != null)
            {
                IInfoData_8649EEEB infoData = dataContext as IInfoData_8649EEEB;
                if (infoData != null)
                {
                    IValueTag state = infoData.State as IValueTag;
                    state.Subscribe((s, a) => State_ValueChanged(s, a));

                    ValueChangedEventArgs args = new ValueChangedEventArgs(state);
                    State_ValueChanged(state, args);
                }
            }
        }

        private void State_ValueChanged(IValueTag sender, ValueChangedEventArgs args)
        {
            this.Dispatcher.Invoke(() =>
            {
                InfoData_8649EEEB dt = this.DataContext as InfoData_8649EEEB;
                if (dt != null)
                {
                    bool hasError = dt.ObjectId.Synchron != 0 && dt.SlaveCount.Synchron > 0 && dt.State.Synchron != 8;
                    Brush foregroundBrush = new SyncUnitErrorToForeground().Convert(hasError, null, null, null) as Brush;
                    dispObjectId.Foreground = foregroundBrush;
                    dispSlaveCount.Foreground = foregroundBrush;
                    groupBox.Foreground = foregroundBrush;
                }
            });
        }
    }
}
