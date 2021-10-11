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
        /// <param name="interLoopDelay">Sets the delay between retrievals of logs.</param>
        public void StartLoggingMessages(eMessageCategory minLevelCategory, int interLoopDelay = 25)
        {
            this._minLoggingLevel.Synchron = (short)minLevelCategory;

            Task.Run(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(interLoopDelay);
                    LogMessages(Pop());
                }
            });
        }

        private IList<IValueTag> expectDequeingTags { get; set; }
        
        /// <summary>
        /// Pops messages from this logger.
        /// </summary>
        /// <returns>Messages from the logger</returns>
        public IEnumerable<PlainTcoLogItem> Pop()
        {
            var poppedMessages = new List<PlainTcoLogItem>();
            this.Connector.ReadBatch(expectDequeingTags);
            var messagesToDequeue = this._buffer.Where(p => p.ExpectDequeing.LastValue);
            
            if (messagesToDequeue.Count() > 0)
            {               
                var messagesToReadValueTags = messagesToDequeue.SelectMany(p => p.RetrieveValueTags());
                this.Connector.ReadBatch(messagesToReadValueTags);
                
                foreach (var message in messagesToDequeue)
                {
                    var plain = message.LastKnownPlain;                  
                    poppedMessages.Add(plain);                                       
                    message.ExpectDequeing.Cyclic = false;
                }

                this.Connector.WriteBatch(expectDequeingTags);
            }
            
            return poppedMessages;            
        }      

        /// <summary>
        /// Peeks messaging from this logger without effecting dequeuing.
        /// </summary>
        /// <returns>Messages from this logger.</returns>
        public IEnumerable<PlainTcoLogItem> Peek()
        {
            var poppedMessages = new List<PlainTcoLogItem>();
            this.Connector.ReadBatch(expectDequeingTags);
            var messagesToDequeue = this._buffer.Where(p => p.ExpectDequeing.LastValue);

            if (messagesToDequeue.Count() > 0)
            {
                var messagesToReadValueTags = messagesToDequeue.SelectMany(p => p.RetrieveValueTags());
                this.Connector.ReadBatch(messagesToReadValueTags);

                foreach (var message in messagesToDequeue)
                {
                    var plain = message.LastKnownPlain;
                    poppedMessages.Add(plain);
                    message.ExpectDequeing.Cyclic = false;
                }

                this.Connector.WriteBatch(expectDequeingTags);
            }

            return poppedMessages;
        }

        /// <summary>
        /// Logs messages into <see cref="TcOpen.Inxton.Logging.ITcoLogger"/> of this application.
        /// </summary>
        /// <param name="messages">Messages to push to the application logger.</param>
        public void LogMessages(IEnumerable<PlainTcoLogItem> messages)
        {          
            foreach (var plain in messages)
            {
                LogMessage(plain, new { Logger = this.Symbol, ParentSymbol = plain.ParentsObjectSymbol, ParentName = plain.ParentsHumanReadable, Cycle = plain.Cycle });               
            }
        }

        private void LogMessage(PlainTcoLogItem message, object payload)
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
