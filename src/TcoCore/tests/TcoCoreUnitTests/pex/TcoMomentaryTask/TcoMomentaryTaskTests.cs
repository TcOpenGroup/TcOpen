using NUnit.Framework;
using TcoCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton;
using TcoCore.Logging;

namespace TcoCore.PexTests
{
    [TestFixture()]
    public class TcoMomentaryTaskTests
    {

        private TcOpen.Inxton.Logging.DummyLoggerAdapter _logger;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _logger = new TcOpen.Inxton.Logging.DummyLoggerAdapter();
            TcoAppDomain.Current.Builder.SetUpLogger(_logger);
        }

        [Test()]
        public void PexCtor()
        {
            var task = new TcoMomentaryTask(new MockRootObject(), string.Empty, string.Empty);
            Assert.IsNotNull(task);
        }

        [Test()]
        public void LogPayloadPropertyTest()
        {
            var task = new TcoMomentaryTask();
            task.LogPayloadDecoration = () => "hey I am payload".ToUpper();

            var actual = task.LogPayloadDecoration();

            Assert.AreEqual("hey I am payload".ToUpper(), actual);
        }

        [Test()]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void CanExecuteTest_enabled_serviceable(bool isEnabled, bool isServiceable)
        {
            var task = new TcoMomentaryTask();
            task._enabled.Synchron = isEnabled;
            task._isServiceable.Synchron = isServiceable;
            var expected = isEnabled && isServiceable;

            var actual = task.CanExecute(new object());

            Assert.AreEqual(expected, actual);
        }

        [Test()]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void ExecuteTest(bool isEnabled, bool isServiceable)
        {
            var task = new TcoMomentaryTask();
            task._enabled.Synchron = isEnabled;
            task._isServiceable.Synchron = isServiceable;
            task._setOnRequest.Synchron = false;
            _logger.ClearLastMessage();
            var expected = isEnabled && isServiceable;

            task.Execute(new object());

            Assert.AreEqual(false, task._setOnRequest.Synchron);
            Assert.IsTrue(_logger.IsLastMessageEmpty());          
        }

        [Test()]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void StopTest(bool isEnabled, bool isServiceable)
        {
            var task = new TcoMomentaryTask();
            task._enabled.Synchron = isEnabled;
            task._isServiceable.Synchron = isServiceable;
            _logger.ClearLastMessage();
            var expected = isEnabled && isServiceable;

            task.Stop();

            Assert.AreEqual(false, task._setOnRequest.Synchron);

            if (expected)
            {
                Assert.AreEqual("Task '' stopped. {@sender}", _logger.LastMessage.message);
                Assert.IsInstanceOf<LogInfo>(_logger.LastMessage.payload);
                Assert.AreEqual("Information", _logger.LastMessage.serverity);
            }
            else
            {
                Assert.IsTrue(_logger.IsLastMessageEmpty());
            }
        }

        [Test()]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void StartTest(bool isEnabled, bool isServiceable)
        {
            var task = new TcoMomentaryTask();
            task._enabled.Synchron = isEnabled;
            task._isServiceable.Synchron = isServiceable;
            task._setOnRequest.Synchron = false;
            _logger.ClearLastMessage();
            var expected = isEnabled && isServiceable;

            task.Start();

            Assert.AreEqual(expected, task._setOnRequest.Synchron);

            if (expected)
            {
                Assert.AreEqual("Task '' invoked. {@sender}", _logger.LastMessage.message);
                Assert.IsInstanceOf<LogInfo>(_logger.LastMessage.payload);
                Assert.AreEqual("Information", _logger.LastMessage.serverity);
            }
            else
            {
                Assert.IsTrue(_logger.IsLastMessageEmpty());
            }
        }

        [Test()]
        public void CodeProvider_get_default_when_null()
        {
            var task = new TcoMomentaryTask();
            Assert.IsInstanceOf<TcoCore.Swift.TcoMomentaryTaskDefaultCodeProvider>(task.CodeProvider);
        }

        [Test()]
        public void CodeProvider_get_customized()
        {
            var task = new TcoMomentaryTaskWithCustomizedCodeProvider();
            Assert.IsInstanceOf<TcoMomentaryTaskCustomizedCodeProvider>(task.CodeProvider);
        }

        [Test()]
        public void RecordTaskAction_get_set_test()
        {
            var task = new TcoMomentaryTask();
            Assert.IsNull(task.RecordTaskAction);
            task.RecordTaskAction = (a, b) => Console.WriteLine();
            Assert.IsNotNull(task.RecordTaskAction);
        }
    }
}