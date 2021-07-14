#define WITH_SEC

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using TcoData;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Data.Wpf.Properties;
using Vortex.Presentation.Wpf;

namespace TcoData
{
    public class DataViewModel<T> : BindableBase, FunctionAvailability where T : IBrowsableDataObject, new()
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

            StartCreateNewCommand       = new RelayCommand(p => StartCreatingNew()              , _ => this.Mode == ViewMode.Display,                                callerObject: this.DataExchange,   commandName:    nameof(StartCreateNewCommand));
            CreateNewCommand            = new RelayCommand(p => this.CreateNew()                , _ => this.RecordIdentifier != string.Empty,                        callerObject: this.DataExchange,   commandName:    nameof(CreateNewCommand));
            CancelCreateNewCommand      = new RelayCommand(p => this.Mode = ViewMode.Display    , _ => true,                                                         callerObject: this.DataExchange,   commandName:    nameof(CancelCreateNewCommand));
            UpdateCommand               = new RelayCommand(p => Update()                        , _ => this.Mode == ViewMode.Edit,                                   callerObject: this.DataExchange,   commandName:    nameof(UpdateCommand));
            DeleteCommand               = new RelayCommand(p => Delete()                        , _ => this.SelectedRecord != null && this.Mode == ViewMode.Display, callerObject: this.DataExchange,   commandName:    nameof(DeleteCommand));
            EditCommand                 = new RelayCommand(p => StartEdit()                     , _ => this.Mode == ViewMode.Display && SelectedRecord != null,      callerObject: this.DataExchange,   commandName:    nameof(EditCommand));
            CancelEditCommand           = new RelayCommand(p => this.CancelEdit()               , _ => this.Mode == ViewMode.Edit,                                   callerObject: this.DataExchange,   commandName:    nameof(CancelEditCommand));
            FindByCriteriaCommand       = new RelayCommand(p => this.FindById()                 , _ => this.Mode == ViewMode.Display,                                callerObject: this.DataExchange,   commandName:    nameof(FindByCriteriaCommand));
            StartCreateCopyOfExisting   = new RelayCommand(p => this.StartCreatingRecordCopy()  , _ => this.SelectedRecord != null && this.Mode == ViewMode.Display, callerObject: this.DataExchange,   commandName:    nameof(StartCreateCopyOfExisting));
            CreateCopyOfExistingCommand = new RelayCommand(p => this.CreateCopyOfExisting()     , _ => true,                                                         callerObject: this.DataExchange,   commandName:    nameof(CreateCopyOfExistingCommand));
            SendToPlcCommand            = new RelayCommand(p => SendToPlc()                     , _ => this.SelectedRecord != null && this.Mode == ViewMode.Display, callerObject: this.DataExchange,   commandName:    nameof(SendToPlcCommand));
            LoadFromPlcCommand          = new RelayCommand(p => LoadFromPlc()                   , _ => this.Mode == ViewMode.Display,                                callerObject: this.DataExchange,   commandName:    nameof(LoadFromPlcCommand));
            PageUpCommand               = new RelayCommand(p => { this.Skip++; this.Filter(); } , PaginationUpEnabled,                                      callerObject: this.DataExchange,   commandName:    nameof(PageUpCommand));
            PageDownCommand             = new RelayCommand(p => { this.Skip--; this.Filter(); } , _ => this.Skip > 1,                                                callerObject: this.DataExchange,   commandName:    nameof(PageDownCommand));
            ExportCommand               = new RelayCommand(p => this.ExportData()               , _ => this.Mode == ViewMode.Display,                                callerObject: this.DataExchange,   commandName:    nameof(ExportCommand));
            ImportCommand               = new RelayCommand(p => this.ImportData()               , _ => this.Mode == ViewMode.Display,                                callerObject: this.DataExchange,   commandName:    nameof(ImportCommand));


