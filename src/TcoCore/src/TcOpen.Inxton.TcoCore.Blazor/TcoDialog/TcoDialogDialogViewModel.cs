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
        public TcoDialog Dialog { get; private set; } = new TcoDialog();
        public override object Model { get => Dialog; set => Dialog = (TcoDialog)value; }

        public void DialogAnswerOk()
        {
            Dialog._answer.Synchron = (short)eDialogAnswer.OK;

        }
        public void DialogAnswerYes()
        {
            Dialog._answer.Synchron = (short)eDialogAnswer.Yes;

        }
        public void DialogAnswerNo()
        {
            Dialog._answer.Synchron = (short)eDialogAnswer.No;

        }
        public void DialogAnswerCancel()
        {
            Dialog._answer.Synchron = (short)eDialogAnswer.Cancel;

        }
    }
}
