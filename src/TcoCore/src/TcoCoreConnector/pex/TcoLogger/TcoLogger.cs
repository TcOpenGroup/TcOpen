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
           // this._index.Subscribe(RetrieveMessages);
        }

        public void StartLoggingMessages()
        {
            this._index.Subscribe(RetrieveMessages);
        }

        private int lastIndex = 0;
        private volatile object mutex = new object();
        private void RetrieveMessages(IValueTag sender, ValueChangedEventArgs args)
        {
            lock (mutex)
            {
                Task.Run(() =>
                {
                    var index = this._index.Cyclic;

                    if (index < lastIndex)
                    {
                        lastIndex = 0;
                    }

                    while(lastIndex <= index)
                    {
                        var plain = this._buffer.Buffer[lastIndex].LogPlainMessage;
                        LogMessage(plain, new { plain.ParentsObjectSymbol, plain.ParentsHumanReadable });
                        lastIndex++;
                    }                    
                });
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
                case eMessageCategory.TimedOut:                    
                case eMessageCategory.Notification:
                    TcOpen.Inxton.TcoAppDomain.Current.Logger.Information($"{message.Text} {{@sender}}", payload);
                    break;
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
