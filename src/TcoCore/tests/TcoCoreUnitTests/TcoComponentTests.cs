using NUnit.Framework;
using System;
using System.Threading;
using TcoCore;
using TcoCoreTests;
using TcoCoreUnitTests;
using System.Linq;
using Vortex.Connector;


namespace TcoCoreUnitTests
{

    public class T09_TcoComponentTests
    {

        TcoComponentTest tc = ConnectorFixture.Connector.MAIN._tcoComponentTest;
        TcoTaskTest tt = ConnectorFixture.Connector.MAIN._tcoComponentTest._component._task;
        ushort cycles;



        [OneTimeSetUp]
        public void OneSetup()
        {
            tc.SingleCycleRun(() => tc.TriggerRestore());
        }


        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }

        [TearDown]
        public void TearDown()
        {
            tc.SingleCycleRun(() => tc.TriggerRestore());
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(900)]
        public void T900_NotInServiceMode()
        {
            //--Arrange
            cycles = 10;

            ulong startcycle = tc._startCycleCount.Synchron;
            ulong endcycle = tc._endCycleCount.Synchron;
            ulong serviceModeCount = tc._component._serviceModeCount.Synchron;

            //--Act
            tc.MultipleCycleRun(() =>
            {
                tc.PLCinstanceRun();
            }, cycles);
            tt.ReadOutState();

            //--Assert
            Assert.AreEqual(startcycle + cycles, tc._startCycleCount.Synchron);
            Assert.AreEqual(endcycle + cycles, tc._endCycleCount.Synchron);
            Assert.AreEqual(serviceModeCount, tc._component._serviceModeCount.Synchron);
            Assert.IsTrue(tt._isReady.Synchron);
            Assert.IsFalse(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsFalse(tc.IsService());
        }

        [Test, Order(901)]
        public void T901_InvokeTryFromVisuWhileNotInServiceMode()
        {
            //--Arrange
            ulong startcycle = tc._startCycleCount.Synchron;
            ulong endcycle = tc._endCycleCount.Synchron;
            ulong serviceModeCount = tc._component._serviceModeCount.Synchron;

            //--Act
            tc.MultipleCycleRun(() =>
            {
                tt.SetInvokeRequest();
                tt.ReadOutState();
                Assert.IsTrue(tt._isInvokeRequestTrue.Synchron);
                tc.PLCinstanceRun();
                tt.ReadOutState();
                Assert.IsFalse(tt._isInvokeRequestTrue.Synchron);
            }, cycles);
            tt.ReadOutState();

            //--Assert
            Assert.AreEqual(startcycle + cycles, tc._startCycleCount.Synchron);
            Assert.AreEqual(endcycle + cycles, tc._endCycleCount.Synchron);
            Assert.AreEqual(serviceModeCount, tc._component._serviceModeCount.Synchron);
            Assert.IsTrue(tt._isReady.Synchron);
            Assert.IsFalse(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsFalse(tc.IsService());
        }

        [Test, Order(902)]
        public void T902_InvokeTryFromPlcWhileNotInServiceMode()
        {
            //--Arrange
            ulong startcycle = tc._startCycleCount.Synchron;
            ulong endcycle = tc._endCycleCount.Synchron;
            ulong serviceModeCount = tc._component._serviceModeCount.Synchron;

            //--Act
            tc.MultipleCycleRun(() =>
            {
                tt.TriggerInvoke();
                tc.PLCinstanceRun();
            }, cycles);
            tt.ReadOutState();

            //--Assert
            Assert.AreEqual(startcycle + cycles, tc._startCycleCount.Synchron);
            Assert.AreEqual(endcycle + cycles, tc._endCycleCount.Synchron);
            Assert.AreEqual(serviceModeCount, tc._component._serviceModeCount.Synchron);
            Assert.IsFalse(tt._isReady.Synchron);
            Assert.IsTrue(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsFalse(tc.IsService());
        }

        [Test, Order(903)]
        public void T903_InvokeFromVisuInServiceModeCalledAfterServiceMethod()
        {
            //--Arrange
            ulong startcycle = tc._startCycleCount.Synchron;
            ulong endcycle = tc._endCycleCount.Synchron;
            ulong serviceModeCount = tc._component._serviceModeCount.Synchron;

            //--Act
            tc.MultipleCycleRun(() =>
            {
                tc.Service();
                tt.SetInvokeRequest();
                tt.ReadOutState();
                Assert.IsTrue(tt._isInvokeRequestTrue.Synchron);
                tc.PLCinstanceRun();
                tt.ReadOutState();
                Assert.IsFalse(tt._isInvokeRequestTrue.Synchron);
            }, cycles);
            tt.ReadOutState();

            //--Assert
            Assert.AreEqual(startcycle + cycles, tc._startCycleCount.Synchron);
            Assert.AreEqual(endcycle + cycles, tc._endCycleCount.Synchron);
            Assert.AreEqual(serviceModeCount + cycles, tc._component._serviceModeCount.Synchron);
            Assert.IsFalse(tt._isReady.Synchron);
            Assert.IsTrue(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsTrue(tc.IsService());

        }

        [Test, Order(904)]
        public void T904_InvokeFromVisuInServiceModeCalledBeforeServiceMethod()
        {
            //--Arrange
            ulong startcycle = tc._startCycleCount.Synchron;
            ulong endcycle = tc._endCycleCount.Synchron;
            ulong serviceModeCount = tc._component._serviceModeCount.Synchron;

            //--Act
            tc.MultipleCycleRun(() =>
            {
                tt.SetInvokeRequest();
                tc.Service();
                tt.ReadOutState();
                Assert.IsTrue(tt._isInvokeRequestTrue.Synchron);
                tc.PLCinstanceRun();
                tt.ReadOutState();
                Assert.IsFalse(tt._isInvokeRequestTrue.Synchron);
            }, cycles);
            tt.ReadOutState();

            //--Assert
            Assert.AreEqual(startcycle + cycles, tc._startCycleCount.Synchron);
            Assert.AreEqual(endcycle + cycles, tc._endCycleCount.Synchron);
            Assert.AreEqual(serviceModeCount + cycles, tc._component._serviceModeCount.Synchron);
            Assert.IsFalse(tt._isReady.Synchron);
            Assert.IsTrue(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsTrue(tc.IsService());

        }

        [Test, Order(905)]
        public void T905_InvokeFromPlcInServiceModeCalledAfterServiceMethod()
        {
            //--Arrange
            ulong startcycle = tc._startCycleCount.Synchron;
            ulong endcycle = tc._endCycleCount.Synchron;
            ulong serviceModeCount = tc._component._serviceModeCount.Synchron;

            //--Act
            tc.MultipleCycleRun(() =>
            {
                tc.Service();
                tt.TriggerInvoke();
                tc.PLCinstanceRun();
            }, cycles);
            tt.ReadOutState();

            //--Assert
            Assert.AreEqual(startcycle + cycles, tc._startCycleCount.Synchron);
            Assert.AreEqual(endcycle + cycles, tc._endCycleCount.Synchron);
            Assert.AreEqual(serviceModeCount + cycles, tc._component._serviceModeCount.Synchron);
            Assert.IsFalse(tt._isReady.Synchron);
            Assert.IsTrue(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsTrue(tc.IsService());

        }

        [Test, Order(906)]
        public void T906_InvokeFromPlcInServiceModeCalledBeforeServiceMethod()
        {
            //--Arrange
            ulong startcycle = tc._startCycleCount.Synchron;
            ulong endcycle = tc._endCycleCount.Synchron;
            ulong serviceModeCount = tc._component._serviceModeCount.Synchron;

            //--Act
            tc.MultipleCycleRun(() =>
            {
                tt.TriggerInvoke();
                tc.Service();
                tc.PLCinstanceRun();
            }, cycles);
            tt.ReadOutState();

            //--Assert
            Assert.AreEqual(startcycle + cycles, tc._startCycleCount.Synchron);
            Assert.AreEqual(endcycle + cycles, tc._endCycleCount.Synchron);
            Assert.AreEqual(serviceModeCount + cycles, tc._component._serviceModeCount.Synchron);
            Assert.IsFalse(tt._isReady.Synchron);
            Assert.IsTrue(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsTrue(tc.IsService());
        }

        [Test, Order(907)]
        public void T907_InvokeFromVisuInServiceModeSwitchOffServiceModeCheckIfTaskNotRestored()
        {
            //--Arrange
            ulong startcycle = tc._startCycleCount.Synchron;
            ulong endcycle = tc._endCycleCount.Synchron;
            ulong serviceModeCount = tc._component._serviceModeCount.Synchron;
            ushort cc = 0;
            //--Act
            tc.MultipleCycleRun(() =>
            {
                if (cc <= cycles / 2)
                {
                    tc.Service();
                }
                cc++;
                tt.SetInvokeRequest();
                tt.ReadOutState();
                Assert.IsTrue(tt._isInvokeRequestTrue.Synchron);
                tc.PLCinstanceRun();
                tt.ReadOutState();
                Assert.IsFalse(tt._isInvokeRequestTrue.Synchron);
            }, cycles);
            tt.ReadOutState();

            //--Assert
            Assert.AreEqual(startcycle + cycles, tc._startCycleCount.Synchron);
            Assert.AreEqual(endcycle + cycles, tc._endCycleCount.Synchron);
            Assert.AreEqual(serviceModeCount + (ushort)(cycles / 2 +1), tc._component._serviceModeCount.Synchron);
            Assert.IsFalse(tt._isReady.Synchron);
            Assert.IsTrue(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsFalse(tc.IsService());
        }

        [Test, Order(908)]
        public void T908_InvokeFromPlcInServiceModeSwitchOffServiceModeCheckIfTaskNotRestored()
        {
            //--Arrange
            ulong startcycle = tc._startCycleCount.Synchron;
            ulong endcycle = tc._endCycleCount.Synchron;
            ulong serviceModeCount = tc._component._serviceModeCount.Synchron;
            ushort cc = 0;
            //--Act
            tc.MultipleCycleRun(() =>
            {
                if (cc <= cycles / 2)
                {
                    tc.Service();
                }
                cc++;
                tt.TriggerInvoke();
                tc.PLCinstanceRun();
            }, cycles);
            tt.ReadOutState();

            //--Assert
            Assert.AreEqual(startcycle + cycles, tc._startCycleCount.Synchron);
            Assert.AreEqual(endcycle + cycles, tc._endCycleCount.Synchron);
            Assert.AreEqual(serviceModeCount + (ushort)(cycles / 2 + 1), tc._component._serviceModeCount.Synchron);
            Assert.IsFalse(tt._isReady.Synchron);
            Assert.IsTrue(tt._isBusy.Synchron);
            Assert.IsFalse(tt._isDone.Synchron);
            Assert.IsFalse(tt._isError.Synchron);
            Assert.IsFalse(tc.IsService());
        }


    }
}