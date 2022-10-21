using System.Collections.ObjectModel;
using Vortex.Connector.ValueTypes;
using Vortex.Presentation.Wpf;
using TcoCognexVision.Converters;
using System.Linq;
using TcoCore;
using System.Text;
using Vortex.Connector;
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
            //ResultData = new ObservableCollection<IndexedData<string>>();
            Model = new TcoDataman_v_5_x_xEx();
        }

        //public TcoDataman_v_5_x_x Component { get; private set; }
        public TcoDataman_v_5_x_xEx Component { get; private set; }

        public override object Model 
        { 
            get => Component; 
            set 
            {
                if(value.GetType().Equals(typeof(TcoDataman_v_5_x_xEx)))
                {
                    Component = value as TcoDataman_v_5_x_xEx;
                    UpdateAndFormatResultData();
                    Component._resultData.Length.Subscribe((sender, arg) => UpdateAndFormatResultData());
                    Component._resultData.Id.Subscribe((sender, arg) => UpdateAndFormatResultData());
                }
                else if (value.GetType().Equals(typeof(TcoDataman_v_5_x_x)))
                {
                    Component = new TcoDataman_v_5_x_xEx(value as TcoDataman_v_5_x_x, ResultData);
                    UpdateAndFormatResultData();
                    Component._resultData.Length.Subscribe((sender, arg) => UpdateAndFormatResultData());
                    Component._resultData.Id.Subscribe((sender, arg) => UpdateAndFormatResultData());
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

                    if (Component != null && Component.GetConnector() != null && Component._resultData.GetConnector() != null)
                    {
                        Component._resultData.Read();
                        if (CurrentDisplayFormat == eDisplayFormat.Array_of_decimals)
                        {
                            for (int i = 0; i < Component._resultData.Length.LastValue; i++)
                            {
                                ResultData.Add(new IndexedData<string>(i, Component._resultData.Data[i].LastValue.ToString()));
                            }
                        }
                        else if (CurrentDisplayFormat == eDisplayFormat.Array_of_hexdecimals)
                        {
                            for (int i = 0; i < Component._resultData.Length.LastValue; i++)
                            {
                                ResultData.Add(new IndexedData<string>(i, Component._resultData.Data[i].LastValue.ToString("X")));
                            }
                        }
                        else if (CurrentDisplayFormat == eDisplayFormat.String)
                        {
                            for (int i = 0; i < Component._resultData.Length.LastValue; i++)
                            {
                                byte _byte = Component._resultData.Data[i].LastValue;
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
                    return;
                    //swallow
                }
            });

           
            //Component._resultData.Data
            //    .Select((data, index) => new IndexedData<OnlinerBaseType>(index, data))
            //    .ToList()
            //    .ForEach(x => { ResultData.Add(x); });
        }

    }

    public class TcoDataman_v_5_x_xServiceViewModel : TcoDataman_v_5_x_xViewModel
    {

    }

    public class TcoDataman_v_5_x_xEx : TcoDataman_v_5_x_x
    {
        public ObservableCollection<IndexedData<string>> ResultData { get; private set; }

        public TcoDataman_v_5_x_xEx()
        {
            ResultData = new ObservableCollection<IndexedData<string>>();
        }
        public TcoDataman_v_5_x_xEx(TcoDataman_v_5_x_x _component, ObservableCollection<IndexedData<string>> _resultData) 
        {
            var properties = _component.GetType().GetProperties();
            properties.ToList().ForEach(property =>
                {
                    if (this.GetType().GetProperty(property.Name) != null && property.CanWrite)
                    {
                        var value = _component.GetType().GetProperty(property.Name).GetValue(_component, null);
                        this.GetType().GetProperty(property.Name).SetValue(this, value, null);
                    }
                });

            ResultData = _resultData;
        }
    }
}

