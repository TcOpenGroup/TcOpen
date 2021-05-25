using NUnit.Framework;
using System;
using System.Text;
using System.Threading;
using TcoDrivesBeckhoffTests;
using TcoDrivesBeckhoff;
using TcoCore;

namespace TcoDrivesBeckhoffUnitTests.PlcExecutedTests
{

    public class T01_TcoDriveSimpleTests
    {

        TcoDriveSimpleTestContext tc = ConnectorFixture.Connector.MAIN._tcoDriveSimpleTestContext;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ConnectorFixture.Connector.MAIN._wpfContextCall.Synchron = false;
            tc._done.Synchron = false;
            tc.ExecuteProbeRun(2, (int)eTcoDriveSimpleTests.Restore);
            tc.ExecuteProbeRun(2, (int)eTcoDriveSimpleTests.CleanUp);
            tc.ExecuteProbeRun(2, (int)eTcoDriveSimpleTests.Reset);
        }

        [SetUp]
        public void Setup()
        {
            tc._done.Synchron = false;
        }

        [TearDown]
        public void TearDown()
        {
            tc._done.Synchron = false;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ConnectorFixture.Connector.MAIN._wpfContextCall.Synchron = false;
            tc._done.Synchron = false;
            tc.ExecuteProbeRun(2, (int)eTcoDriveSimpleTests.Restore);
            tc.ExecuteProbeRun(2, (int)eTcoDriveSimpleTests.CleanUp);
            tc.ExecuteProbeRun(2, (int)eTcoDriveSimpleTests.Reset);
            tc.ExecuteProbeRun(2, (int)eTcoDriveSimpleTests.Stop);
            tc.ExecuteProbeRun(2, (int)eTcoDriveSimpleTests.PowerDisable);
        }

