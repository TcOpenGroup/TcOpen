using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoCore
{
    public interface IsTcoContext
    {
        void AddMessage(TcoMessage message);

        IEnumerable<PlainTcoMessage> ActiveMessages { get; }

        ulong LastStartCycleCount { get; }

        void RefreshActiveMessages();
    }
}
