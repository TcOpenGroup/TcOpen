using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation;

namespace TcoCore
{
    public class TcoDialogDialogViewModel : RenderableViewModelBase
    {
        public event Notify DialogCloseEvent;
        public TcoDialog Dialog { get; private set; } = new TcoDialog();
        public override object Model { get => Dialog; set => Dialog = (TcoDialog)value; }

        protected virtual void OnProcessCompleted()
        {
            //if ProcessCompleted is not null then call delegate
            DialogCloseEvent?.Invoke();
        }

        public void DialogAnswerOk()
        {
            Dialog._answer.Synchron = (short)eDialogAnswer.OK;
            OnProcessCompleted();

        }
        public void DialogAnswerYes()
        {
            Dialog._answer.Synchron = (short)eDialogAnswer.Yes;
            OnProcessCompleted();
        }
        public void DialogAnswerNo()
        {
            Dialog._answer.Synchron = (short)eDialogAnswer.No;
            OnProcessCompleted();
        }
        public void DialogAnswerCancel()
        {
            Dialog._answer.Synchron = (short)eDialogAnswer.Cancel;
            OnProcessCompleted();
        }
    }
}
