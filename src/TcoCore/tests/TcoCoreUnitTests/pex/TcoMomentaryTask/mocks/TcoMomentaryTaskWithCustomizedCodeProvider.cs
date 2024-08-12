using System;
using System.Linq;

namespace TcoCore.PexTests
{
    internal class TcoMomentaryTaskWithCustomizedCodeProvider : TcoMomentaryTask
    {
        public TcoMomentaryTaskWithCustomizedCodeProvider()
        {
            this.CodeProvider = new TcoMomentaryTaskCustomizedCodeProvider();
        }
    }
}
