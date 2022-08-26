using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton;
using Vortex.Presentation;

namespace TcoCore
{
    public class TcoDialogDialogViewModel : RenderableViewModelBase
    {
        public TcoDialog Dialog { get; private set; } = new TcoDialog();
        public override object Model { get => Dialog; set => Dialog = (TcoDialog)value; }

        public void DialogAnswerOk()
        {
            Dialog._answer.Synchron = (short)eDialogAnswer.OK;
            TcoAppDomain.Current.Logger.Information($"{nameof(DialogAnswerOk)} of {Dialog.HumanReadable} was executed @{{payload}}.", new { Dialog.Symbol });
        }
        public void DialogAnswerYes()
        {
            Dialog._answer.Synchron = (short)eDialogAnswer.Yes;
            TcoAppDomain.Current.Logger.Information($"{nameof(DialogAnswerYes)} of {Dialog.HumanReadable} was executed @{{payload}}.", new { Dialog.Symbol });
        }
        public void DialogAnswerNo()
        {
            Dialog._answer.Synchron = (short)eDialogAnswer.No;
            TcoAppDomain.Current.Logger.Information($"{nameof(DialogAnswerNo)} of {Dialog.HumanReadable} was executed @{{payload}}.", new { Dialog.Symbol });
        }
        public void DialogAnswerCancel()
        {
            Dialog._answer.Synchron = (short)eDialogAnswer.Cancel;
            TcoAppDomain.Current.Logger.Information($"{nameof(DialogAnswerCancel)} of {Dialog.HumanReadable} was executed @{{payload}}.", new { Dialog.Symbol });
        }

      
    }
}
