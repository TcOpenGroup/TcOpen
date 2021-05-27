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
            var actual = new DummyLogger();
            Assert.IsNotNull(actual);
        }

        [Test()]
        public void DebugTest()
        {
            var actual = new DummyLogger();
            actual.Debug<object>("Debug message");
        }

        [Test()]
        public void ErrorTest()
        {
            var actual = new DummyLogger();
            actual.Error<object>("Error message");
        }

        [Test()]
        public void FatalTest()
        {
            var actual = new DummyLogger();
            actual.Fatal<object>("Fatal message");
        }

        [Test()]
        public void InformationTest()
        {
            var actual = new DummyLogger();
            actual.Information<object>("Information message");
        }

        [Test()]
        public void VerboseTest()
        {
            var actual = new DummyLogger();
            actual.Verbose<object>("Verbose message");
        }

        [Test()]
        public void WarningTest()
        {
            var actual = new DummyLogger();
            actual.Warning<object>("Warning message");
        }
    }
}