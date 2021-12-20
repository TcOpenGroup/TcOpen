using NUnit.Framework;
using TcoCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton;
using TcoCore.Logging;
using Vortex.Connector;

namespace TcoCore.PexTests
{
    [TestFixture()]
    public class TcoSequencerTests
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
            var sequencer = new TcoSequencer(new MockRootObject(), string.Empty, string.Empty);
            Assert.IsNotNull(sequencer);           
        }

        [Test()]
        public void StepForwardExecuteTest()
        {
            var sequencer = new TcoSequencer(new MockRootObject(), string.Empty, string.Empty);            
            var task = sequencer._modeController._stepForward;

            task._enabled.Synchron = true;
            task._isServiceable.Synchron = true;

            task.Write();

            task.Execute(null);

            Assert.AreEqual("Task 'Step forward' invoked. {@sender}", _logger.LastMessage.message);            
            Assert.IsInstanceOf<LogInfo>(_logger.LastMessage.payload);
            Assert.IsInstanceOf<PlainStepDetails>((_logger.LastMessage.payload as LogInfo).Details);
            Assert.AreEqual("Information", _logger.LastMessage.serverity);
        }

        [Test()]
        public void StepBackwardExecuteTest()
        {
            var sequencer = new TcoSequencer(new MockRootObject(), string.Empty, string.Empty);
            var task = sequencer._modeController._stepBackward;

            task._enabled.Synchron = true;
            task._isServiceable.Synchron = true;

            task.Write();

            task.Execute(null);

            Assert.AreEqual("Task 'Step backward' invoked. {@sender}", _logger.LastMessage.message);
            Assert.IsInstanceOf<LogInfo>(_logger.LastMessage.payload);
            Assert.IsInstanceOf<PlainStepDetails>((_logger.LastMessage.payload as LogInfo).Details);
            Assert.AreEqual("Information", _logger.LastMessage.serverity);
        }

        [Test()]
        public void StepInExecuteTest()
        {
            var sequencer = new TcoSequencer(new MockRootObject(), string.Empty, string.Empty);
            var task = sequencer._modeController._stepIn;

            task._enabled.Synchron = true;
            task._isServiceable.Synchron = true;

            task.Write();

            task.Execute(null);

            Assert.AreEqual("Task 'Step in' invoked. {@sender}", _logger.LastMessage.message);
            Assert.IsInstanceOf<LogInfo>(_logger.LastMessage.payload);
            Assert.IsInstanceOf<PlainStepDetails>((_logger.LastMessage.payload as LogInfo).Details);
            Assert.AreEqual("Information", _logger.LastMessage.serverity);
        }

        [Test()]
        public void ChangeModeTest()
        {
            var sequencer = new TcoSequencer(new MockRootObject(), string.Empty, string.Empty);
            var task = sequencer._modeController._changeMode;

            task._enabled.Synchron = true;
            task._isServiceable.Synchron = true;

            task.Write();

            task.Execute(null);

            Assert.AreEqual("Task 'Change mode' invoked. {@sender}", _logger.LastMessage.message);
            Assert.IsInstanceOf<LogInfo>(_logger.LastMessage.payload);
            Assert.IsInstanceOf<PlainStepDetails>((_logger.LastMessage.payload as LogInfo).Details);
            Assert.AreEqual("Information", _logger.LastMessage.serverity);
        }
    }
}