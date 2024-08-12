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
    public class TcoMultiAxisTests
    {
        TcoMultiAxisContext tc = ConnectorFixture.Connector.MAIN._tcoMultiAxisTestContext;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ConnectorFixture.Connector.MAIN._wpfContextCall.Synchron = false;
            ConnectorFixture.Connector.MAIN._multiAxisTestActive.Synchron = true;
            ConnectorFixture.Connector.MAIN._singleAxisTestActive.Synchron = false;

            tc.ExecuteProbeRun(2, (int)eTcoMultiAxisTests.Restore);
            tc.ExecuteProbeRun(2, (int)eTcoMultiAxisTests.CleanUp);
            tc.ExecuteProbeRun(2, (int)eTcoMultiAxisTests.Reset);
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;
            tc._plcCycleCounter.Synchron = 0;
        }

        [SetUp]
        public void Setup()
        {
            tc._axis1Disabled.Synchron = false;
            tc._axis2Disabled.Synchron = false;
            tc._axis3Disabled.Synchron = false;
            tc._axis4Disabled.Synchron = false;
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
            ConnectorFixture.Connector.MAIN._multiAxisTestActive.Synchron = true;
            ConnectorFixture.Connector.MAIN._singleAxisTestActive.Synchron = false;

            tc._done.Synchron = false;
            tc.ExecuteProbeRun(2, (int)eTcoMultiAxisTests.Restore);
            tc.ExecuteProbeRun(2, (int)eTcoMultiAxisTests.CleanUp);
            tc.ExecuteProbeRun(2, (int)eTcoMultiAxisTests.Reset);
            tc.ExecuteProbeRun(2, (int)eTcoMultiAxisTests.Stop);
            tc.ExecuteProbeRun(2, (int)eTcoMultiAxisTests.PowerDisable);
            tc.ExecuteProbeRun(2, (int)eTcoMultiAxisTests.Restore);
            tc._done.Synchron = false;
            tc._arranged.Synchron = false;
            tc._plcCycleCounter.Synchron = 0;
        }

        [Test, Order((int)eTcoMultiAxisTests.Message)]
        public void T003_Message()
        {
            string message = "Test error message";
            tc._inString.Synchron = message;
            tc.ExecuteProbeRun((int)eTcoMultiAxisTests.Message, () => tc._done.Synchron);
            Assert.AreEqual(message, tc._sut._messenger._mime.Text.Synchron); //Check if message apears in the mime.
            tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.CleanUp);
            Assert.AreEqual(string.Empty, tc._sut._messenger._mime.Text.Synchron);
        }

        [Test, Order((int)eTcoMultiAxisTests.PowerDisable)]
        public void T004_PowerDisable()
        {
            tc.ExecuteProbeRun((int)eTcoMultiAxisTests.PowerDisable, () => tc._done.Synchron);
            Assert.IsTrue(tc._sut._axis1._axisStatus.Disabled.Synchron);
            Assert.IsTrue(tc._sut._axis1._axisStatus.HasBeenStopped.Synchron);
            Assert.IsTrue(tc._sut._axis1._axisStatus.NotMoving.Synchron);

            Assert.IsTrue(tc._sut._axis2._axisStatus.Disabled.Synchron);
            Assert.IsTrue(tc._sut._axis2._axisStatus.HasBeenStopped.Synchron);
            Assert.IsTrue(tc._sut._axis2._axisStatus.NotMoving.Synchron);

            Assert.IsTrue(tc._sut._axis3._axisStatus.Disabled.Synchron);
            Assert.IsTrue(tc._sut._axis3._axisStatus.HasBeenStopped.Synchron);
            Assert.IsTrue(tc._sut._axis3._axisStatus.NotMoving.Synchron);

            Assert.IsTrue(tc._sut._axis4._axisStatus.Disabled.Synchron);
            Assert.IsTrue(tc._sut._axis4._axisStatus.HasBeenStopped.Synchron);
            Assert.IsTrue(tc._sut._axis4._axisStatus.NotMoving.Synchron);
#if WITH_HARDWARE
            Assert.IsFalse(tc._sut._axis1._axisStatus.Operational.Synchron);
            Assert.IsFalse(tc._sut._axis2._axisStatus.Operational.Synchron);
            Assert.IsFalse(tc._sut._axis3._axisStatus.Operational.Synchron);
            Assert.IsFalse(tc._sut._axis4._axisStatus.Operational.Synchron);
#endif
            Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);

            Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);

            Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);

            Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
        }

        [Test, Order((int)eTcoMultiAxisTests.PowerEnable)]
        public void T005_PowerEnable()
        {
            tc.ExecuteProbeRun((int)eTcoMultiAxisTests.PowerEnable, () => tc._done.Synchron);

#if WITH_HARDWARE
            Assert.IsFalse(tc._sut._axis._axisStatus.Disabled.Synchron);
#endif
            Assert.IsTrue(tc._sut._axis1._axisStatus.HasBeenStopped.Synchron);
            Assert.IsTrue(tc._sut._axis1._axisStatus.NotMoving.Synchron);
            Assert.IsTrue(tc._sut._axis1._axisStatus.Operational.Synchron);
            Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);

            Assert.IsTrue(tc._sut._axis2._axisStatus.HasBeenStopped.Synchron);
            Assert.IsTrue(tc._sut._axis2._axisStatus.NotMoving.Synchron);
            Assert.IsTrue(tc._sut._axis2._axisStatus.Operational.Synchron);
            Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);

            Assert.IsTrue(tc._sut._axis3._axisStatus.HasBeenStopped.Synchron);
            Assert.IsTrue(tc._sut._axis3._axisStatus.NotMoving.Synchron);
            Assert.IsTrue(tc._sut._axis3._axisStatus.Operational.Synchron);
            Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);

            Assert.IsTrue(tc._sut._axis4._axisStatus.HasBeenStopped.Synchron);
            Assert.IsTrue(tc._sut._axis4._axisStatus.NotMoving.Synchron);
            Assert.IsTrue(tc._sut._axis4._axisStatus.Operational.Synchron);
            Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
        }

        [Test, Order((int)eTcoMultiAxisTests.Reset)]
        public void T006_Reset()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.Reset);
            }
            Assert.IsTrue(tc._sut._axis1._axisStatus.Error.Synchron);
            Assert.IsTrue(tc._sut._axis2._axisStatus.Error.Synchron);
            Assert.IsTrue(tc._sut._axis3._axisStatus.Error.Synchron);
            Assert.IsTrue(tc._sut._axis4._axisStatus.Error.Synchron);

            tc.ExecuteProbeRun((int)eTcoMultiAxisTests.Reset, () => tc._done.Synchron);

            Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);

            Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);

            Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);

            Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
        }

        [Test, Order((int)eTcoMultiAxisTests.HommingDirect)]
        public void T011_HommingDirect()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.HommingDirect);
            }
            double actpos1 = tc._sut._axis1._axisStatus.ActPos.Synchron;
            double setpos1 = 2 * actpos1;
            tc._positionAxis1.Synchron = setpos1;
            tc._velocity.Synchron = 100;

            double actpos2 = tc._sut._axis2._axisStatus.ActPos.Synchron;
            double setpos2 = 2 * actpos2;
            tc._positionAxis2.Synchron = setpos2;
            tc._velocity.Synchron = 100;

            double actpos3 = tc._sut._axis3._axisStatus.ActPos.Synchron;
            double setpos3 = 2 * actpos3;

            tc._positionAxis3.Synchron = setpos3;
            tc._velocity.Synchron = 100;

            double actpos4 = tc._sut._axis4._axisStatus.ActPos.Synchron;
            double setpos4 = 2 * actpos4;
            tc._positionAxis4.Synchron = setpos4;
            tc._velocity.Synchron = 100;

            tc.ExecuteProbeRun((int)eTcoMultiAxisTests.HommingDirect, () => tc._done.Synchron);

            Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);
            Assert.AreEqual(setpos1, tc._sut._axis1._axisStatus.ActPos.Synchron, 1);

            Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);
            Assert.AreEqual(setpos2, tc._sut._axis2._axisStatus.ActPos.Synchron, 1);

            Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);
            Assert.AreEqual(setpos3, tc._sut._axis3._axisStatus.ActPos.Synchron, 1);

            Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
            Assert.AreEqual(setpos4, tc._sut._axis4._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoMultiAxisTests.MoveAbsoluteDisabled)]
        public void T017_MoveAbsoluteDisabled()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.MoveAbsoluteDisabled);
            }

            double setpos = 1000;
            tc._positionAxis1.Synchron = setpos;
            tc._positionAxis2.Synchron = setpos;
            tc._positionAxis3.Synchron = setpos;
            tc._positionAxis4.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoMultiAxisTests.MoveAbsoluteDisabled,
                () => tc._done.Synchron
            );
