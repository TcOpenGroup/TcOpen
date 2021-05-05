using NUnit.Framework;
using System;
using System.Text;
using System.Threading;
using TcoDrivesBeckhoffTests;
using TcoDrivesBeckhoff;

namespace TcoDrivesBeckhoffUnitTests
{

    public class T01_TcoDriveSimpleTests
    {

        TcoDrivesBeckhoffTests.TcoDrivesBeckhoffContext tc = TcoDrivesBeckhoffUnitTests.ConnectorFixture.Connector.MAIN._tcoDrivesBeckhoffContext;
        //TcoDrivesBeckhoffTests.TcoDriveSimpleTest ti = TcoDrivesBeckhoffUnitTests.ConnectorFixture.Connector.MAIN._tcoDriveSimpleTestContext._axis;


        ushort cycles;

        [OneTimeSetUp]
        public void OneSetup()
        {
            tc.SingleCycleRun(() =>
            {
                tc._axis.InvokeRestore();
                tc.Axis();
            });
        }


        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }

        [TearDown]
        public void TearDown()
        {
            tc.SingleCycleRun(() =>
            {
                tc._axis.InvokeRestore();
                tc.Axis();
            });
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(100)]
        public void T100_PowerDisable()
        {
            //--Arrange
            cycles = 10;
            tc._enable.Synchron = false;

            //--Act
            tc.MultipleCycleRun(() =>
            {
                tc.Axis();
            }, cycles);

            //--Assert
            Assert.IsTrue(tc._axis._axisStatus.Disabled.Synchron);
            Assert.IsTrue(tc._axis._axisStatus.HasBeenStopped.Synchron);
            Assert.IsTrue(tc._axis._axisStatus.NotMoving.Synchron);
            Assert.IsFalse(tc._axis._axisStatus.Operational.Synchron);
        }

        [Test, Order(101)]
        public void T101_PowerEnable()
        {
            //--Arrange
            cycles = 10;
            //tc._enable.Synchron = true;

            ////--Act
            //tc.MultipleCycleRun(() =>
            //{
            //    tc.Axis();
            //}, cycles);

            ////--Assert
            //Assert.IsFalse(tc._axis._axisStatus.Disabled.Synchron);
            //Assert.IsTrue(tc._axis._axisStatus.HasBeenStopped.Synchron);
            //Assert.IsTrue(tc._axis._axisStatus.NotMoving.Synchron);
            //Assert.IsTrue(tc._axis._axisStatus.Operational.Synchron);
        }


    }
}