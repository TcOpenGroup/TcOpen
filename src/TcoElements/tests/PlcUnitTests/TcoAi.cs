using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoCore.Testing;
using TcoElementsTests;
using Vortex.Connector;

namespace TcoElementsUnitTests
{
    public class TcoAi
    {
        TcoAiTests sut;

        [OneTimeSetUp]
        public void Setup()
        {
            Entry.TcoElementsTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
            sut = Entry.TcoElementsTests.MAIN._tcoAnalogSensorTests;
        }

        [Test]
        public void T50_NotInitialized()
        {
            //-- Act
            sut.Run(1, 50);
            sut.Read();
        }

        [Test]
        [TestCase(100, 100, 0)]
        [TestCase(50, 50, 0)]
        [TestCase(0, 0, 0)]
        [TestCase(-50, -50, 0)]
        [TestCase(-100, -100, 0)]
        public void T100_RoundedRawRoundedRealTest(int signal, double required, int digit)
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = -100;
            sut._sut._config.RawHigh.Synchron = 100;
            sut._sut._config.RealLow.Synchron = -100;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 0;

            sut._signal.Synchron = signal;

            //-- Act
            sut.Run(1, 100);

            var rounded = Math.Round(sut._ScaledResult.Synchron, digit);

            //-- Assert
            if (required != rounded)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(required == rounded);
        }

        [Test]
        [TestCase(32767, 100, 0)]
        [TestCase(32767 / 2, 100 / 2, 2)]
        [TestCase(0, 0, 0)]
        public void T100_UnsignedRawTest(int signal, double required, int digit)
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = 0;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = 0;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 0;

            sut._signal.Synchron = signal;

            //-- Act

            sut.Run(1, 100);

            var rounded = Math.Round(sut._ScaledResult.Synchron, digit);

            //-- Assert
            if (required != rounded)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(required == rounded);
        }

        [Test]
        [TestCase(32767, 100, 0)]
        [TestCase(0, 50, 2)]
        [TestCase(-32768, 0, 0)]
        public void T100_SignedRawTest(int signal, double required, int digit)
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = -32768;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = 0;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 0;

            sut._signal.Synchron = signal;

            //-- Act

            sut.Run(1, 100);

            var rounded = Math.Round(sut._ScaledResult.Synchron, digit);

            //-- Assert
            if (required != rounded)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(required == rounded);
        }

        [Test]
        [TestCase(32767, 100, 0)]
        [TestCase(32767 / 2, 75, 2)]
        [TestCase(0, 50, 0)]
        public void T100_UnsignedRaw_RealOffsetTest(int signal, double required, int digit)
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = 0;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = 50;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 0;

            sut._signal.Synchron = signal;

            //-- Act
            sut.Run(1, 100);

            var rounded = Math.Round(sut._ScaledResult.Synchron, digit);

            //-- Assert
            if (required != rounded)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(required == rounded);
        }

        [Test]
        [TestCase(32767, 100, 0)]
        [TestCase(0, 75, 2)]
        [TestCase(-32768, 50, 0)]
        public void T100_SignedRaw_RealOffsetTest(int signal, double required, int digit)
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = -32768;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = 50;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 0;

            sut._signal.Synchron = signal;

            //-- Act
            sut.Run(1, 100);

            var rounded = Math.Round(sut._ScaledResult.Synchron, digit);

            //-- Assert
            if (required != rounded)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(required == rounded);
        }

        [Test]
        [TestCase(32767, 100, 0)]
        [TestCase(32767 / 2, 0, 2)]
        [TestCase(0, -100, 0)]
        public void T100_UnsignedRaw_RealSignedOffsetTest(int signal, double required, int digit)
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = 0;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = -100;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 0;

            sut._signal.Synchron = signal;

            //-- Act
            sut.Run(1, 100);

            var rounded = Math.Round(sut._ScaledResult.Synchron, digit);

            //-- Assert
            if (required != rounded)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(required == rounded);
        }

        [Test]
        [TestCase(32767, 100, 0)]
        [TestCase(0, 0, 2)]
        [TestCase(-32768, -100, 0)]
        public void T100_SignedRaw_RealSignedOffsetTest(int signal, double required, int digit)
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = -32768;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = -100;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 0;

            sut._signal.Synchron = signal;

            //-- Act
            sut.Run(1, 100);

            var rounded = Math.Round(sut._ScaledResult.Synchron, digit);

            //-- Assert
            if (required != rounded)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(required == rounded);
        }

        [Test]
        [TestCase(32767, 120, 0)]
        [TestCase(32767 / 2, 20, 2)]
        [TestCase(0, -80, 0)]
        public void T100_UnsignedRaw_RealSignedOffset_WithOffsetCorrectionTest(
            int signal,
            double required,
            int digit
        )
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = 0;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = -100;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 20;

            sut._signal.Synchron = signal;

            //-- Act
            sut.Run(1, 100);

            var rounded = Math.Round(sut._ScaledResult.Synchron, digit);

            //-- Assert
            if (required != rounded)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(required == rounded);
        }

        [Test]
        [TestCase(32767, 120, 0)]
        [TestCase(0, 20, 2)]
        [TestCase(-32768, -80, 0)]
        public void T100_SignedRaw_RealSignedOffset_WithOffsetCorrectionTest(
            int signal,
            double required,
            int digit
        )
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = -32768;
            sut._sut._config.RawHigh.Synchron = 32767;
            sut._sut._config.RealLow.Synchron = -100;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 20;

            sut._signal.Synchron = signal;

            //-- Act
            sut.Run(1, 100);

            var rounded = Math.Round(sut._ScaledResult.Synchron, digit);

            //-- Assert
            if (required != rounded)
            {
                Assert.AreEqual(
                    "Expecting `equal` values",
                    sut._sut._messenger._mime.Text.Synchron
                );
            }

            Assert.IsTrue(required == rounded);
        }
    }
}
