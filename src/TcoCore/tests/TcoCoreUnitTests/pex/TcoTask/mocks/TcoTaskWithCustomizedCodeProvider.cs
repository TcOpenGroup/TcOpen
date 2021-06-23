using System;
using System.Linq;

namespace TcoCore.PexTests
{
    internal class TcoTaskWithCustomizedCodeProvider : TcoMomentaryTask
    {
        public TcoTaskWithCustomizedCodeProvider()
        {
            this.CodeProvider = new TcoTaskCustomizedCodeProvider();
        }
    }
}