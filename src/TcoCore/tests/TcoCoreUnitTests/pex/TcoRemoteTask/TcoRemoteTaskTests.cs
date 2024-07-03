using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TcoCore;
using Vortex.Connector;

namespace TcoCore.PexTests
{
    [TestFixture()]
    public class TcoRemoteTaskTests
    {
        [Test()]
        public void InitializeTest()
        {
            var remoteTask = new TcoRemoteTask(new MockRootObject(), string.Empty, string.Empty);
            var actual = string.Empty;
            remoteTask.Initialize(() => actual = "I've been executed");
            Assert.IsTrue(remoteTask._isInitialized.Synchron);
            Assert.IsTrue(remoteTask._startSignature.CyclicReading);
            Assert.IsInstanceOf<Action>(remoteTask.DeferredAction);
        }

        [Test()]
        public void InitializeTest1()
        {
            var remoteTask = new TcoRemoteTask(new MockRootObject(), string.Empty, string.Empty);
            var actual = string.Empty;
            remoteTask.Initialize(() =>
            {
                actual = "I've been executed";
                return true;
            });
            Assert.IsTrue(remoteTask._isInitialized.Synchron);
            Assert.IsTrue(remoteTask._startSignature.CyclicReading);
            Assert.IsInstanceOf<Action>(remoteTask.DeferredAction);
        }

        [Test()]
        public void InitializeExclusivelyTest()
        {
            var remoteTask = new TcoRemoteTask(new MockRootObject(), string.Empty, string.Empty);
            var actual = string.Empty;
            remoteTask.InitializeExclusively(() =>
            {
                actual = "I've been executed";
                return true;
            });
            Assert.IsTrue(remoteTask._isInitialized.Synchron);
            Assert.IsTrue(remoteTask._startSignature.CyclicReading);
            Assert.IsInstanceOf<Action>(remoteTask.DeferredAction);
        }

        [Test()]
        public void InitializeExclusivelyTest1()
        {
            var remoteTask = new TcoRemoteTask(new MockRootObject(), string.Empty, string.Empty);
            var actual = string.Empty;
            remoteTask.InitializeExclusively(() => actual = "I've been executed");
            Assert.IsTrue(remoteTask._isInitialized.Synchron);
            Assert.IsTrue(remoteTask._startSignature.CyclicReading);
            Assert.IsInstanceOf<Action>(remoteTask.DeferredAction);
        }

        [Test()]
        public void SameApplicationTriesInitializeExclusivelyTest()
        {
            var remoteTask = new TcoRemoteTask(new MockRootObject(), string.Empty, string.Empty);
            var actual = string.Empty;
            remoteTask.InitializeExclusively(() => actual = "I've been executed");
            Assert.IsTrue(remoteTask._isInitialized.Synchron);
            Assert.IsTrue(remoteTask._startSignature.CyclicReading);
            Assert.IsInstanceOf<Action>(remoteTask.DeferredAction);

            Assert.Throws<MultipleRemoteCallInitializationException>(
                () => remoteTask.InitializeExclusively(() => actual = "I've been executed")
            );
        }

        [Test()]
        public void DeInitializeTest()
        {
            var remoteTask = new TcoRemoteTask(new MockRootObject(), string.Empty, string.Empty);
            var actual = string.Empty;
            remoteTask.InitializeExclusively(() => actual = "I've been executed");
            Assert.IsTrue(remoteTask._isInitialized.Synchron);
            Assert.IsTrue(remoteTask._startSignature.CyclicReading);
            Assert.IsInstanceOf<Action>(remoteTask.DeferredAction);

            remoteTask.DeInitialize();

            Assert.IsFalse(remoteTask._isInitialized.Synchron);
            Assert.IsTrue(remoteTask._startSignature.CyclicReading);
            Assert.IsInstanceOf<Action>(remoteTask.DeferredAction);
        }

        [Test()]
        public void ResetExecutionTest()
        {
            var remoteTask = new TcoRemoteTask(new MockRootObject(), string.Empty, string.Empty);
            var actual = string.Empty;
            remoteTask.InitializeExclusively(() => actual = "I've been executed");
            Assert.IsTrue(remoteTask._isInitialized.Synchron);
            Assert.IsTrue(remoteTask._startSignature.CyclicReading);
            Assert.IsInstanceOf<Action>(remoteTask.DeferredAction);
            remoteTask._startSignature.Synchron = "13245654468798746513";
            remoteTask._doneSignarure.Synchron = "876465146431313";
            remoteTask._hasException.Synchron = true;

            remoteTask.ResetExecution();

            Assert.AreEqual(string.Empty, remoteTask._startSignature.Synchron);
            Assert.AreEqual(string.Empty, remoteTask._doneSignarure.Synchron);
            Assert.AreEqual(false, remoteTask._hasException.Synchron);
        }
    }
}
