using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;

namespace TcoData
{
    public interface IDataViewModel
    {

        TcoDataExchange DataExchange { get; }
        Task FillObservableRecordsAsync();
        List<ValueChangeItem> Changes { get; }
        List<IBrowsableDataObject> ObservableRecords { get;}
        int Limit { get; set; }
        int Page { get; set; }
        string FilterById { get; set; }
        eSearchMode SearchMode { get; set; }
        long FilteredCount { get; set; }
        IBrowsableDataObject SelectedRecord { get; set; }
    }
}
