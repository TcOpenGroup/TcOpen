using System.IO;
using System.Reflection;
using NUnit.Framework;
using TcoCore.Testing;
using TcoPneumatics;
using TcoPneumaticsTests;

namespace TcoPneumaticsTests
{
    public class DoubleCylinderTests
    {
        DoubleCylinderTestContext sut = ConnectorFixture.Connector.MAIN._doubleCylinderTests;

        [OneTimeSetUp()]
        public void OneTimeSetUp() { }

        [SetUp]
        public void TestSetup()
        {
            sut._atHomeSignal.Synchron = false;
            sut._atWorkSignal.Synchron = false;
            sut.ExecuteProbeRun(1, (int)eCyclinderTests.StopMovement);
        }

        [Test]
        [Timeout(10000)]
        [Order(50)]
        public void Invalid()
        {
            sut.ExecuteProbeRun(2, (int)eCyclinderTests.Invalid);
        }

        [Test]
        [Timeout(10000)]
        [Order(50)]
        public void TestTaskLableInterpolation()
        {
            Assert.AreEqual("MOVE HOME SWEET HOME", sut._sut._moveHomeDefault.AttributeName);
            Assert.AreEqual("MOVE TO WORK HARD WORK", sut._sut._moveWorkDefault.AttributeName);
            Assert.AreEqual("STOP AND THINK", sut._sut._stopDefault.AttributeName);
        }

        [Test]
        [Timeout(10000)]
        [Order(100)]
        public void StopMovement()
        {
            sut._atHomeSignal.Synchron = false;
            sut._atWorkSignal.Synchron = false;
            sut.ExecuteProbeRun(2, (int)eCyclinderTests.StopMovement);
            Assert.AreEqual(false, sut._moveHomeSignal.Synchron);
            Assert.AreEqual(false, sut._moveWorkSignal.Synchron);
        }

        [Test]
        [Timeout(10000)]
        [Order(200)]
        public void MoveHomeMoving()
        {
            sut.ExecuteProbeRun(1, (int)eCyclinderTests.MoveHomeMoving);
            Assert.AreEqual(false, sut._atHomeSignal.Synchron);
            Assert.AreEqual(false, sut._moveHomeDone.Synchron);

            sut.ExecuteProbeRun(1, (int)eCyclinderTests.MoveHomeMoving);
            Assert.AreEqual(false, sut._atHomeSignal.Synchron);
            Assert.AreEqual(false, sut._moveWorkSignal.Synchron);
            Assert.AreEqual(true, sut._moveHomeSignal.Synchron);
            Assert.AreEqual(false, sut._moveHomeDone.Synchron);
        }

        [Test]
        [Timeout(10000)]
        [Order(300)]
        public void MoveHomeMovingReached()
        {
            sut.ExecuteProbeRun(1, (int)eCyclinderTests.MoveHomeMovingReached);
            Assert.AreEqual(false, sut._moveHomeDone.Synchron);

            sut.ExecuteProbeRun(1, (int)eCyclinderTests.MoveHomeMovingReached);
            Assert.AreEqual(false, sut._moveWorkSignal.Synchron);
            Assert.AreEqual(true, sut._moveHomeSignal.Synchron);
            Assert.AreEqual(false, sut._moveHomeDone.Synchron);

            sut._atHomeSignal.Synchron = true;

            sut.ExecuteProbeRun(1, (int)eCyclinderTests.MoveHomeMovingReached);

            Assert.AreEqual(false, sut._moveWorkSignal.Synchron);
            Assert.AreEqual(true, sut._atHomeSignal.Synchron);
            Assert.AreEqual(true, sut._moveHomeSignal.Synchron);
            Assert.AreEqual(true, sut._moveHomeDone.Synchron);
        }

        [Test]
        [Timeout(10000)]
        [Order(400)]
        public void MoveWorkMoving()
        {
            sut.ExecuteProbeRun(1, (int)eCyclinderTests.MoveWorkMoving);
            Assert.AreEqual(false, sut._atWorkSignal.Synchron);
            Assert.AreEqual(false, sut._moveWorkDone.Synchron);

            sut.ExecuteProbeRun(1, (int)eCyclinderTests.MoveWorkMoving);
            Assert.AreEqual(false, sut._atWorkSignal.Synchron);
            Assert.AreEqual(false, sut._moveHomeSignal.Synchron);
            Assert.AreEqual(true, sut._moveWorkSignal.Synchron);
            Assert.AreEqual(false, sut._moveWorkDone.Synchron);
        }

