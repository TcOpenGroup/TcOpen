using System.Collections.ObjectModel;
using Vortex.Presentation.Wpf;
using TcoCognexVision.Converters;
using Vortex.Connector;
using System.Text;

namespace TcoCognexVision
{
    public class TcoInsight_v_5_x_xViewModel : RenderableViewModel
    {
        private eDisplayFormat _currentDisplayFormat;
        public eDisplayFormat CurrentDisplayFormat
        {
            get => _currentDisplayFormat;
            set
            {
                _currentDisplayFormat = value;
                UpdateAndFormatResultData();
            }
        }
        public ObservableCollection<IndexedData<string>> InspectionResults { get ; private set; }
        public TcoInsight_v_5_x_xViewModel() : base()
        {
            InspectionResults = new ObservableCollection<IndexedData<string>>();
            Model = new TcoInsight_v_5_x_x();
        }
        public TcoInsight_v_5_x_x Component { get; private set; }
        public override object Model 
        { 
            get => Component; 
            set 
            {
                Component = value as TcoInsight_v_5_x_x;
                UpdateAndFormatResultData();
                Component._results.InspectionId.Subscribe((sender, arg) => UpdateAndFormatResultData());
            }
        }
        private void UpdateAndFormatResultData()
        {
            TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    if (InspectionResults == null)
                    {
                        InspectionResults = new ObservableCollection<IndexedData<string>>();
                    }
                    else
                    {
                        InspectionResults.Clear();
                    }
                    if (Component != null && Component.GetConnector() != null && Component._results != null)
                    {
                        Component._results.Read();
                        int resultLength = Component._config.ResultDataSize.Synchron;
                        if (CurrentDisplayFormat == eDisplayFormat.Array_of_decimals)
                        {
                            for (int i = 0; i < resultLength; i++)
                            {
                                InspectionResults.Add(new IndexedData<string>(i, Component._results.InspectionResults[i].LastValue.ToString()));
                            }
                        }
                        else if (CurrentDisplayFormat == eDisplayFormat.Array_of_hexdecimals)
                        {
                            for (int i = 0; i < resultLength; i++)
                            {
                                InspectionResults.Add(new IndexedData<string>(i, Component._results.InspectionResults[i].LastValue.ToString("X")));
                            }
                        }
                        else if (CurrentDisplayFormat == eDisplayFormat.String)
                        {
                            for (int i = 0; i < resultLength; i++)
                            {
                                byte _byte = Component._results.InspectionResults[i].LastValue;
                                string _string = "";
                                if (_byte > 0)
                                    _string = Encoding.UTF8.GetString(new byte[] { _byte });
                                else _string = "N/A";

                                InspectionResults.Add(new IndexedData<string>(i, _string));
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
    public class TcoInsight_v_5_x_xServiceViewModel : TcoInsight_v_5_x_xViewModel
    {

    }
}

