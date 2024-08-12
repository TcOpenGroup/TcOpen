using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoCore.Testing;
using TcoElementsTests;
using Vortex.Connector;

namespace TcoElementsUnitTests
{
    public class TcoAo
    {
        TcoAoTests sut;

        [OneTimeSetUp]
        public void Setup()
        {
            Entry.TcoElementsTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
            sut = Entry.TcoElementsTests.MAIN._tcoAnalogActuatorTests;
        }

        [Test]
        public void T50_NotInitialized()
        {
            //-- Act
            sut.Run(1, 50);
            sut.Read();
        }

        [Test]
        [TestCase(100, 100)]
        [TestCase(50, 50)]
        [TestCase(0, 0)]
        [TestCase(-50, -50)]
        [TestCase(-100, -100)]
        public void T200_RoundedRawRoundedReal_OneCycle_Test(int required, float setVal)
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = -100;
            sut._sut._config.RawHigh.Synchron = 100;
            sut._sut._config.RealLow.Synchron = -100;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 0;

            sut._sut._requiredValue.Synchron = setVal;

            //-- Act
            sut.Run(1, 200);

            //-- Assert
            if (required != sut._UnscaledResult.Synchron)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(required == sut._UnscaledResult.Synchron);
        }

        [Test]
        [TestCase(32767, 100, 0)]
        [TestCase(32767 / 2, 100 / 2, 1)]
        [TestCase(0, 0, 1)]
        public void T100_UnsignedRawTest(double required, float setVal, int epsilon)
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = 0;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = 0;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 0;

            sut._sut._requiredValue.Synchron = setVal;

            //-- Act

            sut.Run(1, 100);

            //-- Assert
            var isEqual =
                ((sut._UnscaledResult.Synchron - epsilon) <= required)
                && (required <= (sut._UnscaledResult.Synchron + epsilon));
            if (!isEqual)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(isEqual);
        }

        [Test]
        [TestCase(32767, 100, 0)]
        [TestCase(0, 50, 1)]
        [TestCase(-32768, 0, 0)]
        public void T100_SignedRawTest(double required, float setVal, int epsilon)
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = -32768;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = 0;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 0;

            sut._sut._requiredValue.Synchron = setVal;

            //-- Act

            sut.Run(1, 100);

            //-- Assert
            var isEqual =
                ((sut._UnscaledResult.Synchron - epsilon) <= required)
                && (required <= (sut._UnscaledResult.Synchron + epsilon));
            if (!isEqual)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(isEqual);
        }

        [Test]
        [TestCase(32767, 100, 0)]
        [TestCase(32767 / 2, 75, 1)]
        [TestCase(0, 50, 0)]
        public void T100_UnsignedRaw_RealOffsetTest(double required, float setVal, int epsilon)
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = 0;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = 50;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 0;

            sut._sut._requiredValue.Synchron = setVal;

            //-- Act

            sut.Run(2, 100);

            //-- Assert
            var isEqual =
                ((sut._UnscaledResult.Synchron - epsilon) <= required)
                && (required <= (sut._UnscaledResult.Synchron + epsilon));
            if (!isEqual)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(isEqual);
        }

        [Test]
        [TestCase(32767, 100, 0)]
        [TestCase(0, 75, 1)]
        [TestCase(-32768, 50, 0)]
        public void T100_SignedRaw_RealOffsetTest(double required, float setVal, int epsilon)
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = -32768;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = 50;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 0;

            sut._sut._requiredValue.Synchron = setVal;

            //-- Act

            sut.Run(1, 100);

            //-- Assert
            var isEqual =
                ((sut._UnscaledResult.Synchron - epsilon) <= required)
                && (required <= (sut._UnscaledResult.Synchron + epsilon));
            if (!isEqual)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(isEqual);
        }

        [Test]
        [TestCase(32767, 100, 0)]
        [TestCase(32767 / 2, 0, 2)]
        [TestCase(0, -100, 0)]
        public void T100_UnsignedRaw_RealSignedOffsetTest(
            double required,
            float setVal,
            int epsilon
        )
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = 0;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = -100;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 0;

            sut._sut._requiredValue.Synchron = setVal;

            //-- Act

            sut.Run(1, 100);

            //-- Assert
            var isEqual =
                ((sut._UnscaledResult.Synchron - epsilon) <= required)
                && (required <= (sut._UnscaledResult.Synchron + epsilon));
            if (!isEqual)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(isEqual);
        }

        [Test]
        [TestCase(32767, 100, 0)]
        [TestCase(0, 0, 1)]
        [TestCase(-32768, -100, 0)]
        public void T100_SignedRaw_RealSignedOffsetTest(double required, float setVal, int epsilon)
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = -32768;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = -100;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 0;
            sut._sut._requiredValue.Synchron = setVal;

            //-- Act

            sut.Run(1, 100);

            //-- Assert
            var isEqual =
                ((sut._UnscaledResult.Synchron - epsilon) <= required)
                && (required <= (sut._UnscaledResult.Synchron + epsilon));
            if (!isEqual)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(isEqual);
        }

        [Test]
        [TestCase(32787, 100, 0)]
        [TestCase(16404, 0, 1)]
        [TestCase(20, -100, 0)]
        public void T100_UnsignedRaw_RealSignedOffset_WithOffsetCorrectionTest(
            double required,
            float setVal,
            int epsilon
        )
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = 0;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = -100;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 20;

            sut._sut._requiredValue.Synchron = setVal;

            //-- Act

            sut.Run(1, 100);

            //-- Assert
            var isEqual =
                ((sut._UnscaledResult.Synchron - epsilon) <= required)
                && (required <= (sut._UnscaledResult.Synchron + epsilon));
            if (!isEqual)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(isEqual);
        }

        [Test]
        [TestCase(32787, 100, 0)]
        [TestCase(20, 0, 1)]
        [TestCase(-32748, -100, 0)]
        public void T100_SignedRaw_RealSignedOffset_WithOffsetCorrectionTest(
            double required,
            float setVal,
            int epsilon
        )
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = -32768;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = -100;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 20;

            sut._sut._requiredValue.Synchron = setVal;

            //-- Act

            sut.Run(1, 100);

            //-- Assert
            var isEqual =
                ((sut._UnscaledResult.Synchron - epsilon) <= required)
                && (required <= (sut._UnscaledResult.Synchron + epsilon));
            if (!isEqual)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(isEqual);
        }
    }
}
