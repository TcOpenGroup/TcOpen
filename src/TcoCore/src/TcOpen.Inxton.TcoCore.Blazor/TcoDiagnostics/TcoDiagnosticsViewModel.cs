using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Input;
using Vortex.Connector;
using Vortex.Presentation;

namespace TcoCore
{
    public class TcoDiagnosticsViewModel : RenderableViewModelBase
    {

        private volatile object updatemutex = new object();
        PlainTcoMessage selectedMessage;

        public TcoDiagnosticsViewModel()
        {
            this.UpdateMessagesCommand = new RelayCommand(a => this.UpdateMessages(), (x) => !this.AutoUpdate && !this.DiagnosticsRunning);
        }

        public TcoDiagnosticsViewModel(IsTcoObject tcoObject)
        {
            _tcoObject = tcoObject;
            this.UpdateMessagesCommand = new RelayCommand(a => this.UpdateMessages(), (x) => !this.AutoUpdate && !this.DiagnosticsRunning);
        }
        /// <summary>
        /// Gets the command that executes update of messages on demand.
        /// </summary>
        public RelayCommand UpdateMessagesCommand { get; private set; }

        internal IsTcoObject _tcoObject { get; set; }
        public override object Model { get => this._tcoObject; set => this._tcoObject = value as IsTcoObject; }

        public object Categories
        {
            get
            {
                return Enum.GetValues(typeof(eMessageCategory)).Cast<eMessageCategory>().Skip(1);
            }
        }
        public eMessageCategory MinMessageCategoryFilter
        {
            get;
            set;
        } = eMessageCategory.Info;

        bool autoUpdate;
        /// <summary>
        /// Gets or sets whether the diagnostics view should auto refresh.
        /// </summary>
        public bool AutoUpdate
        {
            get
            {
                return autoUpdate;
            }
            set
            {
                if (autoUpdate == value)
                {
                    return;
                }

                SetProperty(ref autoUpdate, value);
            }
        }
        bool diagnosticsRunning;

        /// <summary>
        /// Gets or sets whether the diagnostic loop in running.
        /// </summary>
        public bool DiagnosticsRunning
        {
            get => diagnosticsRunning;
            internal set
            {
                if (diagnosticsRunning == value)
                {
                    return;
                }

                SetProperty(ref diagnosticsRunning, value);
            }
        }
        /// <summary>
        /// Updates messages of diagnostics view.
        /// </summary>
        internal void UpdateMessages()
        {
            if (DiagnosticsRunning)
            {
                return;
            }

            lock (updatemutex)
            {
                DiagnosticsRunning = true;

                Task.Run(() =>
                {
                    MessageDisplay = _tcoObject.GetActiveMessages().Where(p => p.CategoryAsEnum >= MinMessageCategoryFilter)
                                             .OrderByDescending(p => p.Category)
                                             .OrderBy(p => p.TimeStamp);


                }).Wait();

                DiagnosticsRunning = false;
            }
        }
        IEnumerable<PlainTcoMessage> messageDisplay = new List<PlainTcoMessage>();
        /// <summary>
        /// Gets list of messages to be displayed in message list of the view.
        /// </summary>
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

        /// <summary>
        ///  Gets or sets currently selected message from the list of messages.
        /// </summary>
        public PlainTcoMessage SelectedMessage
        {
            get
            {
                return selectedMessage;
            }
            set
            {
                if (value == null)
                    return;

                var clone = value.ShallowClone();

                if (selectedMessage == clone)
                {
                    return;
                }

                SetProperty(ref selectedMessage, clone);
                //this.OnPropertyChanged(nameof(AffectedObjectPresentation));
            }
        }

        public IVortexObject AffectedObject { get; set; }
       

    }



}
