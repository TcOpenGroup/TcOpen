using System;
using System.Collections.Generic;
using System.Text;

namespace TcoCore.Testing
{
    public interface ITestContext
    {
        bool ContextOpen();
        bool ContextClose();      
    }
}