#if WITH_HARDWARE
            Assert.AreEqual(16992, tc._sut._axis1._axisStatus.ErrorId.Synchron); //"Controller enable"

            Assert.IsTrue(tc._sut._axis1._axisStatus.Error.Synchron);

            Assert.AreEqual(16992, tc._sut._axis2._axisStatus.ErrorId.Synchron); //"Controller enable"

            Assert.IsTrue(tc._sut._axis2._axisStatus.Error.Synchron);

            Assert.AreEqual(16992, tc._sut._axis3._axisStatus.ErrorId.Synchron); //"Controller enable"

            Assert.IsTrue(tc._sut._axis3._axisStatus.Error.Synchron);

            Assert.AreEqual(16992, tc._sut._axis4._axisStatus.ErrorId.Synchron); //"Controller enable"

            Assert.IsTrue(tc._sut._axis4._axisStatus.Error.Synchron);
#endif
            while (
                tc._sut._axis1._axisStatus.Error.Synchron
                && tc._sut._axis2._axisStatus.Error.Synchron
                && tc._sut._axis3._axisStatus.Error.Synchron
                && tc._sut._axis4._axisStatus.Error.Synchron
            )
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoMultiAxisTests.MoveAbsoluteDisabledPositiveDirection)]
        public void T018_MoveAbsoluteDisabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoMultiAxisTests.MoveAbsoluteDisabledPositiveDirection
                );
            }
            double actpos1 = tc._sut._axis1._axisStatus.ActPos.Synchron;
            double actpos2 = tc._sut._axis2._axisStatus.ActPos.Synchron;
            double actpos3 = tc._sut._axis3._axisStatus.ActPos.Synchron;
            double actpos4 = tc._sut._axis4._axisStatus.ActPos.Synchron;

            double setpos = 1000;
            tc._positionAxis1.Synchron = actpos1 + setpos;
            tc._positionAxis2.Synchron = actpos2 + setpos;
            tc._positionAxis3.Synchron = actpos3 + setpos;
            tc._positionAxis4.Synchron = actpos4 + setpos;

            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoMultiAxisTests.MoveAbsoluteDisabledPositiveDirection,
                () => tc._done.Synchron
            );
            Assert.IsTrue(tc._sut._axis1._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis1._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos1, tc._sut._axis1._axisStatus.ActPos.Synchron, 1);

            Assert.IsTrue(tc._sut._axis2._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis2._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos2, tc._sut._axis2._axisStatus.ActPos.Synchron, 1);

            Assert.IsTrue(tc._sut._axis3._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis3._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos3, tc._sut._axis3._axisStatus.ActPos.Synchron, 1);

            Assert.IsTrue(tc._sut._axis4._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis4._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos4, tc._sut._axis4._axisStatus.ActPos.Synchron, 1);

            while (
                tc._sut._axis1._axisStatus.Error.Synchron
                && tc._sut._axis2._axisStatus.Error.Synchron
                && tc._sut._axis3._axisStatus.Error.Synchron
                && tc._sut._axis4._axisStatus.Error.Synchron
            )
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoMultiAxisTests.MoveAbsoluteEnabledPositiveDirection)]
        public void T019_MoveAbsoluteEnabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.MoveAbsoluteEnabledPositiveDirection);
            }
            double actpos1 = tc._sut._axis1._axisStatus.ActPos.Synchron;
            double actpos2 = tc._sut._axis2._axisStatus.ActPos.Synchron;
            double actpos3 = tc._sut._axis3._axisStatus.ActPos.Synchron;
            double actpos4 = tc._sut._axis4._axisStatus.ActPos.Synchron;

            double setpos = 1000;
            tc._positionAxis1.Synchron = actpos1 + setpos;
            tc._positionAxis2.Synchron = actpos2 + setpos;
            tc._positionAxis3.Synchron = actpos3 + setpos;
            tc._positionAxis4.Synchron = actpos4 + setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoMultiAxisTests.MoveAbsoluteEnabledPositiveDirection,
                () => tc._done.Synchron
            );
            Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);
            Assert.AreEqual(actpos1 + setpos, tc._sut._axis1._axisStatus.ActPos.Synchron, 1);

            Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);
            Assert.AreEqual(actpos2 + setpos, tc._sut._axis2._axisStatus.ActPos.Synchron, 1);

            Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);
            Assert.AreEqual(actpos3 + setpos, tc._sut._axis3._axisStatus.ActPos.Synchron, 1);

            Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
            Assert.AreEqual(actpos4 + setpos, tc._sut._axis4._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoMultiAxisTests.MoveAbsoluteDisabledNegativeDirection)]
        public void T020_MoveAbsoluteDisabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoMultiAxisTests.MoveAbsoluteDisabledNegativeDirection
                );
            }
            double actpos1 = tc._sut._axis1._axisStatus.ActPos.Synchron;
            double actpos2 = tc._sut._axis2._axisStatus.ActPos.Synchron;
            double actpos3 = tc._sut._axis3._axisStatus.ActPos.Synchron;
            double actpos4 = tc._sut._axis4._axisStatus.ActPos.Synchron;

            double setpos = 1000;
            tc._positionAxis1.Synchron = actpos1 - setpos;
            tc._positionAxis2.Synchron = actpos2 - setpos;
            tc._positionAxis3.Synchron = actpos3 - setpos;
            tc._positionAxis4.Synchron = actpos4 - setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoMultiAxisTests.MoveAbsoluteDisabledNegativeDirection,
                () => tc._done.Synchron
            );

            Assert.IsTrue(tc._sut._axis1._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis1._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos1, tc._sut._axis1._axisStatus.ActPos.Synchron, 1);

            Assert.IsTrue(tc._sut._axis2._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis2._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos2, tc._sut._axis2._axisStatus.ActPos.Synchron, 1);

            Assert.IsTrue(tc._sut._axis3._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis3._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos3, tc._sut._axis3._axisStatus.ActPos.Synchron, 1);

            Assert.IsTrue(tc._sut._axis4._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis4._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos4, tc._sut._axis4._axisStatus.ActPos.Synchron, 1);

            while (
                tc._sut._axis1._axisStatus.Error.Synchron
                && tc._sut._axis2._axisStatus.Error.Synchron
                && tc._sut._axis3._axisStatus.Error.Synchron
                && tc._sut._axis4._axisStatus.Error.Synchron
            )
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.Reset);
            }

            Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoMultiAxisTests.MoveAbsoluteEnabledNegativeDirection)]
        public void T021_MoveAbsoluteEnabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.MoveAbsoluteEnabledNegativeDirection);
            }
            double actpos1 = tc._sut._axis1._axisStatus.ActPos.Synchron;
            double actpos2 = tc._sut._axis2._axisStatus.ActPos.Synchron;
            double actpos3 = tc._sut._axis3._axisStatus.ActPos.Synchron;
            double actpos4 = tc._sut._axis4._axisStatus.ActPos.Synchron;

            double setpos = 1000;
            tc._positionAxis1.Synchron = actpos1 - setpos;
            tc._positionAxis2.Synchron = actpos2 - setpos;
            tc._positionAxis3.Synchron = actpos3 - setpos;
            tc._positionAxis4.Synchron = actpos4 - setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoMultiAxisTests.MoveAbsoluteEnabledNegativeDirection,
                () => tc._done.Synchron
            );
            Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos1 - setpos, tc._sut._axis1._axisStatus.ActPos.Synchron, 1);

            Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos2 - setpos, tc._sut._axis2._axisStatus.ActPos.Synchron, 1);

            Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos3 - setpos, tc._sut._axis3._axisStatus.ActPos.Synchron, 1);

            Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos4 - setpos, tc._sut._axis4._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoMultiAxisTests.MoveAbsoluteAxisDisabled)]
        public void T022_MoveAbsoluteDisabled()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.MoveAbsoluteAxisDisabled);
            }
            double actpos1 = tc._sut._axis1._axisStatus.ActPos.Synchron;
            double actpos2 = tc._sut._axis2._axisStatus.ActPos.Synchron;
            double actpos3 = tc._sut._axis3._axisStatus.ActPos.Synchron;
            double actpos4 = tc._sut._axis4._axisStatus.ActPos.Synchron;

            double setpos = 1000;
            tc._positionAxis1.Synchron = setpos;
            tc._positionAxis2.Synchron = setpos;
            tc._positionAxis3.Synchron = setpos;
            tc._positionAxis4.Synchron = setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoMultiAxisTests.MoveAbsoluteAxisDisabled,
                () => tc._done.Synchron
            );
