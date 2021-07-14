namespace TcoCore
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using TcoCore;
    using TcOpen.Inxton;
    using TcOpen.Inxton.Input;
    using Vortex.Connector;
   

    public class TcoDiagnosticsViewModel : Vortex.Presentation.Wpf.RenderableViewModel
    {
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
      
        protected IsTcoObject _tcoObject { get; set; }
      
        private volatile object updatemutex = new object();

        /// <summary>
        /// Updates messages of diagnostics view.
        /// </summary>
        internal void UpdateMessages()
        {   
            if(DiagnosticsRunning)
            {
                return;
            }

            lock(updatemutex)
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
      
        /// <summary>
        /// Gets list of available standard message categories.
        /// </summary>
        public object Categories
        {
            get
            {
                return Enum.GetValues(typeof(eMessageCategory)).Cast<eMessageCategory>().Skip(1);
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
        /// Gets or sets the minimal category that will appear in the diagnostics view.
        /// </summary>
        public eMessageCategory MinMessageCategoryFilter
        {
            get;
            set;
        } = eMessageCategory.Info;

        
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
                this.OnPropertyChanged(nameof(AffectedObjectPresentation));
            }
        }
       
        /// <summary>
        /// Gets the command that executes update of messages on demand.
        /// </summary>
        public RelayCommand UpdateMessagesCommand { get; private set; }
        
        public override object Model { get => this._tcoObject; set => this._tcoObject = value as IsTcoObject; }


        private ulong lastSelectedMessageIdentity = 0;
        private object affectedObjectPresenation = null;
        public object AffectedObjectPresentation
        {
            get
            {
                if (this.SelectedMessage == null)
                    return null;

                if(this.SelectedMessage.Identity == lastSelectedMessageIdentity)
                {
                    return affectedObjectPresenation;
                }
                
                if (SelectedMessage != null)
                {
                    var affectedObject = this._tcoObject.GetConnector().IdentityProvider.GetVortexerByIdentity(SelectedMessage.Identity);

                    if (affectedObject != null)
                    {
                        try
                        {
                            TcoAppDomain.Current.Dispatcher.InvokeAsync(() =>
                            {
                                affectedObjectPresenation = Vortex.Presentation.Wpf.Renderer.Get.CreatePresentation("Service", (IVortexObject)affectedObject);
                            });
                            
                        }
                        catch (Exception)
                        {

                            //throw;
                        }
                    }
                }

                lastSelectedMessageIdentity = this.SelectedMessage.Identity;
                return affectedObjectPresenation;
            }
        }
    }
}
