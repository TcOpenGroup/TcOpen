using System;
using System.Collections.Generic;
using System.Text;

namespace TcoTestRunners
{
    public interface ITestContext
    {
        bool ContextOpen();
        bool ContextClose();

    }
}
