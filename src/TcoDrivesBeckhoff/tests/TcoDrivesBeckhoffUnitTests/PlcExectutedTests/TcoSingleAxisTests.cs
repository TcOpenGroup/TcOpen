// #define WITH_HARDWARE
using System;
using System.Text;
using System.Threading;
using NUnit.Framework;
using TcoCore;
using TcoDrivesBeckhoff;
using TcoDrivesBeckhoffTests;

namespace TcoDrivesBeckhoffUnitTests.PlcExecutedTests
{
    public class TcoSingleAxisTests
    {
        TcoSingleAxisContext tc = ConnectorFixture.Connector.MAIN._tcoSingleAxisTestContext;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ConnectorFixture.Connector.MAIN._wpfContextCall.Synchron = false;
            ConnectorFixture.Connector.MAIN._multiAxisTestActive.Synchron = false;
            ConnectorFixture.Connector.MAIN._singleAxisTestActive.Synchron = true;
            tc.ExecuteProbeRun(2, (int)eTcoSingleAxisTests.Restore);
            tc.ExecuteProbeRun(2, (int)eTcoSingleAxisTests.CleanUp);
            tc.ExecuteProbeRun(2, (int)eTcoSingleAxisTests.Reset);
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
            ConnectorFixture.Connector.MAIN._multiAxisTestActive.Synchron = false;
            ConnectorFixture.Connector.MAIN._singleAxisTestActive.Synchron = true;
            tc._done.Synchron = false;
            tc.ExecuteProbeRun(2, (int)eTcoSingleAxisTests.Restore);
            tc.ExecuteProbeRun(2, (int)eTcoSingleAxisTests.CleanUp);
            tc.ExecuteProbeRun(2, (int)eTcoSingleAxisTests.Reset);
            tc.ExecuteProbeRun(2, (int)eTcoSingleAxisTests.Stop);
            tc.ExecuteProbeRun(2, (int)eTcoSingleAxisTests.PowerDisable);
            tc.ExecuteProbeRun(2, (int)eTcoSingleAxisTests.Restore);
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;
            tc._plcCycleCounter.Synchron = 0;
        }

