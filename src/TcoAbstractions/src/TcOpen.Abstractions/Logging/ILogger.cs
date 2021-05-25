using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Abstractions.Logging
{
    public interface ITcoLogger
    {
        void Debug<T>(string stringTemplate, T payload = default(T));
        void Verbose<T>(string stringTemplate, T payload = default(T));
        void Information<T>(string stringTemplate, T payload = default(T));
        void Warning<T>(string stringTemplate, T payload = default(T));
        void Error<T>(string stringTemplate, T payload = default(T));
        void Fatal<T>(string stringTemplate, T payload = default(T));

    }    
}
