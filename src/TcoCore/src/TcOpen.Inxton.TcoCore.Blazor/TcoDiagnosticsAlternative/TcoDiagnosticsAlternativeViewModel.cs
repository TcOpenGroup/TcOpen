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
        private HashSet<ulong> activeMessages = new HashSet<ulong>();

        internal void LogMessages()
        {
            if (DiagnosticsRunning)
            {
                return;
            }

            lock (updatemutex)
            {
                DiagnosticsRunning = true;

                // Fetch the active messages
                var newMessageDisplay = _tcoObject.MessageHandler.GetActiveMessages()
                    .Where(p => p.CategoryAsEnum >= MinMessageCategoryFilter)
                    .OrderByDescending(p => p.Category)
                    .ThenBy(p => p.TimeStamp)
                    .ToList();

                // Create a set of message identities for easier lookup
                var newMessageIdentities = new HashSet<ulong>(newMessageDisplay.Select(m => m.Identity));

                // Log new messages that weren't active before
                foreach (var message in newMessageDisplay)
                {
                    if (!activeMessages.Contains(message.Identity))
                    {
                        Console.WriteLine($"Logging new message: {message.Identity}");
                        if (!_logger.MessageExistsInDatabase(message))
                        {
                            _logger.LogMessage(message);
                        }
                        activeMessages.Add(message.Identity);
                    }
                }

                // Remove or acknowledge messages that are no longer active
                var messagesToRemove = activeMessages.Where(id => !newMessageIdentities.Contains(id)).ToList();
                foreach (var id in messagesToRemove)
                {
                    Console.WriteLine($"Removing or acknowledging message: {id}");
                    AcknowledgeMessage(id);
                    activeMessages.Remove(id);
                }

                // Update the MessageDisplay
                MessageDisplay = newMessageDisplay;

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

                    foreach (var item in MessageDisplay)
                    {
                        DateTime currentDateTime = DateTime.UtcNow;

                        // Update only if the TimeStampAcknowledged is older than 1980 and the message is not active
                        if (item.TimeStampAcknowledged < new DateTime(1980, 1, 1, 0, 0, 0))
                        {
                            _logger.UpdateMessages(item.Identity, item.TimeStamp, currentDateTime, false);
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

        public void AcknowledgeMessage(ulong identity)
        {
            try
            {
                lock (updatemutex)
                {
                    // Read messages from the database
                    var messages = _logger.ReadMessages();

                    // Find the message with the given identity
                    var message = messages.FirstOrDefault(m => m.Identity == identity);

                    if (message != null)
                    {
                        DateTime currentDateTime = DateTime.UtcNow;

                        // Update only if the TimeStampAcknowledged is older than 1980 and the message is not pinned
                        if (message.TimeStampAcknowledged < new DateTime(1980, 1, 1, 0, 0, 0) && !message.Pinned)
                        {
                            _logger.UpdateMessages(message.Identity, message.TimeStamp, currentDateTime, false);

                            // Assuming you have a way to update the OnlinerMessage object
                            // message.OnlinerMessage.Pinned.Cyclic = false;
                            // message.OnlinerMessage.TimeStampAcknowledged.Cyclic = currentDateTime;

                            TcoAppDomain.Current.Logger.Information("Message acknowledged {@message}", new { Text = message.Text, Category = message.CategoryAsEnum });
                        }
                    }
                    else
                    {
                        TcoAppDomain.Current.Logger.Warning("No message found with identity: {@identity}", identity);
                    }

                    RefreshMessageDisplay();
                }
            }
            catch (Exception ex)
            {
                // Log the exception here
                TcoAppDomain.Current.Logger.Error("An error occurred while acknowledging message: {@error}", ex);
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

        IEnumerable<PlainTcoMessage> dbMessageDisplay  = new List<PlainTcoMessage>();
        public IEnumerable<PlainTcoMessage> DbMessageDisplay
        {
            get => dbMessageDisplay;

            private set
            {
                if (dbMessageDisplay == value)
                {
                    return;
                }

                SetProperty(ref dbMessageDisplay, value);
            }
        }

        public void FetchMessagesFromDb()
        {
            var latestMessages = _logger.ReadMessages();
            DbMessageDisplay = latestMessages;
        }

        public void UpdateAndFetchMessages()
        {
            LogMessages();
            FetchMessagesFromDb();
        }
    }
}