        [Test, Order((int)eTcoSingleAxisTests.Message)]
        public void T003_Message()
        {
            string message = "Test error message";
            tc._inString.Synchron = message;
            tc.ExecuteProbeRun((int)eTcoSingleAxisTests.Message, () => tc._done.Synchron);
            Assert.AreEqual(message, tc._sut._messenger._mime.Text.Synchron); //Check if message apears in the mime.
            tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.CleanUp);
            Assert.AreEqual(string.Empty, tc._sut._messenger._mime.Text.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.PowerDisable)]
        public void T004_PowerDisable()
        {
            tc.ExecuteProbeRun((int)eTcoSingleAxisTests.PowerDisable, () => tc._done.Synchron);
            Assert.IsTrue(tc._sut._axis._axisStatus.Disabled.Synchron);
            Assert.IsTrue(tc._sut._axis._axisStatus.HasBeenStopped.Synchron);
            Assert.IsTrue(tc._sut._axis._axisStatus.NotMoving.Synchron);
#if WITH_HARDWARE
            Assert.IsFalse(tc._sut._axis._axisStatus.Operational.Synchron);
#endif
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.PowerEnable)]
        public void T005_PowerEnable()
        {
            tc.ExecuteProbeRun((int)eTcoSingleAxisTests.PowerEnable, () => tc._done.Synchron);
#if WITH_HARDWARE
            Assert.IsFalse(tc._sut._axis._axisStatus.Disabled.Synchron);
#endif
            Assert.IsTrue(tc._sut._axis._axisStatus.HasBeenStopped.Synchron);
            Assert.IsTrue(tc._sut._axis._axisStatus.NotMoving.Synchron);
            Assert.IsTrue(tc._sut._axis._axisStatus.Operational.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.Reset)]
        public void T006_Reset()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.Reset);
            }
            Assert.IsTrue(tc._sut._axis._axisStatus.Error.Synchron);

            tc.ExecuteProbeRun((int)eTcoSingleAxisTests.Reset, () => tc._done.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveRelativePositiveWithDisabledPositiveDirection)]
        public void T007_MoveRelativePositiveWithDisabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveRelativePositiveWithDisabledPositiveDirection
                );
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveRelativePositiveWithDisabledPositiveDirection,
                () => tc._done.Synchron
            );
            Assert.IsTrue(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            while (tc._sut._axis._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveRelativePositiveWithEnabledPositiveDirection)]
        public void T008_MoveRelativePositiveWithEnabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveRelativePositiveWithEnabledPositiveDirection
                );
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._done.Synchron = false;
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveRelativePositiveWithEnabledPositiveDirection,
                () => tc._done.Synchron
            );
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(actpos + setpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveRelativeNegativeWithDisabledNegativeDirection)]
        public void T009_MoveRelativeNegativeWithDisabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveRelativeNegativeWithDisabledNegativeDirection
                );
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = -1000;
            if ((actpos + setpos) < 10 && (actpos + setpos) > -10)
                setpos = -2000; //just to ensure that final position won't be close to zero
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveRelativeNegativeWithDisabledNegativeDirection,
                () => tc._done.Synchron
            );
            Assert.AreEqual(actpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
            Assert.IsTrue(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            while (tc._sut._axis._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveRelativeNegativeWithEnabledNegativeDirection)]
        public void T010_MoveRelativeNegativeWithEnabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveRelativeNegativeWithEnabledNegativeDirection
                );
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = -1000;
            if ((actpos + setpos) < 10 && (actpos + setpos) > -10)
                setpos = -2000; //just to ensure that final position won't be close to zero
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveRelativeNegativeWithEnabledNegativeDirection,
                () => tc._done.Synchron
            );
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(actpos + setpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoSingleAxisTests.HommingDirect)]
        public void T011_HommingDirect()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.HommingDirect);
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 2 * actpos;
            Assert.IsTrue(actpos >= 10 || actpos <= -10, "Actual position too close to zero.");
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 100;
            tc.ExecuteProbeRun((int)eTcoSingleAxisTests.HommingDirect, () => tc._done.Synchron);
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(setpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveVelocityPositiveWithDisabledPositiveDirection)]
        public void T012_MoveVelocityPositiveWithDisabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveVelocityPositiveWithDisabledPositiveDirection
                );
            }
            double startpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double velocity = 1300;
            double accdcc = 50000;
            double jerk = 150000;
            tc._velocity.Synchron = velocity;
            tc._acceleration.Synchron = accdcc;
            tc._deceleration.Synchron = accdcc;
            tc._jerk.Synchron = jerk;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveVelocityPositiveWithDisabledPositiveDirection,
                () => tc._done.Synchron
            );
            Assert.AreEqual(startpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ActVelo.Synchron, 1);
            Assert.IsTrue(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            while (tc._sut._axis._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveVelocityPositiveWithEnabledPositiveDirection)]
        public void T013_MoveVelocityPositiveWithEnabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveVelocityPositiveWithEnabledPositiveDirection
                );
            }
            double startpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double velocity = 1300;
            double accdcc = 50000;
            double jerk = 150000;
            tc._velocity.Synchron = velocity;
            tc._acceleration.Synchron = accdcc;
            tc._deceleration.Synchron = accdcc;
            tc._jerk.Synchron = jerk;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveVelocityPositiveWithEnabledPositiveDirection,
                () => tc._done.Synchron
            );
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreNotEqual(startpos, tc._sut._axis._axisStatus.ActPos.Synchron);
            Assert.AreEqual(velocity, tc._sut._axis._axisStatus.ActVelo.Synchron, velocity * 0.02);
            //tc.ExecuteProbeRun((int)eTcoSingleAxisTests.Stop);
        }

        [Test, Order((int)eTcoSingleAxisTests.Stop)]
        public void T014_Stop()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.Stop);
            }
            double velocity = 1300;
            double decceleration = 50000;
            double jerk = 150000;
            tc._deceleration.Synchron = decceleration;
            tc._jerk.Synchron = jerk;
            tc.ExecuteProbeRun((int)eTcoSingleAxisTests.Stop, () => tc._done.Synchron);
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ActVelo.Synchron, velocity * 0.02);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveVelocityNegativeWithDisabledNegativeDirection)]
        public void T015_MoveVelocityNegativeWithDisabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveVelocityNegativeWithDisabledNegativeDirection
                );
            }
            double startpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double velocity = 1300;
            double accdcc = 50000;
            double jerk = 150000;
            tc._velocity.Synchron = velocity;
            tc._acceleration.Synchron = accdcc;
            tc._deceleration.Synchron = accdcc;
            tc._jerk.Synchron = jerk;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveVelocityNegativeWithDisabledNegativeDirection,
                () => tc._done.Synchron
            );
            Assert.AreEqual(startpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ActVelo.Synchron, 1);
            Assert.IsTrue(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            while (tc._sut._axis._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveVelocityNegativeWithEnabledNegativeDirection)]
        public void T016_MoveVelocityNegativeWithEnabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveVelocityNegativeWithEnabledNegativeDirection
                );
            }
            double startpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double velocity = 1300;
            double accdcc = 50000;
            double jerk = 150000;
            tc._velocity.Synchron = velocity;
            tc._acceleration.Synchron = accdcc;
            tc._deceleration.Synchron = accdcc;
            tc._jerk.Synchron = jerk;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveVelocityNegativeWithEnabledNegativeDirection,
                () => tc._done.Synchron
            );
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreNotEqual(startpos, tc._sut._axis._axisStatus.ActPos.Synchron);
            Assert.AreEqual(-velocity, tc._sut._axis._axisStatus.ActVelo.Synchron, velocity * 0.02);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveAbsoluteDisabled)]
        public void T017_MoveAbsoluteDisabled()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.MoveAbsoluteDisabled);
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveAbsoluteDisabled,
                () => tc._done.Synchron
            );
