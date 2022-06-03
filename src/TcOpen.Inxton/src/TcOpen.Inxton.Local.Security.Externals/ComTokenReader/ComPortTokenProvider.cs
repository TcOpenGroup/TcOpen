using System;
using System.IO.Ports;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security
{
    public class ComPortTokenProvider : IExternalAuthorization
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

        public string AuthorizationErrorMessage { get; set; }

        public bool WillChangeToken { get; set; }

        public delegate string GetDataDelegate(string recievedData);

        public GetDataDelegate GetDataHandler;

        public event AuthorizationRequestDelegate AuthorizationRequest;
        public event AuthorizationTokenChangeRequestDelegate AuthorizationTokenChange;

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

        public IUser RequestAuthorization()
        {
            throw new NotImplementedException();
        }

        public void RequestTokenChange()
        {
            throw new NotImplementedException();
        }
    }
}
