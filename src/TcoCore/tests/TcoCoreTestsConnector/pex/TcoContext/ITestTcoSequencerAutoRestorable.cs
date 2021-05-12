using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoCoreTests
{
    public interface ITestTcoSequencerAutoRestorable
    {
        void ContextOpen();
        void SequencerOpen();
        void SequencerClose();
        void ContextClose();

    }

}