        [Test, Order((int)eTcoDriveSimpleTests.Message)]
        public void T003_Message()
        {
            string message = "Test error message";
            tc._string.Synchron = message;
            tc.ExecuteProbeRun(1,(int)eTcoDriveSimpleTests.Message);
            Assert.AreEqual(message, tc._sut._messenger._mime.Text.Synchron);                                 //Check if message apears in the mime.
            tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.CleanUp);
            Assert.AreEqual(string.Empty, tc._sut._messenger._mime.Text.Synchron);                                
        }

        [Test, Order((int)eTcoDriveSimpleTests.PowerDisable)]
        public void T004_PowerDisable()
        {
            tc.ExecuteProbeRun(5, (int)eTcoDriveSimpleTests.PowerDisable);
            Assert.IsTrue(tc._sut._axisStatus.Disabled.Synchron);
            Assert.IsTrue(tc._sut._axisStatus.HasBeenStopped.Synchron);
            Assert.IsTrue(tc._sut._axisStatus.NotMoving.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Operational.Synchron);
        }

        [Test, Order((int)eTcoDriveSimpleTests.PowerEnable)]
        public void T005_PowerEnable()
        {
            tc.ExecuteProbeRun(15, (int)eTcoDriveSimpleTests.PowerEnable);
            Assert.IsFalse(tc._sut._axisStatus.Disabled.Synchron);
            Assert.IsTrue(tc._sut._axisStatus.HasBeenStopped.Synchron);
            Assert.IsTrue(tc._sut._axisStatus.NotMoving.Synchron);
            Assert.IsTrue(tc._sut._axisStatus.Operational.Synchron);
        }

        [Test, Order((int)eTcoDriveSimpleTests.Reset)]
        public void T006_Reset()
        {
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.Reset, () => tc._sut._resetTask._taskState.Synchron == (short)eTaskState.Done);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveRelativePositiveWithDisabledPositiveDirection)]
        public void T007_MoveRelativePositiveWithDisabledPositiveDirection()
        {
            double actpos = tc._sut._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveRelativePositiveWithDisabledPositiveDirection, () => tc._sut._moveRelativeTask._taskState.Synchron == (short)eTaskState.Error);
            Assert.AreEqual(actpos, tc._sut._axisStatus.ActPos.Synchron, 1);
            Assert.IsTrue(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axisStatus.ErrorId.Synchron);   //"No enable for controller and/or feed (Master axis)"
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveRelativePositiveWithEnabledPositiveDirection)]
        public void T008_MoveRelativePositiveWithEnabledPositiveDirection()
        {
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.Reset, () => tc._sut._resetTask._taskState.Synchron == (short)eTaskState.Done);
            double actpos = tc._sut._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveRelativePositiveWithEnabledPositiveDirection, () => tc._sut._moveRelativeTask._taskState.Synchron == (short)eTaskState.Done);
            Assert.AreEqual(actpos + setpos, tc._sut._axisStatus.ActPos.Synchron, 1);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveRelativeNegativeWithDisabledNegativeDirection)]
        public void T009_MoveRelativeNegativeWithDisabledNegativeDirection()
        {
            double actpos = tc._sut._axisStatus.ActPos.Synchron;
            double setpos = -1000;
            if ((actpos + setpos) < 10 && (actpos + setpos) > -10) setpos = -2000;  //just to ensure that final position won't be close to zero
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveRelativeNegativeWithDisabledNegativeDirection, () => tc._sut._moveRelativeTask._taskState.Synchron == (short)eTaskState.Error);
            Assert.AreEqual(actpos, tc._sut._axisStatus.ActPos.Synchron, 1);
            Assert.IsTrue(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axisStatus.ErrorId.Synchron);   //"No enable for controller and/or feed (Master axis)"
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveRelativeNegativeWithEnabledNegativeDirection)]
        public void T010_MoveRelativeNegativeWithEnabledNegativeDirection()
        {
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.Reset, () => tc._sut._resetTask._taskState.Synchron == (short)eTaskState.Done);
            double actpos = tc._sut._axisStatus.ActPos.Synchron;
            double setpos = -1000;
            if ((actpos + setpos) < 10 && (actpos + setpos) > -10) setpos = -2000;  //just to ensure that final position won't be close to zero
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveRelativeNegativeWithEnabledNegativeDirection, () => tc._sut._moveRelativeTask._taskState.Synchron == (short)eTaskState.Done);
            Assert.AreEqual(actpos + setpos, tc._sut._axisStatus.ActPos.Synchron, 1);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
        }

        [Test, Order((int)eTcoDriveSimpleTests.HommingDirect)]
        public void T011_HommingDirect()
        {
            double actpos = tc._sut._axisStatus.ActPos.Synchron;
            double setpos = 2 * actpos;
            Assert.IsTrue(actpos >= 10 || actpos <= -10, "Actual position too close to zero.");
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 100;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.HommingDirect, () => tc._sut._homeTask._taskState.Synchron == (short)eTaskState.Done);
            Assert.AreEqual(setpos, tc._sut._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveVelocityPositiveWithDisabledPositiveDirection)]
        public void T012_MoveVelocityPositiveWithDisabledPositiveDirection()
        {
            double startpos = tc._sut._axisStatus.ActPos.Synchron;
            double velocity = 1300;
            double accdcc = 50000;
            double jerk = 150000;
            tc._velocity.Synchron = velocity;
            tc._acceleration.Synchron = accdcc;
            tc._deceleration.Synchron = accdcc;
            tc._jerk.Synchron = jerk;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveVelocityPositiveWithDisabledPositiveDirection, () => tc._sut._moveVelocityTask._taskState.Synchron == (short)eTaskState.Error);
            Assert.AreEqual(startpos, tc._sut._axisStatus.ActPos.Synchron, 1);
            Assert.AreEqual(0, tc._sut._axisStatus.ActVelo.Synchron, 1);
            Assert.IsTrue(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axisStatus.ErrorId.Synchron);   //"No enable for controller and/or feed (Master axis)"
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveVelocityPositiveWithEnabledPositiveDirection)]
        public void T013_MoveVelocityPositiveWithEnabledPositiveDirection()
        {
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.Reset, () => tc._sut._resetTask._taskState.Synchron == (short)eTaskState.Done);
            double startpos = tc._sut._axisStatus.ActPos.Synchron;
            double velocity = 1300;
            double accdcc = 50000;
            double jerk = 150000;
            tc._velocity.Synchron = velocity;
            tc._acceleration.Synchron = accdcc;
            tc._deceleration.Synchron = accdcc;
            tc._jerk.Synchron = jerk;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveVelocityPositiveWithEnabledPositiveDirection, () => tc._sut._moveVelocityTask._taskState.Synchron == (short)eTaskState.Done);
            Assert.AreNotEqual(startpos, tc._sut._axisStatus.ActPos.Synchron);
            Assert.AreEqual(velocity, tc._sut._axisStatus.ActVelo.Synchron, 10);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
            tc.ExecuteProbeRun(2, (int)eTcoDriveSimpleTests.Stop);
        }

        [Test, Order((int)eTcoDriveSimpleTests.Stop)]
        public void T014_Stop()
        {
            double decceleration = 50000;
            double jerk= 150000;
            tc._deceleration.Synchron = decceleration;
            tc._jerk.Synchron = jerk;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.Stop, () => tc._sut._stopTask._taskState.Synchron == (short)eTaskState.Done);
            Assert.AreEqual(0, tc._sut._axisStatus.ActVelo.Synchron, 1);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);   
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveVelocityNegativeWithDisabledNegativeDirection)]
        public void T015_MoveVelocityNegativeWithDisabledNegativeDirection()
        {
            double startpos = tc._sut._axisStatus.ActPos.Synchron;
            double velocity = 1300;
            double accdcc = 50000;
            double jerk = 150000;
            tc._velocity.Synchron = velocity;
            tc._acceleration.Synchron = accdcc;
            tc._deceleration.Synchron = accdcc;
            tc._jerk.Synchron = jerk;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveVelocityNegativeWithDisabledNegativeDirection, () => tc._sut._moveVelocityTask._taskState.Synchron == (short)eTaskState.Error);
            Assert.AreEqual(startpos, tc._sut._axisStatus.ActPos.Synchron, 1);
            Assert.AreEqual(0, tc._sut._axisStatus.ActVelo.Synchron, 1);
            Assert.IsTrue(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axisStatus.ErrorId.Synchron);   //"No enable for controller and/or feed (Master axis)"
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveVelocityNegativeWithEnabledNegativeDirection)]
        public void T016_MoveVelocityNegativeWithEnabledNegativeDirection()
        {
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.Reset, () => tc._sut._resetTask._taskState.Synchron == (short)eTaskState.Done);
            double startpos = tc._sut._axisStatus.ActPos.Synchron;
            double velocity = 1300;
            double accdcc = 50000;
            double jerk = 150000;
            tc._velocity.Synchron = velocity;
            tc._acceleration.Synchron = accdcc;
            tc._deceleration.Synchron = accdcc;
            tc._jerk.Synchron = jerk;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveVelocityNegativeWithEnabledNegativeDirection, () => tc._sut._moveVelocityTask._taskState.Synchron == (short)eTaskState.Done);
            Assert.AreNotEqual(startpos, tc._sut._axisStatus.ActPos.Synchron);
            Assert.AreEqual(-velocity, tc._sut._axisStatus.ActVelo.Synchron, 10);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
        }
    }
}

