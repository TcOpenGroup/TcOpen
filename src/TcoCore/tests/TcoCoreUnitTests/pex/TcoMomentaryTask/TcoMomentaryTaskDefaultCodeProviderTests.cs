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
    public class TcoMomentaryTaskDefaultCodeProviderTests
    {
        [Test()]
        public void task_on_test()
        {
            var momentaryTask = new TcoMomentaryTaskDefaultCodeProvider(
                new TcoMomentaryTask(new MockRootObject(), "MomentaryTask", "_momentaryTask")
            );
            var expected = "_momentaryTask.On()";
            var actual = momentaryTask.Code(true);

            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void task_off_test()
        {
            var momentaryTask = new TcoMomentaryTaskDefaultCodeProvider(
                new TcoMomentaryTask(new MockRootObject(), "MomentaryTask", "_momentaryTask")
            );
            var expected = "_momentaryTask.Off()";
            var actual = momentaryTask.Code(false);

            Assert.AreEqual(expected, actual);
        }
    }
}
