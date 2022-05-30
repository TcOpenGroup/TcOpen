using System;
using System.IO.Ports;
using System.Linq;

namespace TcOpen.Inxton.Local.Security
{
    public interface ITokenProvider
    {
        void SetReceiveEventHandler(SerialDataReceivedEventHandler dataRecivedEventHandler);        
        string Received { get; }
    }
}
