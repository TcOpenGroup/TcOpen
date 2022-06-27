#define WITH_SEC

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using TcoData;
using TcOpen.Inxton;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Data.Wpf.Properties;
using TcOpen.Inxton.Input;

namespace TcoData
{
    public class DataViewModel<T> : Vortex.Presentation.Wpf.BindableBase, FunctionAvailability where T : IBrowsableDataObject, new()
    {
        string filterByID;

        int limit;

        ViewMode mode;

        readonly ObservableCollection<object> observableRecords = new ObservableCollection<object>();

        string recordIdentifier;

        object selectedRecord;

        int skip = 1;
        public IList<int> PerPageFilter { get; } = new List<int> { 5, 10, 15, 20, 50, 100, 200 };

        public DataViewModel(IRepository<T> repository, TcoDataExchange dataExchange) : base()
        {
            this.DataExchange = dataExchange;
            DataBrowser = CreateBrowsable(repository);

            StartCreateNewCommand = new RelayCommand(p => StartCreatingNew(), _ => this.Mode == ViewMode.Display, () => LogCommand(nameof(StartCreateNewCommand)));
            CreateNewCommand = new RelayCommand(p => this.CreateNew(), _ => this.RecordIdentifier != string.Empty, () => LogCommand(nameof(CreateNewCommand)));
            CancelCreateNewCommand = new RelayCommand(p => this.Mode = ViewMode.Display, _ => true, () => LogCommand(nameof(CancelCreateNewCommand)));
            UpdateCommand = new RelayCommand(p => Update(), _ => this.Mode == ViewMode.Edit, () => LogCommand(nameof(UpdateCommand)));
            DeleteCommand = new RelayCommand(p => Delete(), _ => this.SelectedRecord != null && this.Mode == ViewMode.Display, () => LogCommand(nameof(DeleteCommand)));
            EditCommand = new RelayCommand(p => StartEdit(), _ => this.Mode == ViewMode.Display && SelectedRecord != null, () => LogCommand(nameof(EditCommand)));
            CancelEditCommand = new RelayCommand(p => this.CancelEdit(), _ => this.Mode == ViewMode.Edit, () => LogCommand(nameof(CancelEditCommand)));
            FindByCriteriaCommand = new RelayCommand(p => this.FindById(), _ => this.Mode == ViewMode.Display, () => LogCommand($"{nameof(FindByCriteriaCommand)} '{this.SearchMode} : {FilterByID}'"));
            FindContainsCommand = new RelayCommand(p => { this.SearchMode = eSearchMode.Contains;  this.FindById(); }, _ => this.Mode == ViewMode.Display, () => LogCommand($"{nameof(FindByCriteriaCommand)} '{this.SearchMode} : {FilterByID}'"));
            CancelFilterCommand = new RelayCommand(p => { this.FilterByID = string.Empty; this.FindById(); }, _ => this.Mode == ViewMode.Display, () => LogCommand($"{nameof(FindByCriteriaCommand)} '{this.SearchMode} : {FilterByID}'"));
            StartCreateCopyOfExisting = new RelayCommand(p => this.StartCreatingRecordCopy(), _ => this.SelectedRecord != null && this.Mode == ViewMode.Display, () => LogCommand(nameof(StartCreateCopyOfExisting)));
            CreateCopyOfExistingCommand = new RelayCommand(p => this.CreateCopyOfExisting(), _ => true, () => LogCommand(nameof(CreateCopyOfExistingCommand)));
            SendToPlcCommand = new RelayCommand(p => SendToPlc(), _ => this.SelectedRecord != null && this.Mode == ViewMode.Display, () => LogCommand(nameof(SendToPlcCommand)));
            LoadFromPlcCommand = new RelayCommand(p => LoadFromPlc(), _ => this.Mode == ViewMode.Display, () => LogCommand(nameof(LoadFromPlcCommand)));
            PageUpCommand = new RelayCommand(p => { this.Skip++; this.Filter(); }, PaginationUpEnabled, () => LogCommand(nameof(PageUpCommand)));
            PageDownCommand = new RelayCommand(p => { this.Skip--; this.Filter(); }, _ => this.Skip > 1, () => LogCommand(nameof(PageDownCommand)));
            ExportCommand = new RelayCommand(p => this.ExportData(), _ => this.Mode == ViewMode.Display, () => LogCommand(nameof(ExportCommand)));
            ImportCommand = new RelayCommand(p => this.ImportData(), _ => this.Mode == ViewMode.Display, () => LogCommand(nameof(ImportCommand)));


            CancelEditCommandAvailable = true;
            DeleteCommandAvailable = true;
            EditCommandAvailable = true;
            ExportCommandAvailable = true;
            ImportCommandAvailable = true;
            LoadFromPlcCommandAvailable = true;
            SendToPlcCommandAvailable = true;
            StartCreateCopyOfExistingAvailable = true;
            StartCreateNewCommandAvailable = true;
            UpdateCommandAvailable = true;

            this.FillObservableRecords();
        }

#if NET5_0_OR_GREATER
        private void RequeryCommands()
        {          
            ((RelayCommand)StartCreateNewCommand).RaiseCanExecuteChanged();
            ((RelayCommand)CreateNewCommand).RaiseCanExecuteChanged();

            ((RelayCommand)CancelCreateNewCommand).RaiseCanExecuteChanged();
            ((RelayCommand)UpdateCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
            ((RelayCommand)EditCommand).RaiseCanExecuteChanged();
            ((RelayCommand)CancelEditCommand).RaiseCanExecuteChanged();
            ((RelayCommand)FindByCriteriaCommand).RaiseCanExecuteChanged();
            ((RelayCommand)StartCreateCopyOfExisting).RaiseCanExecuteChanged();

            ((RelayCommand)CreateCopyOfExistingCommand).RaiseCanExecuteChanged();
            ((RelayCommand)SendToPlcCommand).RaiseCanExecuteChanged();
            ((RelayCommand)LoadFromPlcCommand).RaiseCanExecuteChanged();
            ((RelayCommand)PageUpCommand).RaiseCanExecuteChanged();
            ((RelayCommand)PageDownCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ExportCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ImportCommand).RaiseCanExecuteChanged();
        }
#endif

        private void LogCommand(string commandName) => TcoAppDomain
            .Current?
            .Logger?
            .Information<string>($"{DataExchange.Symbol}.{commandName}");

        private bool PaginationUpEnabled(object arg) => this.Pages > this.Skip;

        private void CancelEdit()
        {
            this.Mode = ViewMode.Display;
            // Clears canceled changes
            if(SelectedRecord != null)
            { 
                ((dynamic)DataExchange)._data.CopyPlainToShadow((dynamic)SelectedRecord);
            }
        }


        private void StartEdit()
        {
            this.Mode = ViewMode.Edit;            
        }

        private void ExportData()
        {
            try
            {
                var exports = this.DataBrowser.Export(p => true);
                var sfd = new SaveFileDialog();
                sfd.ShowDialog();

                using (var sw = new System.IO.StreamWriter(sfd.FileName))
                {
                    foreach (var item in exports)
                    {
                        sw.Write(item + "\r");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ImportData()
        {
            try
            {
                var ofd = new OpenFileDialog();
                ofd.ShowDialog();
                var fileName = ofd.FileName;

                var imports = new List<string>();
                foreach (var item in System.IO.File.ReadAllLines(fileName))
                {
                    imports.Add(item);
                }


                this.DataBrowser.Import(imports);
                this.FillObservableRecords();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

#if WITH_SEC
        // [VortexPrincipalPermissionAttribute(SecurityAction.Demand, Role = Security.Roles.data_exchange_view_can_user_add_record)]
#endif
        private void StartCreatingNew()
        {
            this.Mode = ViewMode.New; RecordIdentifier = string.Empty;         
        }

        private DataBrowser<T> CreateBrowsable(IRepository<T> repository)
        {
            return TcOpen.Inxton.Data.DataBrowser.Factory(repository);
        }

#if WITH_SEC
        // [VortexPrincipalPermissionAttribute(SecurityAction.Demand, Role = Security.Roles.data_exchange_view_can_user_add_record)]
#endif
        void CreateCopyOfExisting()
        {
            Vortex.Presentation.Wpf.ActionRunner.Runner.Execute(() =>
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
            });
        }

        private void CreateNew()
        {
            Vortex.Presentation.Wpf.ActionRunner.Runner.Execute(() =>
            {
                var plainer = ((dynamic)DataExchange)._data.CreatePlainerType();
                plainer._EntityId = RecordIdentifier;
                DataBrowser.AddRecord(plainer);
                var plain = DataBrowser.FindById(plainer._EntityId);
                ((dynamic)DataExchange)._data.CopyPlainToShadow(plain);
                FillObservableRecords();
                SelectedRecord = plain;
                this.Mode = ViewMode.Edit;          
            });
        }

#if WITH_SEC
        // [VortexPrincipalPermissionAttribute(SecurityAction.Demand, Role = Security.Roles.data_exchange_view_can_user_delete_record)]
#endif
        private void Delete()
        {

            var a = ((dynamic)DataExchange)._data.CreatePlainerType();
            a.CopyShadowToPlain(((dynamic)DataExchange)._data);
            string id = $"{DataExchange.Symbol}.{a._EntityId}";
            Vortex.Presentation.Wpf.ActionRunner.Runner.Execute(() =>
            {
                DataBrowser.Delete(a);
                this.FilterByID = "";
                FillObservableRecords();
                this.SelectedRecord = this.ObservableRecords.FirstOrDefault();
            },
            id,
            () => MessageBox.Show($"{strings.WouldYouLikeToDeleteRecord}: '{a._EntityId}'?", "Data", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes);
        }

        private void Filter()
        {
            FillObservableRecords();
        }

        private void FindById()
        {
            this.Skip = 1;
            FillObservableRecords();
        }
#if WITH_SEC
        // [VortexPrincipalPermissionAttribute(SecurityAction.Demand, Role = Security.Roles.data_exchange_view_can_user_send_to_plc)]
#endif
        private void SendToPlc()
        {

            Vortex.Presentation.Wpf.ActionRunner.Runner.Execute(() =>
            {
                ((dynamic)DataExchange)._data.FlushPlainToOnline((dynamic)this.SelectedRecord);
            }, $"{((dynamic)DataExchange)._data._EntityId}", () => MessageBox.Show($"{strings.LoadToController} '{((dynamic)this.SelectedRecord)._EntityId}'?", "Data", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes);
        }

#if WITH_SEC
        // [VortexPrincipalPermissionAttribute(SecurityAction.Demand, Role = Security.Roles.data_exchange_view_can_user_add_record)]
#endif
        private void StartCreatingRecordCopy()
        {

            RecordIdentifier = $"Copy of {((dynamic)SelectedRecord)._EntityId}";
            this.Mode = ViewMode.Copy;
        }

        private ICrudDataObject CrudDataObject
        {
            get
            {
                return ((dynamic)(this.DataExchange))._data as ICrudDataObject;
            }
        }

#if WITH_SEC
        // [VortexPrincipalPermissionAttribute(SecurityAction.Demand, Role = Security.Roles.data_exchange_view_can_update_record)]
#endif
        private void Update()
        {
            var a = ((dynamic)DataExchange)._data.CreatePlainerType();
            a.CopyShadowToPlain(((dynamic)DataExchange)._data);
            string info = $"{this.DataExchange.Symbol} {a._EntityId}";
            Vortex.Presentation.Wpf.ActionRunner.Runner.Execute(() =>
            {                                 
                CrudDataObject?.ChangeTracker.SaveObservedChanges(a);
                var validationErrrors = new StringBuilder();
                var validations = DataBrowser.UpdateRecord(a);
                var validationFailed = false;
                foreach (DataItemValidation validationItem in validations)
                {
                    if(validationItem.Failed)
                    {
                        validationErrrors.AppendLine($"{validationItem.Error}");
                        validationFailed = true;
                    }
                } 

                if(validationFailed)
                {
                    MessageBox.Show($"Some data is not valid: {validationErrrors}", "Data validation", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                FillObservableRecords();                
                this.Mode = ViewMode.Display;           
            }, info,
            () => MessageBox.Show($"{strings.WouldYouLikeToUpdateRecord} '{a._EntityId}'?", "Data", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes);
        }

        private int page
        {
            get
            {
                if (this.Skip > 0)
                    return this.Skip - 1;
                else
                {
                    return 0;
                }
            }
        }

        internal void FillObservableRecords()
        {
            ObservableRecords.Clear();
            DataBrowser.Filter(this.FilterByID, this.Limit, this.page * this.Limit, SearchMode);
            foreach (var item in DataBrowser.Records)
            {
                ObservableRecords.Add(item);
            }

            filteredCount = this.DataBrowser.FilteredCount(this.FilterByID);

#if NET5_0_OR_GREATER
                    this.RequeryCommands();
#endif
        }
        void LoadFromPlc()
        {
            Vortex.Presentation.Wpf.ActionRunner.Runner.Execute(() =>
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
            }, "", () => MessageBox.Show($"{strings.WouldYouLikeToGetFromPLC}", "Data", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes);
        }

        public ICommand ExportCommand { get; }
        public ICommand ImportCommand { get; }

        public ICommand CancelCreateNewCommand { get; }

        public ICommand CancelEditCommand { get; }

        public ICommand CreateCopyOfExistingCommand { get; }

        public ICommand CreateNewCommand { get; }

        public DataBrowser<T> DataBrowser { get; set; }

        public TcoDataExchange DataExchange { get; }

        public ICommand DeleteCommand { get; }

        public ICommand EditCommand { get; }

        public ICommand PageUpCommand { get; }
        public ICommand PageDownCommand { get; }

        public string FilterByID
        {
            get
            {
                if (string.IsNullOrEmpty(filterByID))
                {
                    filterByID = "";
                }

                return filterByID;
            }
            set
            {
                if (filterByID == value)
                {
                    return;
                }

                SetProperty(ref filterByID, value);
            }
        }

        public ICommand FindByCriteriaCommand
        {
            get; private set;
        }

        public ICommand FindContainsCommand
        {
            get; private set;
        }

        public ViewMode Mode
        {
            get
            {
                return mode;
            }
            set
            {                         
                SetProperty(ref mode, value);
#if NET5_0_OR_GREATER
                this.RequeryCommands();
#endif
            }
        }


        public int Limit
        {
            get
            {
                if (limit == 0)
                {
                    limit = 15;
                    this.OnPropertyChanged(nameof(Pages));
                }

                return limit;
            }
            set
            {
                if (limit == value)
                {
                    return;
                }

                if (value <= 100)
                    SetProperty(ref limit, value);
                else
                    SetProperty(ref limit, 100);

                this.OnPropertyChanged(nameof(Pages));

#if NET5_0_OR_GREATER
                    this.RequeryCommands();
#endif
            }
        }

        public int Skip
        {
            get
            {
                return skip;
            }
            set
            {
                if (skip == value)
                {
                    return;
                }

                SetProperty(ref skip, value);
                this.OnPropertyChanged(nameof(Pages));
            }
        }

        private long filteredCount;

        public long Pages
        {
            get
            {
                var divided = filteredCount * 1.0 / Limit * 1.0;
                var ceiling = Math.Ceiling(divided);
                return (long)ceiling;
            }
        }

        public ObservableCollection<object> ObservableRecords
        {
            get
            {
                return observableRecords;
            }
        }

        public string RecordIdentifier
        {
            get
            {
                return recordIdentifier;
            }
            set
            {
                if (recordIdentifier == value)
                {
                    return;
                }
#if NET5_0_OR_GREATER
                RequeryCommands();
#endif
                SetProperty(ref recordIdentifier, value);
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
                SetProperty(ref changes, value);
            }
        }

        public object SelectedRecord
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


                SetProperty(ref selectedRecord, value);

                if (value != null)
                {
#if NET5_0_OR_GREATER
                    this.RequeryCommands();
#endif
                    ((dynamic)DataExchange)._data.CopyPlainToShadow((dynamic)value);
                    ((ICrudDataObject)((dynamic)DataExchange)._data).Changes = ((IPlainTcoEntity)selectedRecord).Changes;
                    Changes = ((ICrudDataObject)((dynamic)DataExchange)._data).Changes;
                }

                CrudDataObject?.ChangeTracker.StartObservingChanges();
            }
        }

        public eSearchMode SearchMode { get; set; } = eSearchMode.Exact;

        public ICommand SendToPlcCommand { get; }

        public ICommand LoadFromPlcCommand { get; }

        public ICommand StartCreateCopyOfExisting { get; }

        public ICommand StartCreateNewCommand { get; }

        public ICommand UpdateCommand { get; }

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
        public RelayCommand CancelFilterCommand { get; }
    }

    public interface FunctionAvailability
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
    }

    public class DataViewModel
    {
        public static DataViewModel<T> Create<T>(IRepository<T> repository, TcoDataExchange dataExchange) where T : IBrowsableDataObject, new()
        {
            return new DataViewModel<T>(repository, dataExchange);
        }
    }

    public enum ViewMode
    {
        Display,
        Edit,
        New,
        Copy
    }

    public class ModeConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (ViewMode)value;
            var p = (string)parameter;
            if (v == ViewMode.Display && p == "Display")
            {
                return Visibility.Visible;
            }

            if (v == ViewMode.Edit && p == "Edit")
            {
                return Visibility.Visible;
            }

            if (v == ViewMode.New && p == "New")
            {
                return Visibility.Visible;
            }

            if ((v == ViewMode.New || v == ViewMode.Copy) && p == "NewCopy")
            {
                return Visibility.Visible;
            }

            if (v == ViewMode.Copy && p == "Copy")
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class NullToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }


    public class PercentageConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture)
        {
            double val = ConvertToDouble(value);

            if (parameter == null)
                return 0.5 * val;

            string[] split = parameter.ToString().Split('.');
            double parameterDouble = ConvertToDouble(split[0]) + ConvertToDouble(split[1]) / (Math.Pow(10, split[1].Length));
            return val * parameterDouble;
        }

        private static double ConvertToDouble(object value)
        {
            return (value is double) ? (double)value : (value is IConvertible) ? (value as IConvertible).ToDouble(null) : double.Parse(value.ToString());
        }

        public object ConvertBack(object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
