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
            if (Dialog != null && !Dialog._isOverInspected.Synchron)
            {
                RetryDisabled = false;
                Dialog._dialogueRetry.Synchron = true;
            }
            else
            {
                RetryDisabled = true;
            }
            TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"{nameof(Retry)} of {Dialog.HumanReadable} was executed @{{payload}}.", new { Dialog.Symbol });
        }
        public void Terminate()
        {
            Dialog._dialogueTerminate.Synchron = true;
            TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"{nameof(Terminate)} of {Dialog.HumanReadable} was executed @{{payload}}.", new { Dialog.Symbol });
        }
        public void Override()
        {
            Dialog._dialogueOverride.Synchron = true;
            TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"{nameof(Override)} of {Dialog.HumanReadable} was executed @{{payload}}.", new { Dialog.Symbol });
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
        public string Description
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var inspector in Inspectors)
                {
                    var value = inspector != null ? string.IsNullOrEmpty(inspector.AttributeName) ? inspector.GetSymbolTail() : inspector.AttributeName : "Missing object information";
                    sb.Append(value);
                }
                return sb.ToString();
            }
        }

        
    }
}
