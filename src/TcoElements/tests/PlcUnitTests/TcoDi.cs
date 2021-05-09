using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoElementsTests;
using TcoCore.Testing;
using Vortex.Connector;

namespace TcoElementsUnitTests
{
    public class TcoDi
    {
        TcoDiTests sut;

        [OneTimeSetUp]
        public void Setup()
        {
            Entry.TcoElementsTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
            sut = Entry.TcoElementsTests.MAIN._tcoDigitalSensorTests;
        }

        [Test]        
        public void T50_NotInitialized()
        {                      
            //-- Act
            sut.Run(1, 50);
            sut.Read();            
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void T100_IsTrueTest(bool signal)
        {          
            //-- Arrange
            sut._signal.Synchron = signal;
            
            //-- Act
            sut.Run(1, 100);

            //-- Assert
            if(!signal)
            { 
                Assert.AreEqual("Expecting `positive` signal", sut._sut._messenger._mime.Text.Synchron);
            }
            
            Assert.IsTrue(signal == sut._IsTrue_result.Synchron);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void T200_IsFalseTest(bool signal)
        {
            //-- Arrange
            sut._signal.Synchron = signal;

            //-- Act
            sut.Run(1, 200);

            //-- Assert

            //-- Assert
            if (signal)
            {
                Assert.AreEqual("Expecting `negative` signal", sut._sut._messenger._mime.Text.Synchron);
            }

            Assert.IsTrue(!signal == sut._IsFalse_result.Synchron);
        }
    }
}
