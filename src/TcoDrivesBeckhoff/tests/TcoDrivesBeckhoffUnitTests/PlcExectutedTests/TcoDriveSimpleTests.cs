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
            tc.ExecuteProbeRun(2, (int)eTcoDriveSimpleTests.Restore);
            tc.ExecuteProbeRun(2, (int)eTcoDriveSimpleTests.CleanUp);
            tc.ExecuteProbeRun(2, (int)eTcoDriveSimpleTests.Reset);
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;
            tc._plcCycleCounter.Synchron = 0;
        }

        [SetUp]
        public void Setup()
        {
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;
            tc._plcCycleCounter.Synchron = 0;
            tc.ExecuteProbeRun(2, 0);
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
            tc.ExecuteProbeRun(2, (int)eTcoDriveSimpleTests.Restore);
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;
            tc._plcCycleCounter.Synchron = 0;
        }

        [Test, Order((int)eTcoDriveSimpleTests.Message)]
        public void T003_Message()
        {
            string message = "Test error message";
            tc._inString.Synchron = message;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.Message,()=>tc._done.Synchron);
            Assert.AreEqual(message, tc._sut._messenger._mime.Text.Synchron);                                 //Check if message apears in the mime.
            tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.CleanUp);
            Assert.AreEqual(string.Empty, tc._sut._messenger._mime.Text.Synchron);                                
        }

        [Test, Order((int)eTcoDriveSimpleTests.PowerDisable)]
        public void T004_PowerDisable()
        {
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.PowerDisable, () => tc._done.Synchron);
            Assert.IsTrue(tc._sut._axisStatus.Disabled.Synchron);
            Assert.IsTrue(tc._sut._axisStatus.HasBeenStopped.Synchron);
            Assert.IsTrue(tc._sut._axisStatus.NotMoving.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Operational.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
        }

        [Test, Order((int)eTcoDriveSimpleTests.PowerEnable)]
        public void T005_PowerEnable()
        {
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.PowerEnable, () => tc._done.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Disabled.Synchron);
            Assert.IsTrue(tc._sut._axisStatus.HasBeenStopped.Synchron);
            Assert.IsTrue(tc._sut._axisStatus.NotMoving.Synchron);
            Assert.IsTrue(tc._sut._axisStatus.Operational.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
        }

        [Test, Order((int)eTcoDriveSimpleTests.Reset)]
        public void T006_Reset()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.Reset);
            }
            Assert.IsTrue(tc._sut._axisStatus.Error.Synchron);

            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.Reset, () => tc._done.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveRelativePositiveWithDisabledPositiveDirection)]
        public void T007_MoveRelativePositiveWithDisabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.MoveRelativePositiveWithDisabledPositiveDirection);
            }
            double actpos = tc._sut._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveRelativePositiveWithDisabledPositiveDirection, () => tc._done.Synchron);
            Assert.IsTrue(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axisStatus.ErrorId.Synchron);   //"No enable for controller and/or feed (Master axis)"
            while (tc._sut._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveRelativePositiveWithEnabledPositiveDirection)]
        public void T008_MoveRelativePositiveWithEnabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.MoveRelativePositiveWithEnabledPositiveDirection);
            }
            double actpos = tc._sut._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._done.Synchron = false;
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveRelativePositiveWithEnabledPositiveDirection, () => tc._done.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(actpos + setpos, tc._sut._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveRelativeNegativeWithDisabledNegativeDirection)]
        public void T009_MoveRelativeNegativeWithDisabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.MoveRelativeNegativeWithDisabledNegativeDirection);
            }
            double actpos = tc._sut._axisStatus.ActPos.Synchron;
            double setpos = -1000;
            if ((actpos + setpos) < 10 && (actpos + setpos) > -10) setpos = -2000;  //just to ensure that final position won't be close to zero
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveRelativeNegativeWithDisabledNegativeDirection, () => tc._done.Synchron);
            Assert.AreEqual(actpos, tc._sut._axisStatus.ActPos.Synchron, 1);
            Assert.IsTrue(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axisStatus.ErrorId.Synchron);   //"No enable for controller and/or feed (Master axis)"
            while (tc._sut._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveRelativeNegativeWithEnabledNegativeDirection)]
        public void T010_MoveRelativeNegativeWithEnabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.MoveRelativeNegativeWithEnabledNegativeDirection);
            }
            double actpos = tc._sut._axisStatus.ActPos.Synchron;
            double setpos = -1000;
            if ((actpos + setpos) < 10 && (actpos + setpos) > -10) setpos = -2000;  //just to ensure that final position won't be close to zero
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveRelativeNegativeWithEnabledNegativeDirection, () => tc._done.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(actpos + setpos, tc._sut._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoDriveSimpleTests.HommingDirect)]
        public void T011_HommingDirect()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.HommingDirect);
            }
            double actpos = tc._sut._axisStatus.ActPos.Synchron;
            double setpos = 2 * actpos;
            Assert.IsTrue(actpos >= 10 || actpos <= -10, "Actual position too close to zero.");
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 100;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.HommingDirect, () => tc._done.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(setpos, tc._sut._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveVelocityPositiveWithDisabledPositiveDirection)]
        public void T012_MoveVelocityPositiveWithDisabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.MoveVelocityPositiveWithDisabledPositiveDirection);
            }
            double startpos = tc._sut._axisStatus.ActPos.Synchron;
            double velocity = 1300;
            double accdcc = 50000;
            double jerk = 150000;
            tc._velocity.Synchron = velocity;
            tc._acceleration.Synchron = accdcc;
            tc._deceleration.Synchron = accdcc;
            tc._jerk.Synchron = jerk;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveVelocityPositiveWithDisabledPositiveDirection, () => tc._done.Synchron);
            Assert.AreEqual(startpos, tc._sut._axisStatus.ActPos.Synchron, 1);
            Assert.AreEqual(0, tc._sut._axisStatus.ActVelo.Synchron, 1);
            Assert.IsTrue(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axisStatus.ErrorId.Synchron);   //"No enable for controller and/or feed (Master axis)"
            while (tc._sut._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveVelocityPositiveWithEnabledPositiveDirection)]
        public void T013_MoveVelocityPositiveWithEnabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.MoveVelocityPositiveWithEnabledPositiveDirection);
            }
            double startpos = tc._sut._axisStatus.ActPos.Synchron;
            double velocity = 1300;
            double accdcc = 50000;
            double jerk = 150000;
            tc._velocity.Synchron = velocity;
            tc._acceleration.Synchron = accdcc;
            tc._deceleration.Synchron = accdcc;
            tc._jerk.Synchron = jerk;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveVelocityPositiveWithEnabledPositiveDirection, () => tc._done.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
            Assert.AreNotEqual(startpos, tc._sut._axisStatus.ActPos.Synchron);
            Assert.AreEqual(velocity, tc._sut._axisStatus.ActVelo.Synchron, velocity*0.02);
            //tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.Stop);
        }

        [Test, Order((int)eTcoDriveSimpleTests.Stop)]
        public void T014_Stop()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.Stop);
            }
            double velocity = 1300;
            double decceleration = 50000;
            double jerk= 150000;
            tc._deceleration.Synchron = decceleration;
            tc._jerk.Synchron = jerk;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.Stop, () => tc._done.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ActVelo.Synchron, velocity * 0.02);
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveVelocityNegativeWithDisabledNegativeDirection)]
        public void T015_MoveVelocityNegativeWithDisabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.MoveVelocityNegativeWithDisabledNegativeDirection);
            }
            double startpos = tc._sut._axisStatus.ActPos.Synchron;
            double velocity = 1300;
            double accdcc = 50000;
            double jerk = 150000;
            tc._velocity.Synchron = velocity;
            tc._acceleration.Synchron = accdcc;
            tc._deceleration.Synchron = accdcc;
            tc._jerk.Synchron = jerk;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveVelocityNegativeWithDisabledNegativeDirection, () => tc._done.Synchron);
            Assert.AreEqual(startpos, tc._sut._axisStatus.ActPos.Synchron, 1);
            Assert.AreEqual(0, tc._sut._axisStatus.ActVelo.Synchron, 1);
            Assert.IsTrue(tc._sut._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axisStatus.ErrorId.Synchron);   //"No enable for controller and/or feed (Master axis)"
            while (tc._sut._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoDriveSimpleTests.MoveVelocityNegativeWithEnabledNegativeDirection)]
        public void T016_MoveVelocityNegativeWithEnabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoDriveSimpleTests.MoveVelocityNegativeWithEnabledNegativeDirection);
            }
            double startpos = tc._sut._axisStatus.ActPos.Synchron;
            double velocity = 1300;
            double accdcc = 50000;
            double jerk = 150000;
            tc._velocity.Synchron = velocity;
            tc._acceleration.Synchron = accdcc;
            tc._deceleration.Synchron = accdcc;
            tc._jerk.Synchron = jerk;
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.MoveVelocityNegativeWithEnabledNegativeDirection, () => tc._done.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
            Assert.AreNotEqual(startpos, tc._sut._axisStatus.ActPos.Synchron);
            Assert.AreEqual(-velocity, tc._sut._axisStatus.ActVelo.Synchron, velocity * 0.02);
        }

        [Test, Order((int)eTcoDriveSimpleTests.GetMotorType)]
        public void T017_GetMotorType()
        {
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.GetMotorType, () => tc._done.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoDriveSimpleTests.GetAxisReferenceType)]
        public void T018_GetAxisReferenceType()
        {
            tc.ExecuteProbeRun((int)eTcoDriveSimpleTests.GetAxisReferenceType, () => tc._done.Synchron);
            Assert.AreEqual(0, tc._sut._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axisStatus.Error.Synchron);
        }





    }
}

