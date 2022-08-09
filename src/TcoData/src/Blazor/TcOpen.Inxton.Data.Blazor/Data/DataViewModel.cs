using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;
using Vortex.Connector;

namespace TcoData
{
    public enum ViewMode
    {
        Display,
        Edit,
        New,
        Copy
    }

    public class DataViewModel<T> : IDataViewModel where T : IBrowsableDataObject, new()
    {

        public DataViewModel(IRepository<T> repository, TcoDataExchange dataExchange) : base()
        {

            this.DataExchange = dataExchange;
            DataBrowser = CreateBrowsable(repository);
        }

        public List<IBrowsableDataObject> ObservableRecords { get; private set; } = new List<IBrowsableDataObject>();


        private DataBrowser<T> CreateBrowsable(IRepository<T> repository)
        {
            return TcOpen.Inxton.Data.DataBrowser.Factory(repository);
        }

        public DataBrowser<T> DataBrowser { get; set; }
        public TcoDataExchange DataExchange { get; }


        IBrowsableDataObject selectedRecord;
        public IBrowsableDataObject SelectedRecord
        {
            get
            {
                return selectedRecord;
            }

            set
            {
                SetRowSelectedButtonState();
                if (selectedRecord == value)
                {
                    return;
                }

                CrudDataObject?.ChangeTracker.StopObservingChanges();
                selectedRecord = value;
                if (value != null)
                {
                    ((dynamic)DataExchange)._data.CopyPlainToShadow((dynamic)value);
                    ((ICrudDataObject)((dynamic)DataExchange)._data).Changes = ((IPlainTcoEntity)selectedRecord).Changes;
                    Changes = ((ICrudDataObject)((dynamic)DataExchange)._data).Changes;
                }

                CrudDataObject?.ChangeTracker.StartObservingChanges();



            }
        }

        private ICrudDataObject CrudDataObject
        {
            get
            {
                return ((dynamic)(this.DataExchange))._data as ICrudDataObject;
            }
        }

        List<ValueChangeItem> changes;
        public List<ValueChangeItem> Changes
        {
            get
            {
                return changes;
            }
            set
            {
                changes = value;
            }
        }




        private ViewMode mode;

        public ViewMode Mode
        {
            get
            {
                return mode;
            }
            set { mode = value; }
        }
       

        [Required(ErrorMessage = "The Name field is required")]
       
        public string RecordIdentifier { get; set; }


        public int Limit { get; set; } = 10;
        public string FilterById { get; set; } = "";
        public eSearchMode SearchMode { get; set; } = eSearchMode.Exact;
        public long FilteredCount { get; set; }
        public int Page { get; set; } = 0;
        public async Task FillObservableRecordsAsync()
        {
            //let another thread to load records, we need main thread to show loading symbol in blazor page
            await Task.Run(() => FillObservableRecords());

        }

        public void FillObservableRecords()
        {
            ObservableRecords.Clear();
            DataBrowser.Filter(FilterById, Limit, Page * Limit, SearchMode);
            //DataBrowser.Filter(this.FilterByID, this.Limit, this.page * this.Limit, SearchMode);
            foreach (var item in DataBrowser.Records)
            {
                ObservableRecords.Add(item);
            }
            FilteredCount = this.DataBrowser.FilteredCount(this.FilterById, SearchMode);

        }

        public void StartCreatingNew()
        {
            this.Mode = ViewMode.New; 
            RecordIdentifier = string.Empty;
            ViewModeNewCopy();
        }

        public void CreateNew()
        {
            var plainer = ((dynamic)DataExchange)._data.CreatePlainerType();
            plainer._EntityId = RecordIdentifier;
            DataBrowser.AddRecord(plainer);
            var plain = DataBrowser.FindById(plainer._EntityId);
            ((dynamic)DataExchange)._data.CopyPlainToShadow(plain);
            FillObservableRecords();
            SelectedRecord = plain;
            Mode = ViewMode.Edit;
            ViewModeEdit();
        }

        public void StartEdit()
        {
            this.Mode = ViewMode.Edit;
            ViewModeEdit();
        }


        public void Update()
        {
            var a = ((dynamic)DataExchange)._data.CreatePlainerType();
            a.CopyShadowToPlain(((dynamic)DataExchange)._data);
            CrudDataObject?.ChangeTracker.SaveObservedChanges(a);
            DataBrowser.UpdateRecord(a);
            FillObservableRecords();
            this.Mode = ViewMode.Display;
            SetRowSelectedButtonState();
        }

        public void CancelEdit()
        {
            this.Mode = ViewMode.Display;
            // Clears canceled changes
            if (SelectedRecord != null)
            {
                ((dynamic)DataExchange)._data.CopyPlainToShadow((dynamic)SelectedRecord);
            }
            SetRowSelectedButtonState();
        }

