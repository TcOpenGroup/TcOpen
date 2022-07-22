using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation;

namespace TcoInspectors
{
    public class TcoInspectorDialogViewModel : RenderableViewModelBase
    {
        public TcoInspectorDialog Dialog { get; private set; } = new TcoInspectorDialog();
        public override object Model { get => Dialog; set => Dialog = (TcoInspectorDialog)value; }

        public bool RetryDisabled { get; set; } = false;
        public void Retry()
        {
            Dialog._dialogueRetry.Synchron = true;
            if (Dialog != null && !Dialog._isOverInspected.Synchron) RetryDisabled = true;

        }
        public void Terminate()
        {
            Dialog._dialogueTerminate.Synchron = true;
        }
        public void Override()
        {
            Dialog._dialogueOverride.Synchron = true;
        }
        //public string Description
        //{
        //    get
        //    {
        //        var sb = new System.Text.StringBuilder();
        //        var cv = new NameOrSymbolConverter();
        //        foreach (var inspector in Inspectors)
        //        {
        //            sb.Append(cv.Convert(inspector, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));
        //        }

        //        return sb.ToString();
        //    }
        //}
    }
}