        [Test]
        [Timeout(10000)]
        [Order(500)]
        public void MoveWorkMovingReached()
        {
            sut.ExecuteProbeRun(1, (int)eCyclinderTests.MoveWorkMovingReached);
            Assert.AreEqual(false, sut._moveWorkDone.Synchron);

            sut.ExecuteProbeRun(1, (int)eCyclinderTests.MoveWorkMovingReached);
            Assert.AreEqual(false, sut._moveHomeSignal.Synchron);
            Assert.AreEqual(true, sut._moveWorkSignal.Synchron);
            Assert.AreEqual(false, sut._moveWorkDone.Synchron);

            sut._atWorkSignal.Synchron = true;

            sut.ExecuteProbeRun(1, (int)eCyclinderTests.MoveWorkMovingReached);

            Assert.AreEqual(false, sut._moveHomeSignal.Synchron);
            Assert.AreEqual(true, sut._atWorkSignal.Synchron);
            Assert.AreEqual(true, sut._moveWorkSignal.Synchron);
            Assert.AreEqual(true, sut._moveWorkDone.Synchron);
        }

        [Test]
        [Timeout(10000)]
        [Order(600)]
        public void AbortMoveTask()
        {
            sut.ExecuteProbeRun(1, (int)eCyclinderTests.MoveHomeMoving);
            Assert.AreEqual(false, sut._atHomeSignal.Synchron);
            Assert.AreEqual(false, sut._moveHomeDone.Synchron);

            sut.ExecuteProbeRun(1, (int)eCyclinderTests.MoveHomeMoving);
            Assert.AreEqual(false, sut._atHomeSignal.Synchron);
            Assert.AreEqual(false, sut._moveWorkSignal.Synchron);
            Assert.AreEqual(true, sut._moveHomeSignal.Synchron);
            Assert.AreEqual(false, sut._moveHomeDone.Synchron);

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.AbortMoveTask);
            Assert.AreEqual(false, sut._moveWorkSignal.Synchron);
            Assert.AreEqual(false, sut._moveHomeSignal.Synchron);
        }

        [Test]
        [Timeout(10000)]
        [Order(700)]
        public void MoveTaskAlarm()
        {
            var timeToAlarm = new System.TimeSpan(0, 0, 0, 0, 50);
            sut._sut._config.TimeToReachHomePosition.Synchron = timeToAlarm;

            sut.ExecuteProbeRun(1, (int)eCyclinderTests.MoveHomeMoving);
            Assert.AreEqual(false, sut._atHomeSignal.Synchron);
            Assert.AreEqual(false, sut._moveHomeDone.Synchron);

            System.Threading.Thread.Sleep((int)timeToAlarm.TotalMilliseconds);
            sut.ExecuteProbeRun(
                (int)eCyclinderTests.MoveHomeMoving,
                () =>
                {
                    for (int i = 0; i < ((int)timeToAlarm.TotalMilliseconds / 10) + 1; i++)
                    {
                        System.Threading.Thread.Sleep(10);
                    }

                    return true;
                }
            );

            Assert.AreEqual(
                "Home sensor not reached yet.",
                sut._sut._moveHomeDefault._messenger._mime.Text.Synchron
            );
        }

        [Test]
        [Timeout(10000)]
        [Order(700)]
        public void Both_proximity_signals_active_error()
        {
            var timeToAlarm = new System.TimeSpan(0, 0, 0, 0, 50);
            sut._sut._config.TimeToReachHomePosition.Synchron = timeToAlarm;

            sut.ExecuteProbeRun(1, 0);
            sut._atHomeSignal.Synchron = true;
            sut._atWorkSignal.Synchron = true;
            sut.ExecuteProbeRun(1, 0);

            Assert.AreEqual(
                "Home and work position sensors are both active. Check the position of sensors!",
                sut._sut._messenger._mime.Text.Synchron
            );
        }