#if WITH_HARDWARE
            Assert.AreEqual(16992, tc._sut._axis1._axisStatus.ErrorId.Synchron); //"Controller enable"

            Assert.IsTrue(tc._sut._axis1._axisStatus.Error.Synchron);

            Assert.AreEqual(16992, tc._sut._axis2._axisStatus.ErrorId.Synchron); //"Controller enable"

            Assert.IsTrue(tc._sut._axis2._axisStatus.Error.Synchron);

            Assert.AreEqual(16992, tc._sut._axis3._axisStatus.ErrorId.Synchron); //"Controller enable"

            Assert.IsTrue(tc._sut._axis3._axisStatus.Error.Synchron);

            Assert.AreEqual(16992, tc._sut._axis4._axisStatus.ErrorId.Synchron); //"Controller enable"

            Assert.IsTrue(tc._sut._axis4._axisStatus.Error.Synchron);
#endif
            while (
                tc._sut._axis1._axisStatus.Error.Synchron
                && tc._sut._axis2._axisStatus.Error.Synchron
                && tc._sut._axis3._axisStatus.Error.Synchron
                && tc._sut._axis4._axisStatus.Error.Synchron
            )
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.CleanUp);
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.Reset);
            }
            Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoMultiAxisTests.MoveAbsoluteAxisDisabledPositiveDirection)]
        public void T023_MoveAbsoluteDisabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.CleanUp);
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoMultiAxisTests.MoveAbsoluteAxisDisabledPositiveDirection
                );
            }
            double actpos1 = tc._sut._axis1._axisStatus.ActPos.Synchron;
            double actpos2 = tc._sut._axis2._axisStatus.ActPos.Synchron;
            double actpos3 = tc._sut._axis3._axisStatus.ActPos.Synchron;
            double actpos4 = tc._sut._axis4._axisStatus.ActPos.Synchron;

            double setpos = 1000;
            tc._positionAxis1.Synchron = actpos1 + setpos;
            tc._positionAxis2.Synchron = actpos2 + setpos;
            tc._positionAxis3.Synchron = actpos3 + setpos;
            tc._positionAxis4.Synchron = actpos4 + setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoMultiAxisTests.MoveAbsoluteAxisDisabledPositiveDirection,
                () => tc._done.Synchron
            );

            Assert.IsTrue(tc._sut._axis1._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis1._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos1, tc._sut._axis1._axisStatus.ActPos.Synchron, 1);

            Assert.IsTrue(tc._sut._axis2._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis2._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos2, tc._sut._axis2._axisStatus.ActPos.Synchron, 1);

            Assert.IsTrue(tc._sut._axis3._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis3._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos3, tc._sut._axis3._axisStatus.ActPos.Synchron, 1);

            Assert.IsTrue(tc._sut._axis4._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis4._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos4, tc._sut._axis4._axisStatus.ActPos.Synchron, 1);

            while (
                tc._sut._axis1._axisStatus.Error.Synchron
                && tc._sut._axis2._axisStatus.Error.Synchron
                && tc._sut._axis3._axisStatus.Error.Synchron
                && tc._sut._axis4._axisStatus.Error.Synchron
            )
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.CleanUp);
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.Reset);
            }

            Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoMultiAxisTests.MoveAbsoluteAxisEnabledPositiveDirection)]
        public void T024_MoveAbsoluteEnabledPositiveDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.CleanUp);
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoMultiAxisTests.MoveAbsoluteAxisEnabledPositiveDirection
                );
            }
            double actpos1 = tc._sut._axis1._axisStatus.ActPos.Synchron;
            double actpos2 = tc._sut._axis2._axisStatus.ActPos.Synchron;
            double actpos3 = tc._sut._axis3._axisStatus.ActPos.Synchron;
            double actpos4 = tc._sut._axis4._axisStatus.ActPos.Synchron;

            double setpos = 1000;
            tc._positionAxis1.Synchron = actpos1 + setpos;
            tc._positionAxis2.Synchron = actpos2 + setpos;
            tc._positionAxis3.Synchron = actpos3 + setpos;
            tc._positionAxis4.Synchron = actpos4 + setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoMultiAxisTests.MoveAbsoluteAxisEnabledPositiveDirection,
                () => tc._done.Synchron
            );

            Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);
            Assert.AreEqual(actpos1 + setpos, tc._sut._axis1._axisStatus.ActPos.Synchron, 1);

            Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);
            Assert.AreEqual(actpos2 + setpos, tc._sut._axis2._axisStatus.ActPos.Synchron, 1);

            Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);
            Assert.AreEqual(actpos3 + setpos, tc._sut._axis3._axisStatus.ActPos.Synchron, 1);

            Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
            Assert.AreEqual(actpos4 + setpos, tc._sut._axis4._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoMultiAxisTests.MoveAbsoluteAxisDisabledNegativeDirection)]
        public void T025_MoveAbsoluteDisabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.CleanUp);
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoMultiAxisTests.MoveAbsoluteAxisDisabledNegativeDirection
                );
            }
            double actpos1 = tc._sut._axis1._axisStatus.ActPos.Synchron;
            double actpos2 = tc._sut._axis2._axisStatus.ActPos.Synchron;
            double actpos3 = tc._sut._axis3._axisStatus.ActPos.Synchron;
            double actpos4 = tc._sut._axis4._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._positionAxis1.Synchron = actpos1 - setpos;
            tc._positionAxis2.Synchron = actpos2 - setpos;
            tc._positionAxis3.Synchron = actpos3 - setpos;
            tc._positionAxis4.Synchron = actpos4 - setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoMultiAxisTests.MoveAbsoluteAxisDisabledNegativeDirection,
                () => tc._done.Synchron
            );
            Assert.IsTrue(tc._sut._axis1._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis1._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos1, tc._sut._axis1._axisStatus.ActPos.Synchron, 1);

            Assert.IsTrue(tc._sut._axis2._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis2._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos2, tc._sut._axis2._axisStatus.ActPos.Synchron, 1);

            Assert.IsTrue(tc._sut._axis3._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis3._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos3, tc._sut._axis3._axisStatus.ActPos.Synchron, 1);

            Assert.IsTrue(tc._sut._axis4._axisStatus.Error.Synchron);
            Assert.AreEqual(16931, tc._sut._axis4._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
            Assert.AreEqual(actpos4, tc._sut._axis4._axisStatus.ActPos.Synchron, 1);

            while (
                tc._sut._axis1._axisStatus.Error.Synchron
                && tc._sut._axis2._axisStatus.Error.Synchron
                && tc._sut._axis3._axisStatus.Error.Synchron
                && tc._sut._axis4._axisStatus.Error.Synchron
            )
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.CleanUp);

                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.Reset);
            }

            Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);

            Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
            Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
        }

        [Test, Order((int)eTcoMultiAxisTests.MoveAbsoluteAxisEnabledNegativeDirection)]
        public void T026_MoveAbsoluteEnabledNegativeDirection()
        {
            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.CleanUp);
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoMultiAxisTests.MoveAbsoluteAxisEnabledNegativeDirection
                );
            }
            double actpos1 = tc._sut._axis1._axisStatus.ActPos.Synchron;
            double actpos2 = tc._sut._axis2._axisStatus.ActPos.Synchron;
            double actpos3 = tc._sut._axis3._axisStatus.ActPos.Synchron;
            double actpos4 = tc._sut._axis4._axisStatus.ActPos.Synchron;

            double setpos = 1000;
            tc._positionAxis1.Synchron = actpos1 - setpos;
            tc._positionAxis2.Synchron = actpos2 - setpos;
            tc._positionAxis3.Synchron = actpos3 - setpos;
            tc._positionAxis4.Synchron = actpos4 - setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoMultiAxisTests.MoveAbsoluteAxisEnabledNegativeDirection,
                () => tc._done.Synchron
            );

            Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);
            Assert.AreEqual(actpos1 - setpos, tc._sut._axis1._axisStatus.ActPos.Synchron, 1);

            Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);
            Assert.AreEqual(actpos2 - setpos, tc._sut._axis2._axisStatus.ActPos.Synchron, 1);

            Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);
            Assert.AreEqual(actpos3 - setpos, tc._sut._axis3._axisStatus.ActPos.Synchron, 1);

            Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
            Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
            Assert.AreEqual(actpos4 - setpos, tc._sut._axis4._axisStatus.ActPos.Synchron, 1);
        }

        [Test, Order((int)eTcoMultiAxisTests.MoveAbsoluteAxisDisabledPositiveDirectionAxisDisabled)]
        [TestCase(false, false, false, true)]
        [TestCase(true, false, true, true)]
        [TestCase(true, true, false, true)]
        [TestCase(true, false, true, false)]
        [TestCase(true, false, false, false)]
        [TestCase(false, false, false, false)]
        public void T027_MoveAbsoluteDisabledPositiveDirection(
            bool axisDisabled1,
            bool axisDisabled2,
            bool axisDisabled3,
            bool axisDisabled4
        )
        {
            tc._axis1Disabled.Synchron = axisDisabled1;
            tc._axis2Disabled.Synchron = axisDisabled2;
            tc._axis3Disabled.Synchron = axisDisabled3;
            tc._axis4Disabled.Synchron = axisDisabled4;

            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.CleanUp);
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoMultiAxisTests.MoveAbsoluteAxisDisabledPositiveDirectionAxisDisabled
                );
            }
            double actpos1 = tc._sut._axis1._axisStatus.ActPos.Synchron;
            double actpos2 = tc._sut._axis2._axisStatus.ActPos.Synchron;
            double actpos3 = tc._sut._axis3._axisStatus.ActPos.Synchron;
            double actpos4 = tc._sut._axis4._axisStatus.ActPos.Synchron;

            double setpos = 1000;
            tc._positionAxis1.Synchron = actpos1 + setpos;
            tc._positionAxis2.Synchron = actpos2 + setpos;
            tc._positionAxis3.Synchron = actpos3 + setpos;
            tc._positionAxis4.Synchron = actpos4 + setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoMultiAxisTests.MoveAbsoluteAxisDisabledPositiveDirectionAxisDisabled,
                () => tc._done.Synchron
            );

            if (!axisDisabled1)
            {
                Assert.IsTrue(tc._sut._axis1._axisStatus.Error.Synchron);
                Assert.AreEqual(16931, tc._sut._axis1._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
                Assert.AreEqual(actpos1, tc._sut._axis1._axisStatus.ActPos.Synchron, 1);
            }
            if (!axisDisabled2)
            {
                Assert.IsTrue(tc._sut._axis2._axisStatus.Error.Synchron);
                Assert.AreEqual(16931, tc._sut._axis2._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
                Assert.AreEqual(actpos2, tc._sut._axis2._axisStatus.ActPos.Synchron, 1);
            }

            if (!axisDisabled3)
            {
                Assert.IsTrue(tc._sut._axis3._axisStatus.Error.Synchron);
                Assert.AreEqual(16931, tc._sut._axis3._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
                Assert.AreEqual(actpos3, tc._sut._axis3._axisStatus.ActPos.Synchron, 1);
            }
            if (!axisDisabled4)
            {
                Assert.IsTrue(tc._sut._axis4._axisStatus.Error.Synchron);
                Assert.AreEqual(16931, tc._sut._axis4._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
                Assert.AreEqual(actpos4, tc._sut._axis4._axisStatus.ActPos.Synchron, 1);
            }
            while (
                !(tc._sut._axis1._axisStatus.Error.Synchron ^ !axisDisabled1)
                && !(tc._sut._axis2._axisStatus.Error.Synchron ^ !axisDisabled2)
                && !(tc._sut._axis3._axisStatus.Error.Synchron ^ !axisDisabled3)
                && !(tc._sut._axis4._axisStatus.Error.Synchron ^ !axisDisabled4)
            )
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.CleanUp);
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.Reset);
            }
            if (!axisDisabled1)
            {
                Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);
                Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);
            }
            if (!axisDisabled2)
            {
                Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);
                Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);
            }
            if (!axisDisabled3)
            {
                Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);
                Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);
            }
            if (!axisDisabled4)
            {
                Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
                Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
            }
        }

        [Test, Order((int)eTcoMultiAxisTests.MoveAbsoluteAxisEnabledPositiveDirectionAxisDisabled)]
        [TestCase(false, false, false, true)]
        [TestCase(true, false, true, true)]
        [TestCase(true, true, false, true)]
        [TestCase(true, false, true, false)]
        [TestCase(true, false, false, false)]
        public void T028_MoveAbsoluteEnabledPositiveDirection(
            bool axisDisabled1,
            bool axisDisabled2,
            bool axisDisabled3,
            bool axisDisabled4
        )
        {
            tc._axis1Disabled.Synchron = axisDisabled1;
            tc._axis2Disabled.Synchron = axisDisabled2;
            tc._axis3Disabled.Synchron = axisDisabled3;
            tc._axis4Disabled.Synchron = axisDisabled4;

            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.CleanUp);
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoMultiAxisTests.MoveAbsoluteAxisEnabledPositiveDirectionAxisDisabled
                );
            }
            double actpos1 = tc._sut._axis1._axisStatus.ActPos.Synchron;
            double actpos2 = tc._sut._axis2._axisStatus.ActPos.Synchron;
            double actpos3 = tc._sut._axis3._axisStatus.ActPos.Synchron;
            double actpos4 = tc._sut._axis4._axisStatus.ActPos.Synchron;

            double setpos = 1000;
            tc._positionAxis1.Synchron = actpos1 + setpos;
            tc._positionAxis2.Synchron = actpos2 + setpos;
            tc._positionAxis3.Synchron = actpos3 + setpos;
            tc._positionAxis4.Synchron = actpos4 + setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoMultiAxisTests.MoveAbsoluteAxisEnabledPositiveDirectionAxisDisabled,
                () => tc._done.Synchron
            );

            if (!axisDisabled1)
            {
                Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);
                Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);
                Assert.AreEqual(actpos1 + setpos, tc._sut._axis1._axisStatus.ActPos.Synchron, 1);
            }
            if (!axisDisabled2)
            {
                Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);
                Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);
                Assert.AreEqual(actpos2 + setpos, tc._sut._axis2._axisStatus.ActPos.Synchron, 1);
            }
            if (!axisDisabled3)
            {
                Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);
                Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);
                Assert.AreEqual(actpos3 + setpos, tc._sut._axis3._axisStatus.ActPos.Synchron, 1);
            }
            if (!axisDisabled4)
            {
                Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
                Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
                Assert.AreEqual(actpos4 + setpos, tc._sut._axis4._axisStatus.ActPos.Synchron, 1);
            }
        }

        [Test, Order((int)eTcoMultiAxisTests.MoveAbsoluteAxisDisabledNegativeDirectionAxisDisabled)]
        [TestCase(false, false, false, true)]
        [TestCase(true, false, true, true)]
        [TestCase(true, true, false, true)]
        [TestCase(true, false, true, false)]
        [TestCase(true, false, false, false)]
        [TestCase(false, false, false, false)]
        public void T029_MoveAbsoluteDisabledNegativeDirection(
            bool axisDisabled1,
            bool axisDisabled2,
            bool axisDisabled3,
            bool axisDisabled4
        )
        {
            tc._axis1Disabled.Synchron = axisDisabled1;
            tc._axis2Disabled.Synchron = axisDisabled2;
            tc._axis3Disabled.Synchron = axisDisabled3;
            tc._axis4Disabled.Synchron = axisDisabled4;

            while (!tc._arranged.Synchron)
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.CleanUp);
                tc.ExecuteProbeRun(
                    1,
                    (int)eTcoMultiAxisTests.MoveAbsoluteAxisDisabledNegativeDirectionAxisDisabled
                );
            }
            double actpos1 = tc._sut._axis1._axisStatus.ActPos.Synchron;
            double actpos2 = tc._sut._axis2._axisStatus.ActPos.Synchron;
            double actpos3 = tc._sut._axis3._axisStatus.ActPos.Synchron;
            double actpos4 = tc._sut._axis4._axisStatus.ActPos.Synchron;
            double setpos = 1000;
            tc._positionAxis1.Synchron = actpos1 - setpos;
            tc._positionAxis2.Synchron = actpos2 - setpos;
            tc._positionAxis3.Synchron = actpos3 - setpos;
            tc._positionAxis4.Synchron = actpos4 - setpos;
            tc._velocity.Synchron = 1400;
            tc._acceleration.Synchron = 50000;
            tc._deceleration.Synchron = 50000;
            tc._jerk.Synchron = 150000;
            tc.ExecuteProbeRun(
                (int)eTcoMultiAxisTests.MoveAbsoluteAxisDisabledNegativeDirectionAxisDisabled,
                () => tc._done.Synchron
            );

            if (!axisDisabled1)
            {
                Assert.IsTrue(tc._sut._axis1._axisStatus.Error.Synchron);
                Assert.AreEqual(16931, tc._sut._axis1._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
                Assert.AreEqual(actpos1, tc._sut._axis1._axisStatus.ActPos.Synchron, 1);
            }
            if (!axisDisabled2)
            {
                Assert.IsTrue(tc._sut._axis2._axisStatus.Error.Synchron);
                Assert.AreEqual(16931, tc._sut._axis2._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
                Assert.AreEqual(actpos2, tc._sut._axis2._axisStatus.ActPos.Synchron, 1);
            }
            if (!axisDisabled3)
            {
                Assert.IsTrue(tc._sut._axis3._axisStatus.Error.Synchron);
                Assert.AreEqual(16931, tc._sut._axis3._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
                Assert.AreEqual(actpos3, tc._sut._axis3._axisStatus.ActPos.Synchron, 1);
            }
            if (!axisDisabled4)
            {
                Assert.IsTrue(tc._sut._axis4._axisStatus.Error.Synchron);
                Assert.AreEqual(16931, tc._sut._axis4._axisStatus.ErrorId.Synchron); //"No enable for controller and/or feed (Master axis)"
                Assert.AreEqual(actpos4, tc._sut._axis4._axisStatus.ActPos.Synchron, 1);
            }
            while (
                !(tc._sut._axis1._axisStatus.Error.Synchron ^ !axisDisabled1)
                && !(tc._sut._axis2._axisStatus.Error.Synchron ^ !axisDisabled2)
                && !(tc._sut._axis3._axisStatus.Error.Synchron ^ !axisDisabled3)
                && !(tc._sut._axis4._axisStatus.Error.Synchron ^ !axisDisabled4)
            )
            {
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.CleanUp);
                tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.Reset);
            }

            if (!axisDisabled1)
            {
                Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);
                Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);
            }
            if (!axisDisabled2)
            {
                Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);
                Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);
            }
            if (!axisDisabled3)
            {
                Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);
                Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);
            }
            if (!axisDisabled4)
            {
                Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
                Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
            }
        }

        [Test, Order((int)eTcoMultiAxisTests.MoveAbsoluteAxisEnabledNegativeDirectionAxisDisabled)]
        [TestCase(false, true, true, true)]
        [TestCase(true, false, true, true)]
        [TestCase(true, true, false, true)]
        [TestCase(true, false, true, false)]
        [TestCase(true, false, false, false)]
        public void T030_MoveAbsoluteEnabledNegativeDirection(
            bool axisDisabled1,
            bool axisDisabled2,
            bool axisDisabled3,
            bool axisDisabled4
        )
        {
            tc._axis1Disabled.Synchron = axisDisabled1;
            tc._axis2Disabled.Synchron = axisDisabled2;
            tc._axis3Disabled.Synchron = axisDisabled3;
            tc._axis4Disabled.Synchron = axisDisabled4;
            {
                while (!tc._arranged.Synchron)
                {
                    tc.ExecuteProbeRun(1, (int)eTcoMultiAxisTests.CleanUp);
                    tc.ExecuteProbeRun(
                        1,
                        (int)eTcoMultiAxisTests.MoveAbsoluteAxisEnabledNegativeDirectionAxisDisabled
                    );
                }
                double actpos1 = tc._sut._axis1._axisStatus.ActPos.Synchron;
                double actpos2 = tc._sut._axis2._axisStatus.ActPos.Synchron;
                double actpos3 = tc._sut._axis3._axisStatus.ActPos.Synchron;
                double actpos4 = tc._sut._axis4._axisStatus.ActPos.Synchron;

                double setpos = 1000;
                tc._positionAxis1.Synchron = actpos1 - setpos;
                tc._positionAxis2.Synchron = actpos2 - setpos;
                tc._positionAxis3.Synchron = actpos3 - setpos;
                tc._positionAxis4.Synchron = actpos4 - setpos;
                tc._velocity.Synchron = 1400;
                tc._acceleration.Synchron = 50000;
                tc._deceleration.Synchron = 50000;
                tc._jerk.Synchron = 150000;
                tc.ExecuteProbeRun(
                    (int)eTcoMultiAxisTests.MoveAbsoluteAxisEnabledNegativeDirectionAxisDisabled,
                    () => tc._done.Synchron
                );

                if (!axisDisabled1)
                {
                    Assert.IsFalse(tc._sut._axis1._axisStatus.Error.Synchron);
                    Assert.AreEqual(0, tc._sut._axis1._axisStatus.ErrorId.Synchron);
                    Assert.AreEqual(
                        actpos1 - setpos,
                        tc._sut._axis1._axisStatus.ActPos.Synchron,
                        1
                    );
                }
                if (!axisDisabled2)
                {
                    Assert.IsFalse(tc._sut._axis2._axisStatus.Error.Synchron);
                    Assert.AreEqual(0, tc._sut._axis2._axisStatus.ErrorId.Synchron);
                    Assert.AreEqual(
                        actpos2 - setpos,
                        tc._sut._axis2._axisStatus.ActPos.Synchron,
                        1
                    );
                }
                if (!axisDisabled3)
                {
                    Assert.IsFalse(tc._sut._axis3._axisStatus.Error.Synchron);
                    Assert.AreEqual(0, tc._sut._axis3._axisStatus.ErrorId.Synchron);
                    Assert.AreEqual(
                        actpos3 - setpos,
                        tc._sut._axis3._axisStatus.ActPos.Synchron,
                        1
                    );
                }

                if (!axisDisabled4)
                {
                    Assert.IsFalse(tc._sut._axis4._axisStatus.Error.Synchron);
                    Assert.AreEqual(0, tc._sut._axis4._axisStatus.ErrorId.Synchron);
                    Assert.AreEqual(
                        actpos4 - setpos,
                        tc._sut._axis4._axisStatus.ActPos.Synchron,
                        1
                    );
                }
            }
        }
    }
}
