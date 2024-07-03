using System;
using System.Linq;

namespace TcoCore.PexTests
{
    internal class TcoToggleTaskWithCustomizedCodeProvider : TcoMomentaryTask
    {
        public TcoToggleTaskWithCustomizedCodeProvider()
        {
            this.CodeProvider = new TcoToggleTaskCustomizedCodeProvider();
        }
    }
}
