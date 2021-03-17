using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoCoreTests
{
    public partial class TcoMessengerTests : ITestTcoContext
    {
        public void ContextClose()
        {
            ((TcoMessengerContextTest)Parent).ContextClose();
        }

        public void ContextOpen()
        {
            ((TcoMessengerContextTest)Parent).ContextOpen();
        }

        
    }
}
