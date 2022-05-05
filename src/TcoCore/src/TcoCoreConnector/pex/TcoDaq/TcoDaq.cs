using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Logging;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoCore
{
    public partial class TcoDaq
    {

        public IEnumerable<TcoDaqItem> Buffer { get { return this.GetKids().Where(p => p is TcoDaqItem).Select(p => p as TcoDaqItem); } }

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {        
                
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
        /// <param name="logTarget">Set custom log target. If default|null default application logger is used.</param>
        public void StartLoggingMessages<T>(eMessageCategory minLevelCategory, int interLoopDelay = 25, ILogger logTarget = null) where T : PlainTcoDaqItem
        {                        
            Task.Run(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(interLoopDelay);
                    if (LogMessages != null)
                    {
                    LogMessages(Pop<T>());
                }
                }
            });
        }

        private IList<IValueTag> expectDequeingTags { get { return this.Buffer.Select(p => p.ExpectDequeing as IValueTag).ToList(); } }
        
        /// <summary>
        /// Pops messages from this logger.
        /// </summary>
        /// <returns>Messages from the logger</returns>
        public IEnumerable<T> Pop<T>() where T : PlainTcoDaqItem
        {
            var poppedMessages = new List<T>();
            this.Connector.ReadBatch(expectDequeingTags);
            var messagesToDequeue = this.Buffer.Where(p => p.ExpectDequeing.LastValue);
            
            if (messagesToDequeue.Count() > 0)
            {               
                var messagesToReadValueTags = messagesToDequeue.SelectMany(p => p.RetrieveValueTags());
                this.Connector.ReadBatch(messagesToReadValueTags);
                
                foreach (var message in messagesToDequeue)
                {
                    var plain = message.LastKnownPlain;                  
                    poppedMessages.Add(plain as T);                                       
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
        public IEnumerable<PlainTcoDaqItem> Peek()
        {
            var poppedMessages = new List<PlainTcoDaqItem>();
            this.Connector.ReadBatch(expectDequeingTags);
            var messagesToDequeue = this.Buffer.Where(p => p.ExpectDequeing.LastValue);

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

        LogMessagesDelegate<PlainTcoDaqItem> LogMessages;
        
        public delegate void LogMessagesDelegate<T>(IEnumerable<T> messages) where T : PlainTcoDaqItem;
                
            }
        }       
    }
}
