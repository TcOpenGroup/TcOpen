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

        private void Authorize(string token, bool deauthenticateWhenSame)
        {
            AuthorizationRequest(token, deauthenticateWhenSame);
        }

        private void ChangeToken(string token)
        {
            AuthorizationTokenChange(token);
        }

        public void RequestAuthorization(string token, bool deauthenticateWhenSame)
        {
            AuthorizationErrorMessage = string.Empty;
            try
            {
                if (WillChangeToken)
                {
                    RequestTokenChange(token);
                }

                Authorize(token, deauthenticateWhenSame);
            }
            catch (Exception ex)
            {
                AuthorizationErrorMessage = ex.Message;
            }
        }

        public string AuthorizationErrorMessage { get; private set; }

        public void RequestTokenChange(string token)
        {
            ChangeToken(token);
            WillChangeToken = false;
        }

        public bool WillChangeToken { get; set; }
    }
}
