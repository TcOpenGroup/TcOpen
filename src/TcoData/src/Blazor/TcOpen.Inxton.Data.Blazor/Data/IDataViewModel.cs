using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;

namespace TcoData
{
    public interface IDataViewModel : FunctionAvailabilityBlazor
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
        ViewMode Mode { get; set; }
        string RecordIdentifier { get; set; }
        void SetDefaultButtonState();
        void StartCreatingNew();
        string CreateNew();
        void StartEdit();
        void Update();
        void CancelEdit();
        void Delete();
        void StartCreatingRecordCopy();
        string CreateCopyOfExisting();
        void SendToPlc();
        void LoadFromPlc();
        void FillObservableRecords();

    }

    public interface FunctionAvailabilityBlazor
    {
        bool StartCreateNewCommandAvailable { get; set; }
        bool StartCreateCopyOfExistingAvailable { get; set; }
        bool UpdateCommandAvailable { get; set; }
        bool CancelEditCommandAvailable { get; set; }
        bool DeleteCommandAvailable { get; set; }
        bool EditCommandAvailable { get; set; }
        bool SendToPlcCommandAvailable { get; set; }
        bool LoadFromPlcCommandAvailable { get; set; }
        bool ExportCommandAvailable { get; set; }
        bool ImportCommandAvailable { get; set; }

        bool NewDisabled { get; set; }
        bool CopyDisabled { get; set; }
        bool UpdateDisabled { get; set; }
        bool EditDisabled { get; set; }
        bool SendToPlcDisabled { get; set; }
        bool FromPlcDisabled { get; set; }
        bool ImportDisabled { get; set; }
        bool ExportDisabled { get; set; }
        bool CancelDisabled { get; set; }
        bool DeleteDisabled { get; set; }
    }
}
