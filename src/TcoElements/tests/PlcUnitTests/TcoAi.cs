using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoElementsTests;
using TcoCore.Testing;
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
        [TestCase(50)]
        [TestCase(100)]
        public void T100_IsTrueTest(int signal)
        {
            //-- Arrange
            sut._sut._config.RawLow.Synchron = 0;
            sut._sut._config.RawHigh.Synchron = 1000;
            sut._sut._config.RealLow.Synchron = 0;
            sut._sut._config.RealHigh.Synchron = 100;

            sut._sut._config.Gain.Synchron = 1;
            sut._sut._config.Offset.Synchron = 0;

            sut._signal.Synchron = signal;

            //-- Act
            sut.Run(1, 100);

            //-- Assert
            if(signal != sut._ScaledResult.Synchron)
            { 
                Assert.AreEqual("Expecting `equal` values", sut._sut._messenger._mime.Text.Synchron);
            }
            
            Assert.IsTrue(signal == sut._ScaledResult.Synchron);
        }

        //[Test]
        //        [TestCase(true)]
        //        [TestCase(false)]
        //        public void T200_IsFalseTest(bool signal)
        //        {
        //            //-- Arrange
        //            sut._signal.Synchron = signal;
        //
        //            //-- Act
        //            sut.Run(1, 200);
        //
        //            //-- Assert
        //
        //            //-- Assert
        //            if (signal)
        //            {
        //                Assert.AreEqual("Expecting `negative` signal", sut._sut._messenger._mime.Text.Synchron);
        //            }
        //
        //            Assert.IsTrue(signal == sut._ScaledResult.Synchron);
        //        }
    }
}
