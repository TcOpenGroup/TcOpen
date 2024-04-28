using System.Collections.ObjectModel;
using Vortex.Presentation.Wpf;
using TcoCognexVision.Converters;
using Vortex.Connector;
using System.Text;
using TcoCore;

namespace TcoCognexVision
{
    public class TcoDataman_v_5_x_xViewModel : RenderableViewModel
    {
        private eDisplayFormat _currentDisplayFormat = eDisplayFormat.String;
        public eDisplayFormat CurrentDisplayFormat
        {
            get => _currentDisplayFormat;
            set
            {
                _currentDisplayFormat = value;
                UpdateAndFormatResultData();
            }
        }
        public ObservableCollection<IndexedData<string>> ResultData { get ; private set; }= new ObservableCollection<IndexedData<string>>();
        public TcoDataman_v_5_x_xViewModel() : base()
        {
            ResultData = new ObservableCollection<IndexedData<string>>();
            //Model = new TcoDataman_v_5_x_x(new EmptyIVortexObject(), "Root", "Root");
             Model = new TcoDataman_v_5_x_x();
        }
        public TcoDataman_v_5_x_x Component { get; private set; }
        public override object Model 
        { 
            get => Component; 
            set 
            {
                Component = value as TcoDataman_v_5_x_x ;
                UpdateAndFormatResultData();
                Component._results.Length.Subscribe((sender, arg) => UpdateAndFormatResultData());
                Component._results.Id.Subscribe((sender, arg) => UpdateAndFormatResultData());
            }
        }
        private void UpdateAndFormatResultData()
        {
            TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                try
                {
              
                     ResultData.Clear();

                    if (Component != null && Component.GetConnector() != null && Component._results != null)
                    {
                        Component._results.Read();
                        if (CurrentDisplayFormat == eDisplayFormat.Array_of_decimals)
                        {
                            for (int i = 0; i < Component._results.Length.LastValue; i++)
                            {
                                ResultData.Add(new IndexedData<string>(i, Component._results.Data[i].LastValue.ToString()));
                            }
                        }
                        else if (CurrentDisplayFormat == eDisplayFormat.Array_of_hexdecimals)
                        {
                            for (int i = 0; i < Component._results.Length.LastValue; i++)
                            {
                                ResultData.Add(new IndexedData<string>(i, Component._results.Data[i].LastValue.ToString("X")));
                            }
                        }
                        else if (CurrentDisplayFormat == eDisplayFormat.String)
                        {
                            for (int i = 0; i < Component._results.Length.LastValue; i++)
                            {
                                byte _byte = Component._results.Data[i].LastValue;
                                string _string = "";
                                if (_byte >= 32)
                                    _string = Encoding.Default.GetString(new byte[] { _byte });
                                else
                                    _string = SpecialAsciiToSignConverters.SpecialAsciiToSign(_byte);

                                ResultData.Add(new IndexedData<string>(i, _string));
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {

                    throw;
                }
            });
        }
    }
    public class TcoDataman_v_5_x_xServiceViewModel : TcoDataman_v_5_x_xViewModel
    {

    }
}

