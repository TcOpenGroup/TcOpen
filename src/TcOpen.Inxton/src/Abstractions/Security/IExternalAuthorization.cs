using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Security
{
    public interface IExternalAuthorization
    {
        IUser RequestAuthorization(string token);
        void RequestTokenChange(string token);

        string AuthorizationErrorMessage { get; }
        bool WillChangeToken { get; set; }
        event AuthorizationRequestDelegate AuthorizationRequest;
        event AuthorizationTokenChangeRequestDelegate AuthorizationTokenChange;
    }

    public delegate IUser AuthorizationRequestDelegate(string token);
    public delegate void AuthorizationTokenChangeRequestDelegate(string token);
}