            CancelEditCommandAvailable         = true;
            DeleteCommandAvailable             = true;
            EditCommandAvailable               = true;
            ExportCommandAvailable             = true;
            ImportCommandAvailable             = true;
            LoadFromPlcCommandAvailable        = true;
            SendToPlcCommandAvailable          = true;
            StartCreateCopyOfExistingAvailable = true;
            StartCreateNewCommandAvailable     = true;
            UpdateCommandAvailable             = true;

            this.FillObservableRecords();
        }

        private bool PaginationUpEnabled(object arg) => this.Pages > this.Skip;

        private void CancelEdit()
        {
            this.Mode = ViewMode.Display;
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
            ActionRunner.Runner.Execute(() =>
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
            ActionRunner.Runner.Execute(() =>
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
            ActionRunner.Runner.Execute(() =>
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

            ActionRunner.Runner.Execute(() =>
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
            ActionRunner.Runner.Execute(() =>
            {
                CrudDataObject?.ChangeTracker.SaveObservedChanges(a);
                DataBrowser.UpdateRecord(a);
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
            DataBrowser.Filter(this.FilterByID, this.Limit, this.page * this.Limit);
            foreach (var item in DataBrowser.Records)
            {
                ObservableRecords.Add(item);
            }
        }
        void LoadFromPlc()
        {
            ActionRunner.Runner.Execute(() =>
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

        public RelayCommand ExportCommand { get; private set; }
        public RelayCommand ImportCommand { get; private set; }

        public RelayCommand CancelCreateNewCommand
        {
            get; private set;
        }

        public RelayCommand CancelEditCommand
        {
            get; private set;
        }

        public RelayCommand CreateCopyOfExistingCommand
        {
            get; private set;
        }

        public RelayCommand CreateNewCommand
        {
            get; private set;
        }

        public DataBrowser<T> DataBrowser
        {
            get; set;
        }

        public TcoDataExchange DataExchange
        {
            get; set;
        }

        public RelayCommand DeleteCommand
        {
            get; private set;
        }

        public RelayCommand EditCommand
        {
            get; private set;
        }

        public RelayCommand PageUpCommand { get; private set; }
        public RelayCommand PageDownCommand { get; private set; }

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

        public RelayCommand FindByCriteriaCommand
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
                if (mode == value)
                {
                    return;
                }

                SetProperty(ref mode, value);
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

        public long Pages
        {
            get
            {
                var filteredCount = this.DataBrowser.FilteredCount(this.FilterByID);
                var divided = filteredCount*1.0 / Limit*1.0;
                var ceiling = Math.Ceiling(divided);
                return (long) ceiling;
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
                    ((dynamic)DataExchange)._data.CopyPlainToShadow((dynamic)value);
                    ((ICrudDataObject)((dynamic)DataExchange)._data).Changes = ((IPlainCrudDataObject)selectedRecord).Changes;
                    Changes = ((ICrudDataObject)((dynamic)DataExchange)._data).Changes;
                }

                CrudDataObject?.ChangeTracker.StartObservingChanges();
            }
        }

        public RelayCommand SendToPlcCommand
        {
            get; private set;
        }

        public RelayCommand LoadFromPlcCommand { get; private set; }

        public RelayCommand StartCreateCopyOfExisting
        {
            get; private set;
        }

        public RelayCommand StartCreateNewCommand
        {
            get; private set;
        }

        public RelayCommand UpdateCommand
        {
            get; private set;
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
    }

    public interface FunctionAvailability
    {
        bool StartCreateNewCommandAvailable     { get; set; }
        bool StartCreateCopyOfExistingAvailable { get; set; }
        bool UpdateCommandAvailable             { get; set; }
        bool CancelEditCommandAvailable         { get; set; }
        bool DeleteCommandAvailable             { get; set; }
        bool EditCommandAvailable               { get; set; }
        bool SendToPlcCommandAvailable          { get; set; }
        bool LoadFromPlcCommandAvailable        { get; set; }
        bool ExportCommandAvailable             { get; set; }
        bool ImportCommandAvailable             { get; set; }
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
