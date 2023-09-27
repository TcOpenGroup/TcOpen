using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using TcOpen.Inxton.Input;
using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;
using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Services;

using Vortex.Presentation;


namespace TcoCore
{
    public class TcoDiagnosticsAlternativeViewModel : RenderableViewModelBase
    {
        private readonly DataService _dataService;

        public TcoDiagnosticsAlternativeViewModel()
        {
          
        }

        public TcoDiagnosticsAlternativeViewModel(DataService dataService)
        {
            _dataService = dataService;
        }

        //public TcoDiagnosticsAlternativeViewModel(IMongoLogger logger)
        //{
        //    _logger = logger;
        //}

        public TcoDiagnosticsAlternativeViewModel(IsTcoObject tcoObject, DataService dataService)
        {
            _tcoObject = tcoObject;
            _dataService = dataService;
        }

        //public TcoDiagnosticsAlternativeViewModel(IMongoLogger logger, IsTcoObject tcoObject)
        //{
        //    _logger = logger;
        //    _tcoObject = tcoObject;
        //}

      
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

        ////========================
        //public event Action<PlainTcoMessage> NewMessageReceived;
        //private volatile object updatemutex = new object();
        //private HashSet<ulong> activeMessages = new HashSet<ulong>();

       

        //public void AcknowledgeMessages()
        //{
        //    try
        //    {
        //        lock (updatemutex)
        //        {
        //            TcoAppDomain.Current.Logger.Information("All message acknowledged {@payload}", new { rootObject = _tcoObject.HumanReadable, rootSymbol = _tcoObject.Symbol });

        //            foreach (var item in DbMessageDisplay.Where(p => p.TimeStampAcknowledged == null))

        //                {
        //                    DateTime currentDateTime = DateTime.UtcNow;
        //                // Check the MessageDisplay for the same identity
        //                var activeMessage = MessageDisplay.FirstOrDefault(m => m.Identity == item.Identity);
        //                bool isActive = false;

        //                if (activeMessage != null)
        //                {
        //                    activeMessage.OnlinerMessage.Pinned.Cyclic = false;
        //                    Thread.Sleep(100);
        //                    isActive = activeMessage.OnlinerMessage.IsActive;
        //                }

        //                if (!isActive)
        //                {
        //                    _logger.UpdateMessages(item.Identity, item.TimeStamp, currentDateTime, false);
        //                }
        //                else
        //                {
        //                    TcoAppDomain.Current.Logger.Information("Fehler scheint noch aktiv {@payload}", new { rootObject = _tcoObject.HumanReadable, rootSymbol = _tcoObject.Symbol });
        //                }

        //                TcoAppDomain.Current.Logger.Information("Message acknowledged {@message}", new { Text = item.Text, Category = item.CategoryAsEnum });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception here
        //        TcoAppDomain.Current.Logger.Error("An error occurred while acknowledging messages: {@error}", ex);
        //    }
        //}

        //public void AcknowledgeMessage(ulong identity)
        //{
        //    try
        //    {
        //        lock (updatemutex)
        //        {
        //            // Find all messages with the given identity and where TimeStampAcknowledged is null
        //            var messagesToAcknowledge = DbMessageDisplay.Where(m => m.Identity == identity && m.TimeStampAcknowledged == null).ToList();

        //            if (messagesToAcknowledge.Any())
        //            {
        //                DateTime currentDateTime = DateTime.UtcNow;

        //                foreach (var messageToAcknowledge in messagesToAcknowledge)
        //                {
        //                    // Check the MessageDisplay for the same identity
        //                    var activeMessage = MessageDisplay.FirstOrDefault(m => m.Identity == messageToAcknowledge.Identity);
        //                    bool isActive = false;

        //                    if (activeMessage != null)
        //                    {
        //                        activeMessage.OnlinerMessage.Pinned.Cyclic = false;
        //                        Thread.Sleep(100);
        //                        isActive = activeMessage.OnlinerMessage.IsActive;
        //                    }

        //                    if (!isActive)
        //                    {
        //                        _logger.UpdateMessages(messageToAcknowledge.Identity, messageToAcknowledge.TimeStamp, currentDateTime, false);
        //                        TcoAppDomain.Current.Logger.Information("Message with identity {@identity} acknowledged", identity);
        //                    }
        //                    else
        //                    {
        //                        TcoAppDomain.Current.Logger.Information("Fehler scheint noch aktiv {@payload}", new { rootObject = _tcoObject.HumanReadable, rootSymbol = _tcoObject.Symbol });
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                TcoAppDomain.Current.Logger.Warning("No message found with identity: {@identity} that has a null TimeStampAcknowledged", identity);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}

        //public void RefreshMessageDisplay()
        //{
        //    //AcknowledgeMessages();
        //    Console.WriteLine($"Refresh");
        //    MessageDisplay = _logger.ReadMessages();
        //}

        //public List<PlainTcoMessage> Messages { get; private set; } = new List<PlainTcoMessage>();

        //IEnumerable<PlainTcoMessage> messageDisplay = new List<PlainTcoMessage>();

        //public IEnumerable<PlainTcoMessage> MessageDisplay
        //{
        //    get => messageDisplay;

        //    private set
        //    {
        //        if (messageDisplay == value)
        //        {
        //            return;
        //        }

        //        SetProperty(ref messageDisplay, value);
        //    }
        //}

        //IEnumerable<PlainTcoMessageExtended> dbMessageDisplay = new List<PlainTcoMessageExtended>();
        //public IEnumerable<PlainTcoMessageExtended> DbMessageDisplay
        //{
        //    get => dbMessageDisplay;

        //    private set
        //    {
        //        if (dbMessageDisplay == value)
        //        {
        //            return;
        //        }

        //        SetProperty(ref dbMessageDisplay, value);
        //    }
        //}

        
    }
}