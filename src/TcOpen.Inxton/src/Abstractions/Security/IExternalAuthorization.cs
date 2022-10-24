using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Security
{
    public interface IExternalAuthorization
    {
        void RequestAuthorization(string token, bool deauthenticateWhenSame);
        void RequestTokenChange(string token);

        string AuthorizationErrorMessage { get; }
        bool WillChangeToken { get; set; }
        event AuthorizationRequestDelegate AuthorizationRequest;
        event AuthorizationTokenChangeRequestDelegate AuthorizationTokenChange;
    }

    public delegate void AuthorizationRequestDelegate(string token, bool deauthenticateWhenSame);
    public delegate void AuthorizationTokenChangeRequestDelegate(string token);
}
