using System;
using System.IO.Ports;

namespace TcOpen.Inxton.Local.Security.Readers
{    
    /// <summary>
    /// External authentication provider for TcOpen application.
    /// </summary>
    public class ExternalTokenAuthorization : ExternalAuthorization
    {
        private readonly ITokenProvider _tokenProvider;

        /// <summary>
        /// Creates new instance of External token authentication.
        /// </summary>
        /// <param name="tokenProvider">Token provider.</param>
        public ExternalTokenAuthorization(ITokenProvider tokenProvider, bool deauthenticateWhenSame)
        {
            this._tokenProvider = tokenProvider;
            this._tokenProvider.SetTokenReceivedAction((token) => this.RequestAuthorization(token, deauthenticateWhenSame));
        }

        /// <summary>
        /// Creates serial port reader for external authentication.
        /// </summary>
        /// <param name="portName">Com port name</param>
        /// <param name="baudRate">Baud rate</param>
        /// <param name="dataBits">Data bits</param>
        /// <param name="stopBits">Stop bits</param>
        /// <param name="parity">Parity</param>
        /// <returns>External authentication provider.</returns>
        public static ExternalAuthorization CreateComReader(string portName, int baudRate = 9600, int dataBits = 8, StopBits stopBits = StopBits.One, Parity parity = Parity.None, bool deauthenticateWhenSame = true)
        {
           return new ExternalTokenAuthorization(new ComPortTokenProvider(portName, baudRate, dataBits, stopBits, parity), deauthenticateWhenSame);
        }

        /// <summary>
        /// Creates external authorization for token present in a string variable the PLC program.
        /// </summary>
        /// <param name="tokenValue">Onliner string containing the value of authentication token.</param>
        /// <param name="tokenPresence">Onliner bool indicating presence of authentication token.</param>
        /// <returns></returns>
        public static ExternalAuthorization CreatePlcTokenReader(Vortex.Connector.ValueTypes.OnlinerString tokenValue,
                                                                 Vortex.Connector.ValueTypes.OnlinerBool tokenPresence,
                                                                 bool deauthenticateWhenSame = true)
        {
            return new ExternalTokenAuthorization(new PlcTokenReader(tokenValue, tokenPresence), deauthenticateWhenSame);
        }

    }
}