        [Test]
        [Timeout(10000)]
        [Order((int)eCyclinderTests.DisableHome)]
        [TestCase(true)]
        [TestCase(false)]
        public void Disable_move_home_action_disabled(bool disabledSignal)
        {
            sut._sut._messenger.Clear();
            var timeToAlarm = new System.TimeSpan(0, 0, 0, 0, 50);
            sut._sut._config.TimeToReachHomePosition.Synchron = timeToAlarm;

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.StopMovement);

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.MoveHomeMoving);

            Assert.AreEqual(false, sut._atHomeSignal.Synchron);
            Assert.AreEqual(true, sut._moveHomeSignal.Synchron);

            sut._disableSignal.Synchron = disabledSignal;
            sut.ExecuteProbeRun(1, (int)eCyclinderTests.DisableHome);

            Assert.AreEqual(false, sut._atHomeSignal.Synchron);
            Assert.AreEqual(!disabledSignal, sut._moveHomeSignal.Synchron);

            if (disabledSignal)
            {
                Assert.AreEqual(
                    "Movement aborted due to : MAIN._doubleCylinderTests._disableSignal",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }
        }

        [Test]
        [Timeout(10000)]
        [Order((int)eCyclinderTests.DisableWork)]
        [TestCase(true)]
        [TestCase(false)]
        public void Disable_move_work_action_disabled(bool disabledSignal)
        {
            sut._sut._messenger.Clear();
            var timeToAlarm = new System.TimeSpan(0, 0, 0, 0, 50);
            sut._sut._config.TimeToReachHomePosition.Synchron = timeToAlarm;

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.StopMovement);

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.MoveWorkMoving);

            Assert.AreEqual(false, sut._atWorkSignal.Synchron);
            Assert.AreEqual(true, sut._moveWorkSignal.Synchron);

            sut._disableSignal.Synchron = disabledSignal;
            sut.ExecuteProbeRun(1, (int)eCyclinderTests.DisableWork);

            Assert.AreEqual(false, sut._atWorkSignal.Synchron);
            Assert.AreEqual(!disabledSignal, sut._moveWorkSignal.Synchron);

            if (disabledSignal)
            {
                Assert.AreEqual(
                    "Movement aborted due to : MAIN._doubleCylinderTests._disableSignal",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }
        }

        [Test]
        [Timeout(10000)]
        [Order((int)eCyclinderTests.SuspendHome)]
        [TestCase(true)]
        [TestCase(false)]
        public void Suspend_move_home_action_disabled(bool suspendSignal)
        {
            sut._sut._messenger.Clear();
            var timeToAlarm = new System.TimeSpan(0, 0, 0, 0, 50);
            sut._sut._config.TimeToReachHomePosition.Synchron = timeToAlarm;

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.StopMovement);

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.MoveHomeMoving);

            Assert.AreEqual(false, sut._atHomeSignal.Synchron);
            Assert.AreEqual(true, sut._moveHomeSignal.Synchron);

            sut._suspendSignal.Synchron = suspendSignal;
            sut.ExecuteProbeRun(1, (int)eCyclinderTests.SuspendHome);

            Assert.AreEqual(false, sut._atHomeSignal.Synchron);
            Assert.AreEqual(!suspendSignal, sut._moveHomeSignal.Synchron);

            if (suspendSignal)
            {
                Assert.AreEqual(
                    "Movement suspended due to : MAIN._doubleCylinderTests._suspendSignal",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }
        }

        [Test]
        [Timeout(10000)]
        [Order((int)eCyclinderTests.SuspendWork)]
        [TestCase(true)]
        [TestCase(false)]
        public void Suspend_move_work_action_disabled(bool suspendSignal)
        {
            sut._sut._messenger.Clear();
            var timeToAlarm = new System.TimeSpan(0, 0, 0, 0, 50);
            sut._sut._config.TimeToReachWorkPosition.Synchron = timeToAlarm;

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.StopMovement);

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.MoveWorkMoving);

            Assert.AreEqual(false, sut._atWorkSignal.Synchron);
            Assert.AreEqual(true, sut._moveWorkSignal.Synchron);

            sut._suspendSignal.Synchron = suspendSignal;
            sut.ExecuteProbeRun(1, (int)eCyclinderTests.SuspendWork);

            Assert.AreEqual(false, sut._atWorkSignal.Synchron);
            Assert.AreEqual(!suspendSignal, sut._moveWorkSignal.Synchron);

            if (suspendSignal)
            {
                Assert.AreEqual(
                    "Movement suspended due to : MAIN._doubleCylinderTests._suspendSignal",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }
        }

        [Test]
        [Timeout(10000)]
        [Order((int)eCyclinderTests.SuspendHomeInverted)]
        [TestCase(true)]
        [TestCase(false)]
        public void Suspend_move_home_inverted_action_disabled(bool suspendSignal)
        {
            sut._sut._messenger.Clear();
            var timeToAlarm = new System.TimeSpan(0, 0, 0, 0, 50);
            sut._sut._config.TimeToReachHomePosition.Synchron = timeToAlarm;

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.StopMovement);

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.MoveHomeMoving);

            Assert.AreEqual(false, sut._atHomeSignal.Synchron);
            Assert.AreEqual(true, sut._moveHomeSignal.Synchron);

            sut._suspendSignal.Synchron = suspendSignal;
            sut.ExecuteProbeRun(1, (int)eCyclinderTests.SuspendHomeInverted);

            Assert.AreEqual(false, sut._atHomeSignal.Synchron);
            Assert.AreEqual(suspendSignal, sut._moveHomeSignal.Synchron);

            if (!suspendSignal)
            {
                Assert.AreEqual(
                    "Movement suspended due to NOT : MAIN._doubleCylinderTests._suspendSignal",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }
        }

        [Test]
        [Timeout(10000)]
        [Order((int)eCyclinderTests.SuspendWorkInverted)]
        [TestCase(true)]
        [TestCase(false)]
        public void Suspend_move_work_inverted_action_disabled(bool suspendSignal)
        {
            sut._sut._messenger.Clear();
            var timeToAlarm = new System.TimeSpan(0, 0, 0, 0, 50);
            sut._sut._config.TimeToReachWorkPosition.Synchron = timeToAlarm;

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.StopMovement);

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.MoveWorkMoving);

            Assert.AreEqual(false, sut._atWorkSignal.Synchron);
            Assert.AreEqual(true, sut._moveWorkSignal.Synchron);

            sut._suspendSignal.Synchron = suspendSignal;
            sut.ExecuteProbeRun(1, (int)eCyclinderTests.SuspendWorkInverted);

            Assert.AreEqual(false, sut._atWorkSignal.Synchron);
            Assert.AreEqual(suspendSignal, sut._moveWorkSignal.Synchron);

            if (!suspendSignal)
            {
                Assert.AreEqual(
                    "Movement suspended due to NOT : MAIN._doubleCylinderTests._suspendSignal",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }
        }

        [Test]
        [Timeout(10000)]
        [Order((int)eCyclinderTests.SuspendHomeExpression)]
        [TestCase(true, "SUSPEND")]
        [TestCase(false, "SUSPEND")]
        public void Suspend_move_home_expression_action_disabled(bool suspendSignal, string msg)
        {
            sut._sut._messenger.Clear();
            var timeToAlarm = new System.TimeSpan(0, 0, 0, 0, 50);
            sut._sut._config.TimeToReachHomePosition.Synchron = timeToAlarm;

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.StopMovement);

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.MoveHomeMoving);
            Assert.AreEqual(false, sut._atHomeSignal.Synchron);
            Assert.AreEqual(true, sut._moveHomeSignal.Synchron);

            sut._expresionMessage.Synchron = msg;
            sut._suspendSignal.Synchron = suspendSignal;
            sut.ExecuteProbeRun(1, (int)eCyclinderTests.SuspendHomeExpression);

            Assert.AreEqual(false, sut._atHomeSignal.Synchron);
            Assert.AreEqual(!suspendSignal, sut._moveHomeSignal.Synchron);

            if (suspendSignal)
            {
                Assert.AreEqual(
                    "Movement suspended due to : " + msg,
                    sut._sut._messenger._mime.Text.Synchron
                );
            }
        }

        [Test]
        [Timeout(10000)]
        [Order((int)eCyclinderTests.SuspendWorkExpression)]
        [TestCase(true, "SUSPEND")]
        [TestCase(false, "SUSPEND")]
        public void Suspend_move_work_expression_action_disabled(bool suspendSignal, string msg)
        {
            sut._sut._messenger.Clear();
            var timeToAlarm = new System.TimeSpan(0, 0, 0, 0, 50);
            sut._sut._config.TimeToReachWorkPosition.Synchron = timeToAlarm;

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.StopMovement);

            sut.ExecuteProbeRun(2, (int)eCyclinderTests.MoveWorkMoving);

            Assert.AreEqual(false, sut._atWorkSignal.Synchron);
            Assert.AreEqual(true, sut._moveWorkSignal.Synchron);

            sut._expresionMessage.Synchron = msg;
            sut._suspendSignal.Synchron = suspendSignal;
            sut.ExecuteProbeRun(1, (int)eCyclinderTests.SuspendWorkExpression);

            Assert.AreEqual(false, sut._atWorkSignal.Synchron);
            Assert.AreEqual(!suspendSignal, sut._moveWorkSignal.Synchron);

            if (suspendSignal)
            {
                Assert.AreEqual(
                    "Movement suspended due to : " + msg,
                    sut._sut._messenger._mime.Text.Synchron
                );
            }
        }
    }
}
