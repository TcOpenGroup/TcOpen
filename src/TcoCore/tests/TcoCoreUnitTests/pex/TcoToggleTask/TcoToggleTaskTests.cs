using NUnit.Framework;
using TcoCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCore.Logging;
using TcOpen.Inxton;

namespace TcoCore.PexTests
{
    [TestFixture()]
    public class TcoToggleTaskTests
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
            var task = new TcoToggleTask(new MockRootObject(), string.Empty, string.Empty);
            Assert.IsNotNull(task);
        }

        [Test()]
        public void LogPayloadPropertyTest()
        {
            var task = new TcoToggleTask();
            task.LogPayloadDecoration = () => "hey I am payload".ToUpper();

            var actual = task.LogPayloadDecoration();

            Assert.AreEqual("hey I am payload".ToUpper(), actual);
        }

        [Test()]
        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void CanExecuteTest(bool isEnabled, bool isServiceable)
        {
            var task = new TcoToggleTask();
            task._enabled.Synchron = isEnabled;
            task._isServiceable.Synchron = isServiceable;
            var expected = isEnabled && isServiceable;

            var actual = task.CanExecute(null);

            Assert.AreEqual(expected, actual);

        }

        [Test()]
        [TestCase(false, false)]
        [TestCase(false, true)]
        [TestCase(true, false)]
        [TestCase(true, true)]
        public void ExecuteTest_toggle_false_to_true(bool isEnabled, bool isServiceable)
        {
            var task = new TcoToggleTask();
            task._enabled.Synchron = isEnabled;
            task._isServiceable.Synchron = isServiceable;
            task._toggleRequest.Synchron = false;
            _logger.ClearLastMessage();
            var canExecute = isEnabled && isServiceable;
            var expected = !task._toggleRequest.Synchron && canExecute;

            task.Execute(new object());

            Assert.AreEqual(expected, task._toggleRequest.Synchron);
         

            if (canExecute)
            {
                Assert.AreEqual("Task '' toggled 'Off -> On'. {@sender}", _logger.LastMessage.message);
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
            var task = new TcoToggleTask();
            Assert.IsInstanceOf<TcoCore.Swift.TcoToggleTaskDefaultCodeProvider>(task.CodeProvider);
        }

        [Test()]
        public void CodeProvider_get_customized()
        {
            var task = new TcoToggleTaskWithCustomizedCodeProvider();
            Assert.IsInstanceOf<TcoToggleTaskCustomizedCodeProvider>(task.CodeProvider);
        }

        [Test()]
        public void RecordTaskAction_get_set_test()
        {
            var task = new TcoToggleTask();
            Assert.IsNull(task.RecordTaskAction);
            task.RecordTaskAction = (a, b) => Console.WriteLine();
            Assert.IsNotNull(task.RecordTaskAction);
        }
    }
}