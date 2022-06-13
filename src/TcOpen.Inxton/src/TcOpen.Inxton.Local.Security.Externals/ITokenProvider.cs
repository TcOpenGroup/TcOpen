using System;
using System.IO.Ports;
using System.Linq;

namespace TcOpen.Inxton.Local.Security
{
    /// <summary>
    /// Interface of generic Token provider.
    /// </summary>
    public interface ITokenProvider
    {
        void SetTokenReceivedAction(Action<string> tokenReceivedAction);               
    }
}
