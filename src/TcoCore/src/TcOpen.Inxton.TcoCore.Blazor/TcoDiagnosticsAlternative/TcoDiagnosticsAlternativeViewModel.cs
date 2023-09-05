using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlcDocu.TcoCore;

using TcoCore.TcoDiagnosticsAlternative.LoggingToDb;

using TcOpen.Inxton;
using TcOpen.Inxton.Input;
using Vortex.Presentation;


namespace TcoCore
{
    public class TcoDiagnosticsAlternativeViewModel : RenderableViewModelBase
    {
        private readonly IMongoLogger _logger;

        public TcoDiagnosticsAlternativeViewModel()
        {
            // Default constructor
        }

        public TcoDiagnosticsAlternativeViewModel(IMongoLogger logger)
        {
            _logger = logger;
        }

        public TcoDiagnosticsAlternativeViewModel(IsTcoObject tcoObject)
        {
            _tcoObject = tcoObject;
        }

        public TcoDiagnosticsAlternativeViewModel(IMongoLogger logger, IsTcoObject tcoObject)
        {
            _logger = logger;
            _tcoObject = tcoObject;
        }

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
        } = DefaulCategory;

        public static eMessageCategory SetDefaultCategory(eMessageCategory item) => DefaulCategory = item;

        public static eMessageCategory DefaulCategory { get; set; } = eMessageCategory.Info;


      
        bool diagnosticsRunning;

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

        //========================
        public event Action<PlainTcoMessage> NewMessageReceived;

        private volatile object updatemutex = new object();

        private bool isBusyLogging = false;


        internal void SaveNewMessages()
        {
            isBusyLogging = true;

            if (DiagnosticsRunning)
            {
                return;
            }

            lock (updatemutex)
            {
                DiagnosticsRunning = true;

                Task.Run(() =>
                {
                    MessageDisplay = _tcoObject.MessageHandler.GetActiveMessages();
                        //.Where(p => p.CategoryAsEnum >= MinMessageCategoryFilter)
                        //.OrderByDescending(p => p.Category)
                        //.OrderBy(p => p.TimeStamp);
                }).Wait();

                foreach (var message in MessageDisplay)
                {
                    if (!_logger.MessageExistsInDatabase(message.Identity))
                    {
                        _logger.LogMessage(message);
                    }
                }
                isBusyLogging = false;
                DiagnosticsRunning = false;
            }
        }


        public void AcknowledgeAllMessages()
        {
            try
            {
                lock (updatemutex)
                {
                    TcoAppDomain.Current.Logger.Information("All message acknowledged {@payload}", new { rootObject = _tcoObject.HumanReadable, rootSymbol = _tcoObject.Symbol });


                    foreach (var item in MessageDisplay.Where(p => p.Pinned))
                    {
                        DateTime currentDateTime = DateTime.Now;

                        // Update only if the TimeStampAcknowledged is older than 1980 and the message is not active
                        if (item.TimeStampAcknowledged < new DateTime(1980, 1, 1, 0, 0, 0))
                        {
                            _logger.SaveNewMessages(item.Identity, item.TimeStamp, currentDateTime, false);
                            item.OnlinerMessage.Pinned.Cyclic = false;
                            item.OnlinerMessage.TimeStampAcknowledged.Cyclic = currentDateTime;
                            TcoAppDomain.Current.Logger.Information("Message acknowledged {@message}", new { Text = item.Text, Category = item.CategoryAsEnum });
                        }
                    }
                    RefreshMessageDisplay();
                }
            }
            catch (Exception ex)
            {
                // Log the exception here
                TcoAppDomain.Current.Logger.Error("An error occurred while acknowledging messages: {@error}", ex);
            }
        }






        public void RefreshMessageDisplay()
        {
            MessageDisplay = _logger.ReadMessages();
        }


        public List<PlainTcoMessage> Messages { get; private set; } = new List<PlainTcoMessage>();

         IEnumerable<PlainTcoMessage> messageDisplay = new List<PlainTcoMessage>();

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

        public IEnumerable<PlainTcoMessage> DbMessageDisplay { get; private set; } = new List<PlainTcoMessage>();

        public void FetchMessagesFromDb()
        {
            var latestMessages = _logger.ReadMessages();
            DbMessageDisplay = latestMessages;
        }

        public void UpdateAndFetchMessages()
        {
            SaveNewMessages();
            FetchMessagesFromDb();
        }

    }

}
