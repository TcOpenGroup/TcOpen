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
using Vortex.Connector.ValueTypes;

namespace TcoInspectors
{
    public class TcoInspectorDialogDialogViewModel
        : Vortex.Presentation.Wpf.RenderableViewModel,
            ICloseable,
            IDisposable
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer =
            new System.Windows.Threading.DispatcherTimer();

        public TcoInspectorDialogDialogViewModel()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);

            dispatcherTimer.Start();

            RetryCommand = new RelayCommand(
                (a) =>
                {
                    Dialog._dialogRetry.Synchron = true;
                    CloseRequestEventHandler(this, new EventArgs());
                },
                (b) => Dialog != null && !Dialog._isOverInspected.Synchron,
                () =>
                    TcOpen.Inxton.TcoAppDomain.Current.Logger.Information(
                        $"{nameof(RetryCommand)} of {Dialog.HumanReadable} was executed @{{payload}}.",
                        new { Dialog.Symbol }
                    )
            );
            TerminateCommand = new RelayCommand(
                (a) =>
                {
                    Dialog._dialogTerminate.Synchron = true;
                    CloseRequestEventHandler(this, new EventArgs());
                },
                (b) => true,
                () =>
                    TcOpen.Inxton.TcoAppDomain.Current.Logger.Information(
                        $"{nameof(TerminateCommand)} of {Dialog.HumanReadable} was executed @{{payload}}.",
                        new { Dialog.Symbol }
                    )
            );
            OverrideCommand = new RelayCommand(
                (a) =>
                {
                    Dialog._dialogOverride.Synchron = true;
                    CloseRequestEventHandler(this, new EventArgs());
                },
                (b) => true,
                () =>
                    TcOpen.Inxton.TcoAppDomain.Current.Logger.Information(
                        $"{nameof(OverrideCommand)} of {Dialog.HumanReadable} was executed @{{payload}}.",
                        new { Dialog.Symbol }
                    )
            );
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

                                _inspectorsList = g
                                    ._inspections.Take(g._inspectionIndex.Synchron)
                                    .Select(p =>
                                        g.GetConnector()
                                            .IdentityProvider.GetVortexerByIdentity(p.Synchron)
                                        as IVortexObject
                                    )
                                    .Where(p => !(p is NullVortexIdentity))
                                    .ToList();

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
                    var insp = inspector as IsInspector;
                    if (insp != null)
                    {
                        if (insp.ResultAsEnum == eInspectorResult.Failed)
                        {
                            sb.Append(
                                cv.Convert(
                                    inspector,
                                    typeof(string),
                                    null,
                                    System.Globalization.CultureInfo.InvariantCulture
                                )
                            );
                            sb.Append("; ");
                        }
                    }
                }

                bool result = sb.ToString().All(c => c == ';' || c == ' ');

                if (result)
                {
                    sb.Clear(); //clear if failures descriptions are empty
                }
                return sb.ToString().TrimEnd(new char[] { ';', ' ' });
            }
        }

        public string FailureDescription
        {
            get
            {
                var parent = Dialog.GetParent();
                parent.Read();

                var sb = new System.Text.StringBuilder();

                foreach (var inspector in Inspectors)
                {
                    var insp = inspector as IsInspector;

                    if (insp != null)
                    {
                        if (insp.ResultAsEnum == eInspectorResult.Failed)
                        {
                            sb.Append(insp.InspectorData.FailureDescription.Synchron);
                            sb.Append("; ");
                        }
                    }
                }

                bool result = sb.ToString().All(c => c == ';' || c == ' ');

                if (result)
                {
                    sb.Clear(); //clear if failures descriptions are empty
                }
                return sb.ToString().TrimEnd(new char[] { ';', ' ' });
            }
        }

        public string ErrorCode
        {
            get
            {
                var parent = Dialog.GetParent();
                parent.Read();
                var sb = new System.Text.StringBuilder();
                var cv = new NameOrSymbolConverter();
                foreach (var inspector in Inspectors)
                {
                    var insp = inspector as IsInspector;
                    if (insp != null)
                    {
                        if (insp.ResultAsEnum == eInspectorResult.Failed)
                        {
                            sb.Append(insp.InspectorData.ErrorCode.Synchron);
                            sb.Append("; ");
                        }
                    }
                }

                bool result = sb.ToString().All(c => c == ';' || c == ' ');

                if (result)
                {
                    sb.Clear(); //clear if failures descriptions are empty
                }
                return sb.ToString().TrimEnd(new char[] { ';', ' ' });
            }
        }

        public TcoInspectorDialog Dialog { get; private set; } //= new TcoInspectorDialog();
        public override object Model
        {
            get => Dialog;
            set => Dialog = (TcoInspectorDialog)value;
        }
        public RelayCommand RetryCommand { get; }
        public RelayCommand TerminateCommand { get; }
        public RelayCommand OverrideCommand { get; }
        public event EventHandler CloseRequestEventHandler;

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Dialog.Read();
            if (
                Dialog._dialogRetry.LastValue
                || Dialog._dialogTerminate.LastValue
                || Dialog._dialogOverride.LastValue
                || !Dialog._isInvoked.LastValue
            )
            {
                CloseRequestEventHandler?.Invoke(this, e);
                Dialog._dialogIsClosed.Synchron = true;
                if (dispatcherTimer != null)
                {
                    dispatcherTimer.Stop(); // stop timer
                    dispatcherTimer.Tick -= dispatcherTimer_Tick;
                }
            }
        }

        public void Dispose()
        {
            if (dispatcherTimer != null)
            {
                dispatcherTimer.Stop();
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
            }
        }
    }

    public interface ICloseable
    {
        event EventHandler CloseRequestEventHandler;
    }
}
