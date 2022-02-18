using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCore;
using TcoInspectors;
using TcOpen.Inxton.Input;
using Vortex.Connector;
using Vortex.Connector.Identity;

namespace TcoInspectors
{
    public class TcoInspectorDialogDialogViewModel : Vortex.Presentation.Wpf.RenderableViewModel, ICloseable
    {
        public TcoInspectorDialogDialogViewModel()
        {            
           
           RetryCommand = new RelayCommand((a) => { Dialog._dialogueRetryTask.Execute(); CloseRequestEventHandler(this, new EventArgs()); }, (b) => Dialog != null && !Dialog._isOverInspected.Synchron);
           TerminateCommand = new RelayCommand((a) => { Dialog._dialogueTerminateTask.Execute(); CloseRequestEventHandler(this, new EventArgs()); });
           OverrideCommand = new RelayCommand((a) => { Dialog._dialogueOverrideTask.Execute(); CloseRequestEventHandler(this, new EventArgs()); });           
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
                var sb = new System.Text.StringBuilder();
                var cv = new NameOrSymbolConverter();
                foreach (var inspector in Inspectors)
                {
                    sb.Append(cv.Convert(inspector, typeof(string), null, System.Globalization.CultureInfo.InvariantCulture));
                }

                return sb.ToString();
            }
        }

        public TcoInspectorDialog Dialog { get; private set; } = new TcoInspectorDialog();
        public override object Model { get => Dialog; set => Dialog = (TcoInspectorDialog)value; }
        public RelayCommand RetryCommand { get; }
        public RelayCommand TerminateCommand { get; }
        public RelayCommand OverrideCommand { get; }
        public event EventHandler CloseRequestEventHandler;
    }

    public interface ICloseable
    {
        event EventHandler CloseRequestEventHandler;
    }
}