#if WITH_HARDWARE
            Assert.AreEqual(16992, tc._sut._axis._axisStatus.ErrorId.Synchron); //"Controller enable"

            Assert.IsTrue(tc._sut._axis._axisStatus.Error.Synchron);
#endif
            while (tc._sut._axis._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveAbsoluteDisabledPositiveDirection)]
        public void T018_MoveAbsoluteDisabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveAbsoluteDisabledPositiveDirection
                );
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = actpos + setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveAbsoluteDisabledPositiveDirection,
                () => tc._done.Synchron
            );
            Assert.IsTrue(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
            while (tc._sut._axis._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveAbsoluteEnabledPositiveDirection)]
        public void T019_MoveAbsoluteEnabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveAbsoluteEnabledPositiveDirection
                );
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = actpos + setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveAbsoluteEnabledPositiveDirection,
                () => tc._done.Synchron
            );
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.AreEqual(actpos + setpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveAbsoluteDisabledNegativeDirection)]
        public void T020_MoveAbsoluteDisabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveAbsoluteDisabledNegativeDirection
                );
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = actpos - setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveAbsoluteDisabledNegativeDirection,
                () => tc._done.Synchron
            );
            Assert.IsTrue(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
            while (tc._sut._axis._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveAbsoluteEnabledNegativeDirection)]
        public void T021_MoveAbsoluteEnabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveAbsoluteEnabledNegativeDirection
                );
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = actpos - setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveAbsoluteEnabledNegativeDirection,
                () => tc._done.Synchron
            );
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos - setpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveAbsoluteAxisDisabled)]
        public void T022_MoveAbsoluteDisabled()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.MoveAbsoluteAxisDisabled);
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveAbsoluteAxisDisabled,
                () => tc._done.Synchron
            );
#if WITH_HARDWARE
            Assert.AreEqual(16992, tc._sut._axis._axisStatus.ErrorId.Synchron); //"Controller enable"

            Assert.IsTrue(tc._sut._axis._axisStatus.Error.Synchron);
