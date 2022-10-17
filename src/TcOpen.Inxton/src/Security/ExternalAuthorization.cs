using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Local.Security
{
    public abstract class ExternalAuthorization : IExternalAuthorization
    {      
        public event AuthorizationRequestDelegate AuthorizationRequest;
        public event AuthorizationTokenChangeRequestDelegate AuthorizationTokenChange;

        private IUser Authorize(string token)
        {
            return AuthorizationRequest(token);
        }

        private void ChangeToken(string token)
        {
            AuthorizationTokenChange(token);
        }
        
        public IUser RequestAuthorization(string token)
        {            
            AuthorizationErrorMessage = string.Empty;
            try
            {
                if (WillChangeToken)
                {
                    RequestTokenChange(token);
                }

                return Authorize(token);
            }
            catch (Exception ex)
            {
                AuthorizationErrorMessage = ex.Message;
            }

            return null;
        }

        public string AuthorizationErrorMessage
        {
            get;
            private set;
        }

        public void RequestTokenChange(string token)
        {
            ChangeToken(token);
            WillChangeToken = false;
        }

        public bool WillChangeToken
        {
            get;
            set;
        }
    }
}
