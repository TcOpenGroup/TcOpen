using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoData;

namespace TcoData
{
    public class TcoDataExchangeControlViewModel : TcoDataExchangeViewModel
    {
        public TcoDataExchangeControlViewModel() : base()
        {

        }

        protected override void UpdateAvailability()
        {
            DataViewModel.CancelEditCommandAvailable = true;
            DataViewModel.DeleteCommandAvailable = true;
            DataViewModel.EditCommandAvailable = true;
            DataViewModel.ExportCommandAvailable = true;
            DataViewModel.ImportCommandAvailable = true;
            DataViewModel.LoadFromPlcCommandAvailable = false;
            DataViewModel.SendToPlcCommandAvailable = false;
            DataViewModel.StartCreateCopyOfExistingAvailable = true;
            DataViewModel.StartCreateNewCommandAvailable = true;
            DataViewModel.UpdateCommandAvailable = true;
        }
    }
}
