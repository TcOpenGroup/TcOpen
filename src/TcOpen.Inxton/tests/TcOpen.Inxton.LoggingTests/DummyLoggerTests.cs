using NUnit.Framework;
using TcOpen.Inxton.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Logging.Tests
{
    [TestFixture()]
    public class DummyLoggerTests
    {
        [Test()]
        public void DummyLoggerTest()
        {
            var actual = new DummyLoggerAdapter();
            Assert.IsNotNull(actual);
        }

        [Test()]
        public void DebugTest()
        {
            var actual = new DummyLoggerAdapter();
            actual.Debug<object>("Debug message");
        }

        [Test()]
        public void ErrorTest()
        {
            var actual = new DummyLoggerAdapter();
            actual.Error<object>("Error message");
        }

        [Test()]
        public void FatalTest()
        {
            var actual = new DummyLoggerAdapter();
            actual.Fatal<object>("Fatal message");
        }

        [Test()]
        public void InformationTest()
        {
            var actual = new DummyLoggerAdapter();
            actual.Information<object>("Information message");
        }

        [Test()]
        public void VerboseTest()
        {
            var actual = new DummyLoggerAdapter();
            actual.Verbose<object>("Verbose message");
        }

        [Test()]
        public void WarningTest()
        {
            var actual = new DummyLoggerAdapter();
            actual.Warning<object>("Warning message");
        }
    }
}