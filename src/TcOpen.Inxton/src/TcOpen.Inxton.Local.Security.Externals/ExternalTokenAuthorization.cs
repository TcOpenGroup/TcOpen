using System;
using System.IO.Ports;

namespace TcOpen.Inxton.Local.Security.Readers
{    
    public class ExternalTokenAuthorization : ExternalAuthorization
    {
        private readonly ITokenProvider _tokenProvider;

        public ExternalTokenAuthorization(ITokenProvider tokenProvider)
        {
            this._tokenProvider = tokenProvider;
            this._tokenProvider.SetReceiveEventHandler(this.GetData);
        }
        
        private string _token;

        protected override string GetToken()
        {
            return _token;
        }

        private void GetData(object sender, SerialDataReceivedEventArgs e)
        {
            System.Threading.Thread.Sleep(500);
            _token = this._tokenProvider.Received;
            this.RequestAuthorization();
        }
    }
}
