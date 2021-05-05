using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoElementsTests;
using TcoCore.Testing;
using Vortex.Connector;

namespace TcoElementsUnitTests
{
    public class TcoDigitalActuator
    {
        TcoDigitalActuatorTests sut;

        [OneTimeSetUp]
        public void Setup()
        {
            Entry.TcoElementsTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
            sut = Entry.TcoElementsTests.MAIN._tcoDigitalActuatorTests;
        }

        [Test]        
        public void T50_NotInitialized()
        {                      
            //-- Act
            sut.Run(1, 50);
            sut.Read();            
        }

        [Test]                
        public void T100_SetTest()
        {                      
            //-- Act
            sut.Run(1, 100);

            //-- Assert             
            Assert.AreEqual("Setting signal (on/true)", sut._sut._messenger._mime.Text.Synchron);                        
            Assert.IsTrue(sut._signal.Synchron);

            sut.Run(1, 150);

            Assert.IsTrue(sut._IsTrue_result.Synchron);
            Assert.IsFalse(sut._IsFalse_result.Synchron);
        }

        [Test]                
        public void T200_ResetTest()
        {          
            //-- Act
            sut.Run(1, 200);
          
            //-- Assert                        
            Assert.AreEqual("Reseting signal (off/false)", sut._sut._messenger._mime.Text.Synchron);            
            Assert.IsFalse(sut._signal.Synchron);

            sut.Run(1, 250);

            Assert.IsFalse(sut._IsTrue_result.Synchron);
            Assert.IsTrue(sut._IsFalse_result.Synchron);
        }        
    }
}
