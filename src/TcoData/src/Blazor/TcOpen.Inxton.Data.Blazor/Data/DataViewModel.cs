using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Data;


namespace TcoData
{
    public class DataViewModel<T> : FunctionAvailability, IDataViewModel where T : IBrowsableDataObject, new()
    {

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

        public DataViewModel(IRepository<T> repository, TcoDataExchange dataExchange) : base()
        {

            this.DataExchange = dataExchange;
            DataBrowser = CreateBrowsable(repository);
            //FillObservableRecords();
            //Task.Run(FillObservableRecordsAsync);
            //this.DataExchange = dataExchange;
            //DataBrowser = CreateBrowsable(repository);

            //StartCreateNewCommand = new RelayCommand(p => StartCreatingNew(), _ => this.Mode == ViewMode.Display, () => LogCommand(nameof(StartCreateNewCommand)));
            //CreateNewCommand = new RelayCommand(p => this.CreateNew(), _ => this.RecordIdentifier != string.Empty, () => LogCommand(nameof(CreateNewCommand)));
            //CancelCreateNewCommand = new RelayCommand(p => this.Mode = ViewMode.Display, _ => true, () => LogCommand(nameof(CancelCreateNewCommand)));
            //UpdateCommand = new RelayCommand(p => Update(), _ => this.Mode == ViewMode.Edit, () => LogCommand(nameof(UpdateCommand)));
            //DeleteCommand = new RelayCommand(p => Delete(), _ => this.SelectedRecord != null && this.Mode == ViewMode.Display, () => LogCommand(nameof(DeleteCommand)));
            //EditCommand = new RelayCommand(p => StartEdit(), _ => this.Mode == ViewMode.Display && SelectedRecord != null, () => LogCommand(nameof(EditCommand)));
            //CancelEditCommand = new RelayCommand(p => this.CancelEdit(), _ => this.Mode == ViewMode.Edit, () => LogCommand(nameof(CancelEditCommand)));
            //FindByCriteriaCommand = new RelayCommand(p => this.FindById(), _ => this.Mode == ViewMode.Display, () => LogCommand($"{nameof(FindByCriteriaCommand)} '{this.SearchMode} : {FilterByID}'"));
            //FindContainsCommand = new RelayCommand(p => { this.SearchMode = eSearchMode.Contains; this.FindById(); }, _ => this.Mode == ViewMode.Display, () => LogCommand($"{nameof(FindByCriteriaCommand)} '{this.SearchMode} : {FilterByID}'"));
            //CancelFilterCommand = new RelayCommand(p => { this.FilterByID = string.Empty; this.FindById(); }, _ => this.Mode == ViewMode.Display, () => LogCommand($"{nameof(FindByCriteriaCommand)} '{this.SearchMode} : {FilterByID}'"));
            //StartCreateCopyOfExisting = new RelayCommand(p => this.StartCreatingRecordCopy(), _ => this.SelectedRecord != null && this.Mode == ViewMode.Display, () => LogCommand(nameof(StartCreateCopyOfExisting)));
            //CreateCopyOfExistingCommand = new RelayCommand(p => this.CreateCopyOfExisting(), _ => true, () => LogCommand(nameof(CreateCopyOfExistingCommand)));
            //SendToPlcCommand = new RelayCommand(p => SendToPlc(), _ => this.SelectedRecord != null && this.Mode == ViewMode.Display, () => LogCommand(nameof(SendToPlcCommand)));
            //LoadFromPlcCommand = new RelayCommand(p => LoadFromPlc(), _ => this.Mode == ViewMode.Display, () => LogCommand(nameof(LoadFromPlcCommand)));
            //PageUpCommand = new RelayCommand(p => { this.Skip++; this.Filter(); }, PaginationUpEnabled, () => LogCommand(nameof(PageUpCommand)));
            //PageDownCommand = new RelayCommand(p => { this.Skip--; this.Filter(); }, _ => this.Skip > 1, () => LogCommand(nameof(PageDownCommand)));
            //ExportCommand = new RelayCommand(p => this.ExportData(), _ => this.Mode == ViewMode.Display, () => LogCommand(nameof(ExportCommand)));
            //ImportCommand = new RelayCommand(p => this.ImportData(), _ => this.Mode == ViewMode.Display, () => LogCommand(nameof(ImportCommand)));


            //CancelEditCommandAvailable = true;
            //DeleteCommandAvailable = true;
            //EditCommandAvailable = true;
            //ExportCommandAvailable = true;
            //ImportCommandAvailable = true;
            //LoadFromPlcCommandAvailable = true;
            //SendToPlcCommandAvailable = true;
            //StartCreateCopyOfExistingAvailable = true;
            //StartCreateNewCommandAvailable = true;
            //UpdateCommandAvailable = true;

            //this.FillObservableRecords();
        }
        //void FillObservableRecords()
        //{
        //    ObservableRecords.Clear();
        //    DataBrowser.Filter("", 15, 0, eSearchMode.Exact);
        //    //DataBrowser.Filter(this.FilterByID, this.Limit, this.page * this.Limit, SearchMode);
        //    foreach (var item in DataBrowser.Records)
        //    {
        //        ObservableRecords.Add(item);
        //    }

        //    //filteredCount = this.DataBrowser.FilteredCount(this.FilterByID, SearchMode);


        //}
        //private int limit;
        //public int Limit 
        //{
        //    get 
        //    { 
        //        return limit; 
        //    }
        //    set
        //    {

        //    }
        //}
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

        public bool StartCreateNewCommandAvailable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool StartCreateCopyOfExistingAvailable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UpdateCommandAvailable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool CancelEditCommandAvailable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DeleteCommandAvailable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool EditCommandAvailable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool SendToPlcCommandAvailable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool LoadFromPlcCommandAvailable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ExportCommandAvailable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ImportCommandAvailable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

   

    public class DataViewModel
    {
        public static DataViewModel<T> Create<T>(IRepository<T> repository, TcoDataExchange dataExchange) where T : IBrowsableDataObject, new()
        {
            return new DataViewModel<T>(repository, dataExchange);
        }
    }

}
