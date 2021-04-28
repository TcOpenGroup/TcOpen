namespace TcoCore
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using TcoCore;
    using Vortex.Connector;
    using Vortex.Presentation.Wpf;
    

    public class TcoDiagnosticsViewModel : RenderableViewModel
    {

        PlainTcoMessage selectedMessage;
    
        public TcoDiagnosticsViewModel()
        {
            this.UpdateMessagesCommand = new RelayCommand(a => this.UpdateMessages(), e => !this.AutoUpdate && !this.DiagnosticsRunning);            
        }


        public TcoDiagnosticsViewModel(IsTcoObject tcoObject)
        {
            _tcoObject = tcoObject;            
             this.UpdateMessagesCommand = new RelayCommand(a => this.UpdateMessages(), e => !this.AutoUpdate && !this.DiagnosticsRunning);
        }
      

        protected IsTcoObject _tcoObject { get; set; }

        public IEnumerable<string> PresentationTypes { get; } 
            = new List<string>() { "Diagnostic", "Manual", "Base", "Display", "Control" };



        private volatile object updatemutex = new object();

        private async void UpdateMessages()
        {           
            DiagnosticsRunning = true;
            await Task.Run(() =>
            {
                MessageDisplay = _tcoObject.GetActiveMessages().Where(p => p.CategoryAsEnum >= MinMessageCategoryFilter)
                                         .OrderByDescending(p => p.Category)
                                         .OrderBy(p => p.TimeStamp);               
            });
            DiagnosticsRunning = false;
        }

        bool diagnosticsRunning;
        public bool DiagnosticsRunning
        {
            get => diagnosticsRunning; 
            set
            {
                if (diagnosticsRunning == value)
                {
                    return;
                }

                SetProperty(ref diagnosticsRunning, value);
            }
        }

        string affectedObjectPresentationType;
        public string AffectedObjectPresentationType
        {
            get
            {
                return affectedObjectPresentationType;
            }
            set
            {
                if (affectedObjectPresentationType == value)
                {
                    return;
                }

                SetProperty(ref affectedObjectPresentationType, value);
                this.OnPropertyChanged(nameof(AffectedObject));
            }
        }

        /// <summary>
        /// Gets <see cref="SelectedMessage"/>'s object that produced the message.
        /// </summary>
        public object AffectedObject
        {
            get
            {
                if (SelectedMessage != null)
                {
                    var affectedObject = this._tcoObject.GetConnector().IdentityProvider.GetVortexerByIdentity(SelectedMessage.Identity);
                    
                    if (affectedObject != null && affectedObjectPresentationType != null)
                    {                       
                        return Renderer.Get.CreatePresentation(affectedObjectPresentationType, (IVortexObject)affectedObject);
                    }
                }

                return null;
            }
        }
       

        bool autoUpdate;
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

        public eMessageCategory MinMessageCategoryFilter
        {
            get;
            set;
        } = eMessageCategory.Notification;

        
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

                this.OnPropertyChanged(nameof(AffectedObject));
            }
        }

        public RelayCommand UpdateAffectedObjectsMessagesCommand { get; private set; }

        public RelayCommand UpdateMessagesCommand { get; private set; }
        public override object Model { get => this._tcoObject; set => this._tcoObject = value as IsTcoObject; }
    }
}
