using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoCore
{
    public partial class TcoLogger
    {
        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {        
            expectDequeingTags = this._buffer.Select(p => p.ExpectDequeing as IValueTag).ToList();
        }

        /// <summary>
        /// Gets or set the minimal level category for PLC event logging.
        /// </summary>
        public eMessageCategory MinLogLevelCategory
        {
            get
            {
                return (eMessageCategory)this._minLoggingLevel.Synchron;
            }

            set
            {
                this._minLoggingLevel.Synchron = (short)value;
            }
        }

        /// <summary>
        /// Starts the event retrieval loop.
        /// <note type="important">
        /// The log retrieval operations for given logger can be started on one system only. 
        /// Make sure you do not run the log retrieval from the same logger in multiple instances.
        /// </note>
        /// </summary>
        /// <param name="minLevelCategory">Sets the minimal logging level.</param>
        public void StartLoggingMessages(eMessageCategory minLevelCategory)
        {
            this._minLoggingLevel.Synchron = (short)minLevelCategory;

            Task.Run(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(2);
                    LogMessages(Pop());
                }
            });
        }

        private IList<IValueTag> expectDequeingTags { get; set; }

        /// <summary>
        /// Pops messages from this logger.
        /// </summary>
        /// <returns>Messages from the logger</returns>
        public IEnumerable<PlainTcoMessage> Pop()
        {
            var poppedMessages = new List<PlainTcoMessage>();
            this.Connector.ReadBatch(expectDequeingTags);
            var messagesToDequeue = this._buffer.Where(p => p.ExpectDequeing.LastValue);

            if (messagesToDequeue.Count() > 0)
            {
                var messagesToReadValueTags = messagesToDequeue.SelectMany(p => p.RetrieveValueTags());
                this.Connector.ReadBatch(messagesToReadValueTags);
                
                foreach (var message in messagesToDequeue)
                {
                    var plain = message.LastPlainMessageNoTranslation;
                    PlainTcoMessage possiblySame =
                        poppedMessages
                        .Where(p => p.Equals(plain) && p.Cycle == plain.Cycle - 1)
                        .LastOrDefault();


                    if (possiblySame != null)
                    {
                        poppedMessages.Remove(possiblySame);
                        poppedMessages.Add(plain);
                    }
                    else
                    {
                        poppedMessages.Add(plain);
                    }
                   
                    message.ExpectDequeing.Cyclic = false;
                }

                this.Connector.WriteBatch(expectDequeingTags);
            }
            
            return poppedMessages;            
        }

        private List<PlainTcoMessage> ActiveMessages { get; } = new List<PlainTcoMessage>();

        /// <summary>
        /// Peeks messaging from this logger without effecting dequeing.
        /// </summary>
        /// <returns>Messages from this logger.</returns>
        public IEnumerable<PlainTcoMessage> Peek()
        {
            var poppedMessages = new List<PlainTcoMessage>();
            this.Connector.ReadBatch(expectDequeingTags);
            var messagesToDequeue = this._buffer.Where(p => p.ExpectDequeing.LastValue);

            if (messagesToDequeue.Count() > 0)
            {
                var messagesToReadValueTags = messagesToDequeue.SelectMany(p => p.RetrieveValueTags());
                this.Connector.ReadBatch(messagesToReadValueTags);

                foreach (var message in messagesToDequeue)
                {
                    var plain = message.LastPlainMessageNoTranslation;
                    poppedMessages.Add(plain);                    
                }                
            }

            return poppedMessages;
        }

        /// <summary>
        /// Logs messages into <see cref="TcOpen.Inxton.Logging.ITcoLogger"/> of this application.
        /// </summary>
        /// <param name="messages">Messages to push to the application logger.</param>
        public void LogMessages(IEnumerable<PlainTcoMessage> messages)
        {          
            foreach (var plain in messages)
            {
                LogMessage(plain, new { plain.ParentsObjectSymbol, plain.ParentsHumanReadable, plain.Cycle });               
            }
        }

        private void LogMessage(PlainTcoMessage message, object payload)
        {
            switch (message.CategoryAsEnum)
            {               
                case eMessageCategory.Debug:
                    TcOpen.Inxton.TcoAppDomain.Current.Logger.Debug($"{message.Text} {{@sender}}", payload);
                    break;
                case eMessageCategory.Trace:
                    TcOpen.Inxton.TcoAppDomain.Current.Logger.Verbose($"{message.Text} {{@sender}}", payload);
                    break;
                case eMessageCategory.Info:                              
                    TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"{message.Text} {{@sender}}", payload);
                    break;
                case eMessageCategory.TimedOut:
                case eMessageCategory.Notification:
                case eMessageCategory.Warning:
                    TcOpen.Inxton.TcoAppDomain.Current.Logger.Warning($"{message.Text} {{@sender}}", payload);
                    break;
                case eMessageCategory.Error:
                case eMessageCategory.ProgrammingError:
                    TcOpen.Inxton.TcoAppDomain.Current.Logger.Error($"{message.Text} {{@sender}}", payload);
                    break;
                case eMessageCategory.Critical:                   
                case eMessageCategory.Catastrophic:
                    TcOpen.Inxton.TcoAppDomain.Current.Logger.Fatal($"{message.Text} {{@sender}}", payload);
                    break;               
                default:
                    break;
            }
        }
    }
}
