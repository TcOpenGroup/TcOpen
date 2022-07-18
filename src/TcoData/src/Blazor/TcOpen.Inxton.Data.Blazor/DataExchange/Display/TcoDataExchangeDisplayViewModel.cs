using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoData;

namespace TcoData
{
    public class TcoDataExchangeDisplayViewModel : TcoDataExchangeViewModel
    {
        public TcoDataExchangeDisplayViewModel() : base()
        {

        }

        protected override void UpdateAvailability()
        {
            DataViewModel.CancelEditCommandAvailable = false;
            DataViewModel.DeleteCommandAvailable = false;
            DataViewModel.EditCommandAvailable = false;
            DataViewModel.ExportCommandAvailable = false;
            DataViewModel.ImportCommandAvailable = false;
            DataViewModel.LoadFromPlcCommandAvailable = false;
            DataViewModel.SendToPlcCommandAvailable = false;
            DataViewModel.StartCreateCopyOfExistingAvailable = false;
            DataViewModel.StartCreateNewCommandAvailable = false;
            DataViewModel.UpdateCommandAvailable = false;
        }
    }
}