        public void Delete()
        {
            var a = ((dynamic)DataExchange)._data.CreatePlainerType();
            a.CopyShadowToPlain(((dynamic)DataExchange)._data);
            string id = $"{DataExchange.Symbol}.{a._EntityId}";
            DataBrowser.Delete(a);
            this.FilterById = "";
            FillObservableRecords();
            this.SelectedRecord = this.ObservableRecords.FirstOrDefault();
            SetRowSelectedButtonState();
        }

        public void StartCreatingRecordCopy()
        {

            RecordIdentifier = $"Copy of {SelectedRecord._EntityId}";
            this.Mode = ViewMode.Copy;
            ViewModeNewCopy();

        }

        public void CreateCopyOfExisting()
        {
            var plainer = ((dynamic)DataExchange)._data.CreatePlainerType();
            plainer.CopyShadowToPlain(((dynamic)DataExchange)._data);
            plainer._EntityId = RecordIdentifier;
            DataBrowser.AddRecord(plainer);
            var plain = DataBrowser.FindById(plainer._EntityId);
            ((dynamic)DataExchange)._data.CopyPlainToShadow(plain);
            FillObservableRecords();
            SelectedRecord = plain;
            this.Mode = ViewMode.Edit;
            ViewModeEdit();
           
        }

        public void LoadFromPlc()
        {
            var plainer = ((dynamic)DataExchange)._data.CreatePlainerType();
            ((dynamic)DataExchange)._data.FlushOnlineToPlain(plainer);
            plainer._EntityId = $"{DataHelpers.CreateUid().ToString()}";
            DataBrowser.AddRecord(plainer);
            var plain = DataBrowser.FindById(plainer._EntityId);
            ((dynamic)DataExchange)._data.CopyPlainToShadow(plain);
            FillObservableRecords();
            SelectedRecord = plain;
            this.Mode = ViewMode.Edit;
            ViewModeEdit();

        }

        public void SendToPlc()
        {
                ((dynamic)DataExchange)._data.FlushPlainToOnline((dynamic)this.SelectedRecord);
            //}, $"{((dynamic)DataExchange)._data._EntityId}", () => MessageBox.Show($"{strings.LoadToController} '{((dynamic)this.SelectedRecord)._EntityId}'?", "Data", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes);
        }

        public bool StartCreateNewCommandAvailable { get; set; }
        public bool StartCreateCopyOfExistingAvailable { get; set; }
        public bool UpdateCommandAvailable { get; set; }
        public bool CancelEditCommandAvailable { get; set; }
        public bool DeleteCommandAvailable { get; set; }
        public bool EditCommandAvailable { get; set; }
        public bool SendToPlcCommandAvailable { get; set; }
        public bool LoadFromPlcCommandAvailable { get; set; }
        public bool ExportCommandAvailable { get; set; }
        public bool ImportCommandAvailable { get; set; }

        public bool NewDisabled { get; set; }
        public bool CopyDisabled { get; set; }
        public bool UpdateDisabled { get; set; }
        public bool EditDisabled { get; set; }
        public bool SendToPlcDisabled { get; set; }
        public bool FromPlcDisabled { get; set; }
        public bool ImportDisabled { get; set; }
        public bool ExportDisabled { get; set; }
        public bool CancelDisabled { get; set; }
        public bool DeleteDisabled { get; set; }

        public void SetDefaultButtonState()
        {
            NewDisabled = false;
            CopyDisabled = true;
            UpdateDisabled = true;
            EditDisabled = true;
            SendToPlcDisabled = true;
            FromPlcDisabled = true;
            ImportDisabled = false;
            ExportDisabled = false;
            CancelDisabled = true;
            DeleteDisabled = true;
        }

        private void SetRowSelectedButtonState()
        {
            NewDisabled = false;
            CopyDisabled = false;
            UpdateDisabled = true;
            EditDisabled = false;
            SendToPlcDisabled = false;
            FromPlcDisabled = false;
            ImportDisabled = false;
            ExportDisabled = false;
            CancelDisabled = true;
            DeleteDisabled = false;
        }
        private void ViewModeNewCopy()
        {
            NewDisabled = true;
            CopyDisabled = true;
            UpdateDisabled = true;
            EditDisabled = true;
            SendToPlcDisabled = true;
            FromPlcDisabled = true;
            ImportDisabled = true;
            ExportDisabled = true;
            CancelDisabled = true;
            DeleteDisabled = true;
        }

        private void ViewModeEdit()
        {
            NewDisabled = true;
            CopyDisabled = true;
            UpdateDisabled = false;
            EditDisabled = true;
            SendToPlcDisabled = true;
            FromPlcDisabled = true;
            ImportDisabled = true;
            ExportDisabled = true;
            CancelDisabled = false;
            DeleteDisabled = true;
        }
    }

   

    public class DataViewModel
    {
        public static DataViewModel<T> Create<T>(IRepository<T> repository, TcoDataExchange dataExchange) where T : IBrowsableDataObject, new()
        {
            return new DataViewModel<T>(repository, dataExchange);
        }
    }

}
