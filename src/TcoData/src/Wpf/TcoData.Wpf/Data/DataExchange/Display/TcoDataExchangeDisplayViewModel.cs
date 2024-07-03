namespace TcoData
{
    public class TcoDataExchangeDisplayViewModel : TcoDataExchangeViewModel
    {
        public TcoDataExchangeDisplayViewModel()
            : base() { }

        protected override void UpdateAvailability()
        {
            ((FunctionAvailability)this.DataViewModel).CancelEditCommandAvailable = false;
            ((FunctionAvailability)this.DataViewModel).DeleteCommandAvailable = false;
            ((FunctionAvailability)this.DataViewModel).EditCommandAvailable = false;
            ((FunctionAvailability)this.DataViewModel).ExportCommandAvailable = false;
            ((FunctionAvailability)this.DataViewModel).ImportCommandAvailable = false;
            ((FunctionAvailability)this.DataViewModel).LoadFromPlcCommandAvailable = false;
            ((FunctionAvailability)this.DataViewModel).SendToPlcCommandAvailable = false;
            ((FunctionAvailability)this.DataViewModel).StartCreateCopyOfExistingAvailable = false;
            ((FunctionAvailability)this.DataViewModel).StartCreateNewCommandAvailable = false;
            ((FunctionAvailability)this.DataViewModel).UpdateCommandAvailable = false;
        }
    }
}
