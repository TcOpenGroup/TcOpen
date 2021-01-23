using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoCoreTests
{
    public interface ITestTcoContext
    {
        void ContextOpen();
        void ContextClose();

    }

}
