using System;
using System.IO.Ports;
using System.Linq;

namespace TcOpen.Inxton.Local.Security
{
    internal class ComPortTokenProvider : ITokenProvider
    {
        private readonly SerialPort port;

        private string recieved;

        public ComPortTokenProvider(string portName, int baudRate, int dataBits, StopBits stopBits, Parity parity)
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

        public void SetReceiveEventHandler(SerialDataReceivedEventHandler dataRecivedEventHandler)
        {
            port.DataReceived -= PortDataReceived;
            port.DataReceived += dataRecivedEventHandler;
        }

        public string Received
        {
            get
            {
                return recieved;
            }
        }

        public delegate string GetDataDelegate(string recievedData);

        public GetDataDelegate GetDataHandler;

        void PortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                recieved = port.ReadTo("\r");

                if (GetDataHandler != null)
                    GetDataHandler(recieved);

                System.Threading.Thread.Sleep(1000);

                recieved = port.ReadExisting();
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
}
