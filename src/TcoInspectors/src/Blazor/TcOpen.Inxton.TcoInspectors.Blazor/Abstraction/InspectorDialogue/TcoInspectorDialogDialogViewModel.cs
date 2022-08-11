using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;
using Vortex.Connector.Identity;
using Vortex.Presentation;

namespace TcoInspectors
{
    public class TcoInspectorDialogDialogViewModel : RenderableViewModelBase
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

        List<IVortexObject> _inspectorsList;
        public IEnumerable<IVortexObject> Inspectors
        {
            get
            {
                if (_inspectorsList == null)
                {
                    _inspectorsList = new List<IVortexObject>();
                    try
                    {

                        var parent = Dialog.GetParent();
                        switch (parent)
                        {
                            case TcoInspectionGroup g:
                                g.Read();
                                _inspectorsList = g._inspections.Take(g._inspectionIndex.LastValue)
                                    .Select(p => g.GetConnector().IdentityProvider.GetVortexerByIdentity(p.LastValue) as IVortexObject)
                                    .Where(p => !(p is NullVortexIdentity)).ToList();
                                break;
                            case TcoInspector i:
                                _inspectorsList.Add(parent);
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        return _inspectorsList;
                    }
                }

                return _inspectorsList;
            }
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
