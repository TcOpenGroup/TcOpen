using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using PlcDocu.TcoCore;

using TcoCore.TcoDiagnosticsAlternative.LoggingToDb;

using TcOpen.Inxton;
using TcOpen.Inxton.Input;
using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;

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
        //public eMessageCategory MinMessageCategoryFilter
        //{
        //    get;
        //    set;
        //} = DefaultCategory;

        //public static eMessageCategory SetDefaultCategory(eMessageCategory item) => DefaultCategory = item;
        //public static eMessageCategory DefaultCategory { get; set; } = eMessageCategory.Info;

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

        internal void LogMesssages()
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

                foreach (var message in (newMessageDisplay))
                {
                    if (_logger != null)
                    {
                    _logger.LogMessage(message);
                    }
                    
                    activeMessages.Add(message.Identity);
                }

            // Remove or acknowledge messages that are no longer active
            var messagesToRemove = activeMessages.Where(id => !newMessageIdentities.Contains(id)).ToList();
                foreach (var id in messagesToRemove)
                {
                    //AcknowledgeMessages();
                    AcknowledgeMessage(id);
                    activeMessages.Remove(id);
                }

                // Update the MessageDisplay
                MessageDisplay = newMessageDisplay;

                DiagnosticsRunning = false;
            }
           
        }

        public void AcknowledgeMessages()
        {
            try
            {
                lock (updatemutex)
                {
                    TcoAppDomain.Current.Logger.Information("All message acknowledged {@payload}", new { rootObject = _tcoObject.HumanReadable, rootSymbol = _tcoObject.Symbol });

                    foreach (var item in DbMessageDisplay.Where(p => p.TimeStampAcknowledged == null))

                        {
                            DateTime currentDateTime = DateTime.UtcNow;
                        // Check the MessageDisplay for the same identity
                        var activeMessage = MessageDisplay.FirstOrDefault(m => m.Identity == item.Identity);
                        bool isActive = false;

                        if (activeMessage != null)
                        {
                            activeMessage.OnlinerMessage.Pinned.Cyclic = false;
                            Thread.Sleep(100);
                            isActive = activeMessage.OnlinerMessage.IsActive;
                        }

                        if (!isActive)
                        {
                            _logger.UpdateMessages(item.Identity, item.TimeStamp, currentDateTime, false);
                        }
                        else
                        {
                            TcoAppDomain.Current.Logger.Information("Fehler scheint noch aktiv {@payload}", new { rootObject = _tcoObject.HumanReadable, rootSymbol = _tcoObject.Symbol });
                        }

                        TcoAppDomain.Current.Logger.Information("Message acknowledged {@message}", new { Text = item.Text, Category = item.CategoryAsEnum });
                    }
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
                    // Find all messages with the given identity and where TimeStampAcknowledged is null
                    var messagesToAcknowledge = DbMessageDisplay.Where(m => m.Identity == identity && m.TimeStampAcknowledged == null).ToList();

                    if (messagesToAcknowledge.Any())
                    {
                        DateTime currentDateTime = DateTime.UtcNow;

                        foreach (var messageToAcknowledge in messagesToAcknowledge)
                        {
                            // Check the MessageDisplay for the same identity
                            var activeMessage = MessageDisplay.FirstOrDefault(m => m.Identity == messageToAcknowledge.Identity);
                            bool isActive = false;

                            if (activeMessage != null)
                            {
                                activeMessage.OnlinerMessage.Pinned.Cyclic = false;
                                Thread.Sleep(100);
                                isActive = activeMessage.OnlinerMessage.IsActive;
                            }

                            if (!isActive)
                            {
                                _logger.UpdateMessages(messageToAcknowledge.Identity, messageToAcknowledge.TimeStamp, currentDateTime, false);
                                TcoAppDomain.Current.Logger.Information("Message with identity {@identity} acknowledged", identity);
                            }
                            else
                            {
                                TcoAppDomain.Current.Logger.Information("Fehler scheint noch aktiv {@payload}", new { rootObject = _tcoObject.HumanReadable, rootSymbol = _tcoObject.Symbol });
                            }
                        }
                    }
                    else
                    {
                        TcoAppDomain.Current.Logger.Warning("No message found with identity: {@identity} that has a null TimeStampAcknowledged", identity);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }



        public void RefreshMessageDisplay()
        {
            //AcknowledgeMessages();
            Console.WriteLine($"Refresh");
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

        IEnumerable<PlainTcoMessageExtended> dbMessageDisplay = new List<PlainTcoMessageExtended>();
        public IEnumerable<PlainTcoMessageExtended> DbMessageDisplay
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
            LogMesssages();
            FetchMessagesFromDb();
        }
        public eMessageCategory MinMessageCategoryFilter { get; set; }

    }
}