using NUnit.Framework;
using TcoCore.Swift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoCore.Swift.Tests
{
    [TestFixture()]
    public class TcoTaskCodeProviderTests
    {
        [Test()]
        public void TcoTaskCodeProviderTest()
        {
            var actual = new TcoTaskDefaultCodeProvider(new TcoTask(new MockRootObject(), "SomeTask", "_task"));
            Assert.IsNotNull(actual);
        }

        [Test()]
        public void TcoTaskCodeProviderCodeTest()
        {
            var taskCodeProvider = new TcoTaskDefaultCodeProvider(new TcoTask(new MockRootObject(), "SomeTask", "_task"));
            var actual = taskCodeProvider.Code();
            Assert.AreEqual("_task.Invoke().Done",actual);
        }
    }
}