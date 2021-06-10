namespace TcoData
{
    public class TcoDataExchangeControlViewModel : TcoDataExchangeViewModel
    {
        public TcoDataExchangeControlViewModel() : base()
        {
           
        }

        protected override void UpdateAvailability()
        {
            ((FunctionAvailability)this.DataViewModel).CancelEditCommandAvailable = true;
            ((FunctionAvailability)this.DataViewModel).DeleteCommandAvailable = true;
            ((FunctionAvailability)this.DataViewModel).EditCommandAvailable = true;
            ((FunctionAvailability)this.DataViewModel).ExportCommandAvailable = true;
            ((FunctionAvailability)this.DataViewModel).ImportCommandAvailable = true;
            ((FunctionAvailability)this.DataViewModel).LoadFromPlcCommandAvailable = false;
            ((FunctionAvailability)this.DataViewModel).SendToPlcCommandAvailable = false;
            ((FunctionAvailability)this.DataViewModel).StartCreateCopyOfExistingAvailable = true;
            ((FunctionAvailability)this.DataViewModel).StartCreateNewCommandAvailable = true;
            ((FunctionAvailability)this.DataViewModel).UpdateCommandAvailable = true;
        }
    }
}
