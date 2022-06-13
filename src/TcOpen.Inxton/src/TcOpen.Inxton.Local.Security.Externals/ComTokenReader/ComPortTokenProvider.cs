using System;
using System.IO.Ports;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security
{
    public class ComPortTokenProvider : ITokenProvider
    {
        private readonly SerialPort port;

        private string _tokenData;

        public ComPortTokenProvider(string portName, int baudRate = 9600, int dataBits = 8, StopBits stopBits = StopBits.One, Parity parity = Parity.None)
        {
            port = new SerialPort();
            port.BaudRate = baudRate;
            port.Parity = parity;
            port.PortName = portName;
            port.DataBits = dataBits;
            port.StopBits = stopBits;
            port.Open();
            port.DataReceived += PortDataReceived;
        }


        public void SetTokenReceivedAction(Action<string> tokenReceivedAction)
        {
            IncomingTokenAction = tokenReceivedAction;
        }
                
        public Action<string> IncomingTokenAction;
      
        
        void PortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {                             
                IncomingTokenAction?.Invoke(port.ReadExisting());
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}
