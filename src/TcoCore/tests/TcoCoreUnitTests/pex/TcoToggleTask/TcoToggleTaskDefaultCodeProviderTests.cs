using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TcoCore.Swift;

namespace TcoCore.Swift.Tests
{
    [TestFixture()]
    public class TcoToggleTaskDefaultCodeProviderTests
    {
        [Test()]
        public void TcoToggleTaskDefaultCodeProviderTest()
        {
            var actual = new TcoToggleTaskDefaultCodeProvider(
                new TcoToggleTask(new MockRootObject(), "ToggleTask", "_toggleTask")
            );
            Assert.IsNotNull(actual);
        }

        [Test()]
        public void TcoToggleTaskDefaultCodeTest()
        {
            var taskCodeProvider = new TcoToggleTaskDefaultCodeProvider(
                new TcoToggleTask(new MockRootObject(), "ToggleTask", "_toggleTask")
            );
            var actual = taskCodeProvider.Code();
            Assert.AreEqual("_toggleTask.Toggle()", actual);
        }
    }
}
