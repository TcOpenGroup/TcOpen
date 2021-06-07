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
    public class TcoTaskTests
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
            var task = new TcoTask(new MockRootObject(), string.Empty, string.Empty);
            Assert.IsNotNull(task);           
        }

        [Test()]
        public void LogPayloadPropertyTest()
        {
            var task = new TcoTask();
            task.LogPayloadDecoration = () => "hey I am payload".ToUpper();

            var actual = task.LogPayloadDecoration();

            Assert.AreEqual("hey I am payload".ToUpper(), actual);
        }

        [Test()]
        [TestCase(true)]
        [TestCase(false)]
        public void ValidateCanExecuteAbortRestoreTest_nested_commands(bool serviceable)
        {            
            var task = new TcoTask();           
            task._isServiceable.Synchron = serviceable;
            task._restoreRequest.Synchron = false;
            task._abortRequest.Synchron = false;
            _logger.ClearLastMessage();

            task.ValidateCanExecuteAbortRestore(null, new Vortex.Connector.ValueTypes.ValueChangedEventArgs(0));

            Assert.AreEqual(serviceable, task.Abort.CanExecute(null));
            Assert.AreEqual(serviceable, task.Restore.CanExecute(null));

            task.Abort.Execute(null);

            if(serviceable)
            {
                Assert.AreEqual("Task '' aborted. {@sender}", _logger.LastMessage.message);
                Assert.IsInstanceOf<LogInfo>(_logger.LastMessage.payload);
                Assert.AreEqual("Information", _logger.LastMessage.serverity);
            }
            else
            {
                Assert.IsTrue(_logger.IsLastMessageEmpty());
            }

            task.Restore.Execute(null);

            if (serviceable)
            {
                Assert.AreEqual("Task '' restored. {@sender}", _logger.LastMessage.message);
                Assert.IsInstanceOf<LogInfo>(_logger.LastMessage.payload);
                Assert.AreEqual("Information", _logger.LastMessage.serverity);
            }
            else
            {
                Assert.IsTrue(_logger.IsLastMessageEmpty());
            }

            Assert.AreEqual(serviceable, task._abortRequest.Synchron);
            Assert.AreEqual(serviceable, task._restoreRequest.Synchron);

        }

        [Test()]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(true, true)]
        public void CanExecuteTest_enabled_serviceable(bool isEnabled, bool isServiceable)
        {
            var task = new TcoTask();
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
            var task = new TcoTask();
            task._enabled.Synchron = isEnabled;
            task._isServiceable.Synchron = isServiceable;
            task._invokeRequest.Synchron = false;
            _logger.ClearLastMessage();
            var expected = isEnabled && isServiceable;

            task.Execute(new object());

            Assert.AreEqual(expected, task._invokeRequest.Synchron);
            Assert.AreEqual(expected, !_logger.IsLastMessageEmpty());

            if(expected)
            { 
                Assert.AreEqual("Task '' executed. {@sender}", _logger.LastMessage.message);
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
        public void ExecuteTestWithRestore(bool isEnabled, bool isServiceable)
        {
            var task = new TcoTask();
            task._enabled.Synchron = isEnabled;
            task._isServiceable.Synchron = isServiceable;
            task._restoreRequest.Synchron = false;
            task._invokeRequest.Synchron = false;
            task._taskState.Synchron = (short)(eTaskState.Done);

            _logger.ClearLastMessage();
            var expected = isEnabled && isServiceable;

            task.Execute(new object());


            Assert.AreEqual(expected, task._restoreRequest.Synchron);
            Assert.AreEqual(expected, task._invokeRequest.Synchron);
            Assert.AreEqual(expected, !_logger.IsLastMessageEmpty());

            if (expected)
            {
                Assert.AreEqual("Task '' executed. {@sender}", _logger.LastMessage.message);
                Assert.IsInstanceOf<LogInfo>(_logger.LastMessage.payload);
                Assert.AreEqual("Information", _logger.LastMessage.serverity);
            }
            else
            {
                Assert.IsTrue(_logger.IsLastMessageEmpty());
            }
        }
    }
}