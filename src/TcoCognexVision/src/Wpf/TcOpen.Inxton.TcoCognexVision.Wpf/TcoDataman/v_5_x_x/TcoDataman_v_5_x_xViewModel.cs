using System.Collections.ObjectModel;
using Vortex.Connector.ValueTypes;
using Vortex.Presentation.Wpf;
using TcoCognexVision.Converters;
using System.Linq;
using TcoCore;
using Vortex.Connector;
using System.Text;

namespace TcoCognexVision
{

    public class TcoDataman_v_5_x_xViewModel : RenderableViewModel
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

        public ObservableCollection<IndexedData<string>> ResultData { get ; private set; }

        public TcoDataman_v_5_x_xViewModel() : base()
        {
            ResultData = new ObservableCollection<IndexedData<string>>();
            Model = new TcoDataman_v_5_x_xExtend();
        }

        public TcoDataman_v_5_x_xExtend Component { get; private set; }

        public override object Model 
        { 
            get => Component; 
            set 
            {
                if (value.GetType().Equals(typeof(TcoDataman_v_5_x_xExtend)))
                {
                    Component = value as TcoDataman_v_5_x_xExtend;
                    UpdateAndFormatResultData();
                    Component.Base._resultData.Length.Subscribe((sender, arg) => UpdateAndFormatResultData());
                    Component.Base._resultData.Id.Subscribe((sender, arg) => UpdateAndFormatResultData());
                }
                else if (value.GetType().Equals(typeof(TcoDataman_v_5_x_x)))
                {
                    Component.Base = value as TcoDataman_v_5_x_x;
                    Component.ResultData = ResultData;
                    UpdateAndFormatResultData();
                    Component.Base._resultData.Length.Subscribe((sender, arg) => UpdateAndFormatResultData());
                    Component.Base._resultData.Id.Subscribe((sender, arg) => UpdateAndFormatResultData());
                }

            }
        }

        private void UpdateAndFormatResultData()
        {
            TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    if (ResultData == null)
                    {
                        ResultData = new ObservableCollection<IndexedData<string>>();
                    }
                    else
                    {
                        ResultData.Clear();
                    }
                    if (Component != null && Component.Base.GetConnector() != null && Component.Base._resultData != null)
                    {
                        Component.Base._resultData.Read();
                        if (CurrentDisplayFormat == eDisplayFormat.Array_of_decimals)
                        {
                            for (int i = 0; i < Component.Base._resultData.Length.LastValue; i++)
                            {
                                ResultData.Add(new IndexedData<string>(i, Component.Base._resultData.Data[i].LastValue.ToString()));
                            }
                        }
                        else if (CurrentDisplayFormat == eDisplayFormat.Array_of_hexdecimals)
                        {
                            for (int i = 0; i < Component.Base._resultData.Length.LastValue; i++)
                            {
                                ResultData.Add(new IndexedData<string>(i, Component.Base._resultData.Data[i].LastValue.ToString("X")));
                            }
                        }
                        else if (CurrentDisplayFormat == eDisplayFormat.String)
                        {
                            for (int i = 0; i < Component.Base._resultData.Length.LastValue; i++)
                            {
                                byte _byte = Component.Base._resultData.Data[i].LastValue;
                                string _string = "";
                                if (_byte > 0)
                                    _string = Encoding.UTF8.GetString(new byte[] { _byte });
                                else _string = "N/A";

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

    public class TcoDataman_v_5_x_xExtend
    {
        public TcoDataman_v_5_x_x Base { get; set; }
        public ObservableCollection<IndexedData<string>> ResultData { get; set; }

        public TcoDataman_v_5_x_xExtend(TcoDataman_v_5_x_x _base, ObservableCollection<IndexedData<string>> _resultData)
        {
            Base = _base;
            ResultData = _resultData;
        }
        public TcoDataman_v_5_x_xExtend()
        {
            Base = new TcoDataman_v_5_x_x();
            ResultData = new ObservableCollection<IndexedData<string>>();
        }

    }
    public class TcoDataman_v_5_x_xServiceViewModel : TcoDataman_v_5_x_xViewModel
    {

    }
}

