using System;
using System.IO.Ports;
using TcOpen.Inxton.Security;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcOpen.Inxton.Local.Security
{
    /// <summary>
    ///  Provides access to authentication token data from the PLC.
    /// </summary>
    public class PlcTokenReader : ITokenProvider
    {

        private readonly OnlinerString _valueToken;
        private readonly OnlinerBool _tokenPresence;

        /// <summary>
        /// Creates new instance of <see cref="PlcTokenReader"/>
        /// </summary>
        /// <param name="valueToken">Onliner of the variable containing value of the token</param>
        /// <param name="tokenPresence">Onliner indicating whether the authentication token is present (inserted/active)</param>
        public PlcTokenReader(OnlinerString valueToken, OnlinerBool tokenPresence)
        {
            _valueToken = valueToken;
            _tokenPresence = tokenPresence;

            _valueToken?.Subscribe(TagDataChanged);
            _tokenPresence?.Subscribe(TagDataPresence);
        }

        public void SetTokenReceivedAction(Action<string> tokenReceivedAction)
        {
            IncomingTokenAction = tokenReceivedAction;
        }
                
        public Action<string> IncomingTokenAction;
      
        
        void TagDataChanged(IValueTag sender, ValueChangedEventArgs args)
        {
            try
            {
                if (_tokenPresence.Synchron)
                {
                    IncomingTokenAction?.Invoke(_valueToken.Cyclic);
                }
                else
                {
                    SecurityManager.Manager.Service.DeAuthenticateCurrentUser();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        void TagDataPresence(IValueTag sender, ValueChangedEventArgs args)
        {
            try
            {
                if(_tokenPresence.Synchron == false)
                { 
                    SecurityManager.Manager.Service.DeAuthenticateCurrentUser();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
