using NUnit.Framework;
using TcOpen.Inxton.Swift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Swift.Tests
{
    [TestFixture()]
    public class RecorderTests
    {
        [Test()]
        public void RecorderTest()
        {
            var actual = new Recorder();
            Assert.IsNotNull(actual.Sequence);
        }
    }
}