#endif
            while (tc._sut._axis._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveAbsoluteAxisDisabledPositiveDirection)]
        public void T023_MoveAbsoluteDisabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveAbsoluteAxisDisabledPositiveDirection
                );
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = actpos + setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveAbsoluteAxisDisabledPositiveDirection,
                () => tc._done.Synchron
            );
            Assert.IsTrue(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
            while (tc._sut._axis._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveAbsoluteAxisEnabledPositiveDirection)]
        public void T024_MoveAbsoluteEnabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveAbsoluteAxisEnabledPositiveDirection
                );
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = actpos + setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveAbsoluteAxisEnabledPositiveDirection,
                () => tc._done.Synchron
            );
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.AreEqual(actpos + setpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveAbsoluteAxisDisabledNegativeDirection)]
        public void T025_MoveAbsoluteDisabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveAbsoluteAxisDisabledNegativeDirection
                );
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = actpos - setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveAbsoluteAxisDisabledNegativeDirection,
                () => tc._done.Synchron
            );
            Assert.IsTrue(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
            while (tc._sut._axis._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveAbsoluteAxisEnabledNegativeDirection)]
        public void T026_MoveAbsoluteEnabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveAbsoluteAxisEnabledNegativeDirection
                );
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = actpos - setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveAbsoluteAxisEnabledNegativeDirection,
                () => tc._done.Synchron
            );
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos - setpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveRelativeAxisDisabled)]
        public void T027_MoveRelativeDisabled()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.MoveRelativeAxisDisabled);
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 100;
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveRelativeAxisDisabled,
                () => tc._done.Synchron
            );
#if WITH_HARDWARE
            Assert.AreEqual(16992, tc._sut._axis._axisStatus.ErrorId.Synchron); //"Controller enable"

            Assert.IsTrue(tc._sut._axis._axisStatus.Error.Synchron);
#endif
            while (tc._sut._axis._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveRelativeAxisDisabledPositiveDirection)]
        public void T028_MoveRelativeDisabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveRelativeAxisDisabledPositiveDirection
                );
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveRelativeAxisDisabledPositiveDirection,
                () => tc._done.Synchron
            );
            Assert.IsTrue(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
            while (tc._sut._axis._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveRelativeAxisEnabledPositiveDirection)]
        public void T029_MoveRelativeEnabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveRelativeAxisEnabledPositiveDirection
                );
            }

            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveRelativeAxisEnabledPositiveDirection,
                () => tc._done.Synchron
            );
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.AreEqual(actpos + setpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveRelativeAxisDisabledNegativeDirection)]
        public void T030_MoveRelativeDisabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveRelativeAxisDisabledNegativeDirection
                );
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = -1000;
            if ((actpos + setpos) < 10 && (actpos + setpos) > -10)
                setpos = -2000; //just to ensure that final position won't be close to zero
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveRelativeAxisDisabledNegativeDirection,
                () => tc._done.Synchron
            );
            Assert.IsTrue(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos, tc._sut._axis._axisStatus.ActPos.Synchron, 1);
            while (tc._sut._axis._axisStatus.Error.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoSingleAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoSingleAxisTests.MoveRelativeAxisEnabledNegativeDirection)]
        public void T031_MoveRelativeEnabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoSingleAxisTests.MoveRelativeAxisEnabledNegativeDirection
                );
            }
            double actpos = tc._sut._axis._axisStatus.ActPos.Synchron;
            double setpos = -1000;
            if ((actpos + setpos) < 10 && (actpos + setpos) > -10)
                setpos = -2000; //just to ensure that final position won't be close to zero
            tc._position.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoSingleAxisTests.MoveRelativeAxisEnabledNegativeDirection,
                () => tc._done.Synchron
            );
            Assert.IsFalse(tc._sut._axis._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual((actpos + setpos), tc._sut._axis._axisStatus.ActPos.Synchron, 1);
        }
    }
}
