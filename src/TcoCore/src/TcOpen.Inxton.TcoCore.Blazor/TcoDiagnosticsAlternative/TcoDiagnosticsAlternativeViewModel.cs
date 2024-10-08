using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;

using Vortex.Presentation;

namespace TcoCore
{
    public class TcoDiagnosticsAlternativeViewModel : RenderableViewModelBase
    {
        IEnumerable<PlainTcoMessage> messageDisplay = new List<PlainTcoMessage>();

        public TcoDiagnosticsAlternativeViewModel()
        {
            //Console.WriteLine("Default constructor called");
        }

        private object updatemutex = new object();
        internal IsTcoObject _tcoObject { get; set; }
        public override object Model { get => this._tcoObject; set => this._tcoObject = value as IsTcoObject; }

        public IEnumerable<PlainTcoMessage> MessageDisplay
        {
            get => messageDisplay;
            private set
            {
                if (messageDisplay == value)
                {
                    return;
                }
                SetProperty(ref messageDisplay, value);
            }
        }

        internal async Task UpdateMessagesAsync()
        {
            if (DiagnosticsRunning)
            {
                return;
            }

            DiagnosticsRunning = true;

            await Task.Run(() =>
            {
                MessageDisplay = _tcoObject?.MessageHandler?.GetActiveMessages();
                if (MessageDisplay == null)
                {
                    throw new InvalidOperationException("The _tcoObject or MessageHandler is null.");
                }
            });

            DiagnosticsRunning = false;
        }

        bool _diagnosticsRunning;
        public bool DiagnosticsRunning
        {
            get => _diagnosticsRunning;
            internal set
            {
                if (_diagnosticsRunning == value)
                {
                    return;
                }
                SetProperty(ref _diagnosticsRunning, value);
            }
        }
    }
}
