using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Input;
using Vortex.Presentation;

namespace TcoCore
{
    public class TcoDialogDialogViewModel : TcoDialogBaseViewModel
    {
        public TcoDialogDialogViewModel() : base()
        {
            OkCommand = new RelayCommand((a) => { Dialog._answer.Synchron = (short)eDialogAnswer.OK; this.Close(this, new EventArgs()); },
                (b) => Dialog._hasOK.Synchron,
                () => TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"{nameof(OkCommand)} of {Dialog.HumanReadable} was executed @{{payload}}.", new { Dialog.Symbol}));

            YesCommand = new RelayCommand((a) => { Dialog._answer.Synchron = (short)eDialogAnswer.Yes; this.Close(this, new EventArgs());},
        (b) => Dialog._hasYes.Synchron,
                () => TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"{nameof(YesCommand)} of {Dialog.HumanReadable} was executed @{{payload}}.", new { Dialog.Symbol }));

            NoCommand = new RelayCommand((a) => {Dialog._answer.Synchron = (short)eDialogAnswer.No; this.Close(this, new EventArgs());}, 
                (b) => Dialog._hasNo.Synchron,
                () => TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"{nameof(NoCommand)} of {Dialog.HumanReadable} was executed @{{payload}}.", new { Dialog.Symbol }));

            CancelCommand = new RelayCommand((a) => {Dialog._answer.Synchron = (short)eDialogAnswer.Cancel; this.Close(this, new EventArgs());}, 
                (b) => Dialog._hasCancel.Synchron,
                () => TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"{nameof(CancelCommand)} of {Dialog.HumanReadable} was executed @{{payload}}.", new { Dialog.Symbol }));
        }

        public TcoDialog Dialog { get; private set; } = new TcoDialog();
        public override object Model { get => Dialog; set => Dialog = (TcoDialog)value; }
        
        public RelayCommand OkCommand { get; }
        public RelayCommand YesCommand { get; }
        public RelayCommand NoCommand { get; }
        public RelayCommand CancelCommand { get; }        
    }

  
}
