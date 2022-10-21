using System.Collections.ObjectModel;
using Vortex.Connector.ValueTypes;
using Vortex.Presentation.Wpf;
using TcoCognexVision.Converters;
using System.Linq;
using TcoCore;

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

        public ObservableCollection<IndexedData<OnlinerBaseType>> ResultData { get ; private set; }

        public TcoDataman_v_5_x_xViewModel() : base()
        {
            ResultData = new ObservableCollection<IndexedData<OnlinerBaseType>>();
            Model = new TcoDataman_v_5_x_x();
            
        }

        //public TcoDataman_v_5_x_x Component { get; private set; }
        public TcoDataman_v_5_x_xEx Component { get; private set; }

        public override object Model 
        { 
            get => Component; 
            set 
            {
                Component = value as TcoDataman_v_5_x_x;
                UpdateAndFormatResultData();
                Component._resultData.Length.ValueChangeEvent += ResultDataChangedEvent;
            }
        }

        private void ResultDataChangedEvent(Vortex.Connector.IValueTag sender, Vortex.Connector.ValueTypes.ValueChangedEventArgs args)
        {
            UpdateAndFormatResultData();
        }
        private void UpdateAndFormatResultData()
        {
            if(ResultData == null)
            {
                ResultData = new ObservableCollection<IndexedData<OnlinerBaseType>>();
            }
            else
            {
                ResultData.Clear();
            }
            if(CurrentDisplayFormat == eDisplayFormat.Array_of_decimals)
            {
                for (int i = 0; i < Component._resultData.Length.Cyclic; i++)
                {
                    ResultData.Add(new IndexedData<OnlinerBaseType>(i, Component._resultData.Data[i]));
                }
            }
            else if (CurrentDisplayFormat == eDisplayFormat.Array_of_hexdecimals)
            {
                for (int i = 0; i < Component._resultData.Length.Cyclic; i++)
                {
                    ResultData.Add(new IndexedData<OnlinerBaseType>(i, Component._resultData.Data[i]));
                }
            }

            //Component._resultData.Data
            //    .Select((data, index) => new IndexedData<OnlinerBaseType>(index, data))
            //    .ToList()
            //    .ForEach(x => { ResultData.Add(x); });
        }

    }

    public class TcoDataman_v_5_x_xServiceViewModel : TcoDataman_v_5_x_xViewModel
    {

    }


            ResultData = _resultData;
        }
    }
}

