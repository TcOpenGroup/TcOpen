using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vortex.Connector;
using Vortex.Connector.ValueTypes.Online;

namespace Sandbox.TcoIo.Wpf.Simulation
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SimulateState : UserControl
    {
        public SimulateState()
        {
            InitializeComponent();
        }


        private void btsSetState_Click(object sender, RoutedEventArgs e)
        {
            UpdateDesc();
            SetState(this.DataContext as IVortexObject);
        }

        private void SetState(IVortexObject obj)
        {
            if (obj != null)
            {
                IVortexObject InfoData = obj.GetKids().Where(i => i.AttributeName.Contains("InfoData")).FirstOrDefault() as IVortexObject;

                if (InfoData != null)
                {
                    IVortexElement State = InfoData.GetKids().Where(i => i.AttributeName.Contains("State")).FirstOrDefault() as IVortexElement;
                    if (State != null)
                    {
                        try
                        {
                            switch (SimulatedState)
                            {
                                case etcSlaveState.NONE:
                                    (State as IOnlineUInt).Cyclic = 0;
                                    break;
                                case etcSlaveState.INIT:
                                    (State as IOnlineUInt).Cyclic = 1;
                                    break;
                                case etcSlaveState.PREOP:
                                    (State as IOnlineUInt).Cyclic = 2;
                                    break;
                                case etcSlaveState.BOOT:
                                    (State as IOnlineUInt).Cyclic = 3;
                                    break;
                                case etcSlaveState.SAFEOP:
                                    (State as IOnlineUInt).Cyclic = 4;
                                    break;
                                case etcSlaveState.OP:
                                    (State as IOnlineUInt).Cyclic = 8;
                                    break;
                                case etcSlaveState.ERROR:
                                    (State as IOnlineUInt).Synchron = (ushort)((State as IOnlineUInt).Synchron | 0x0010);
                                    break;
                                case etcSlaveState.InvalidVendorId:
                                    (State as IOnlineUInt).Synchron = (ushort)((State as IOnlineUInt).Synchron | 0x0020);
                                    break;
                                case etcSlaveState.InitializationError:
                                    (State as IOnlineUInt).Synchron = (ushort)((State as IOnlineUInt).Synchron | 0x0040);
                                    break;
                                case etcSlaveState.SlaveDisabled:
                                    (State as IOnlineUInt).Synchron = (ushort)((State as IOnlineUInt).Synchron | 0x0080);
                                    break;
                                case etcSlaveState.SlaveNotPresent:
                                    (State as IOnlineUInt).Synchron = (ushort)((State as IOnlineUInt).Synchron | 0x0100);
                                    break;
                                case etcSlaveState.SlaveSignalsLinkError:
                                    (State as IOnlineUInt).Synchron = (ushort)((State as IOnlineUInt).Synchron | 0x0200);
                                    break;
                                case etcSlaveState.SlaveSignalsMissingLink:
                                    (State as IOnlineUInt).Synchron = (ushort)((State as IOnlineUInt).Synchron | 0x0400);
                                    break;
                                case etcSlaveState.SlaveSignalsUnexpectedLink:
                                    (State as IOnlineUInt).Synchron = (ushort)((State as IOnlineUInt).Synchron | 0x0800);
                                    break;
                                default:
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }

                foreach (IVortexObject child in obj.GetChildren())
                {
                    SetState(child);
                }
            }
        }

        private void UpdateDesc()
        {
            IVortexObject vo = this.DataContext as IVortexObject;
            if (vo != null)
            {
                tbSymbol.Text = vo.Symbol.ToString();
            }

            switch (SimulatedState)
            {
                case etcSlaveState.NONE:
                    tbStateDesc.Text = "Set to NONE state:";
                    break;
                case etcSlaveState.INIT:
                    tbStateDesc.Text = "Set to INIT state:";
                    break;
                case etcSlaveState.PREOP:
                    tbStateDesc.Text = "Set to PREOP state:";
                    break;
                case etcSlaveState.BOOT:
                    tbStateDesc.Text = "Set to BOOT state:";
                    break;
                case etcSlaveState.SAFEOP:
                    tbStateDesc.Text = "Set to SAFEOP state:";
                    break;
                case etcSlaveState.OP:
                    tbStateDesc.Text = "Set to OP state:";
                    break;
                case etcSlaveState.ERROR:
                    tbStateDesc.Text = "Set to ERROR state:";
                    break;
                case etcSlaveState.InvalidVendorId:
                    tbStateDesc.Text = "Set to InvalidVendorId state:";
                    break;
                case etcSlaveState.InitializationError:
                    tbStateDesc.Text = "Set to InitializationError state:";
                    break;
                case etcSlaveState.SlaveDisabled:
                    tbStateDesc.Text = "Set to SlaveDisabled state:";
                    break;
                case etcSlaveState.SlaveNotPresent:
                    tbStateDesc.Text = "Set to SlaveNotPresent state:";
                    break;
                case etcSlaveState.SlaveSignalsLinkError:
                    tbStateDesc.Text = "Set to SlaveSignalsLinkError state:";
                    break;
                case etcSlaveState.SlaveSignalsMissingLink:
                    tbStateDesc.Text = "Set to SlaveSignalsMissingLink state:";
                    break;
                case etcSlaveState.SlaveSignalsUnexpectedLink:
                    tbStateDesc.Text = "Set to SlaveSignalsUnexpectedLink state:";
                    break;
                default:
                    break;
            }
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateDesc();
        }

        public enum etcSlaveState
        {
            NONE,
            INIT,
            PREOP,
            BOOT,
            SAFEOP,
            OP,
            ERROR,
            InvalidVendorId,
            InitializationError,
            SlaveDisabled,
            SlaveNotPresent,
            SlaveSignalsLinkError,
            SlaveSignalsMissingLink,
            SlaveSignalsUnexpectedLink
        }



        public etcSlaveState SimulatedState
        {
            get { return (etcSlaveState)GetValue(SimulatedStateProperty); }
            set { SetValue(SimulatedStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SimulatedState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SimulatedStateProperty =
            DependencyProperty.Register("SimulatedState", typeof(etcSlaveState), typeof(SimulateState), new PropertyMetadata(etcSlaveState.NONE));


    }
}
