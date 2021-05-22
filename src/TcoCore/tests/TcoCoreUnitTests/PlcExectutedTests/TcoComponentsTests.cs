using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCoreTests;
using TcoCore.Testing;
using TcoCore;

namespace TcoCoreUnitTests.PlcExecutedTests
{

    public class TcoComponentsTests
    {



        TcoComponentTestContext tc = ConnectorFixture.Connector.MAIN._tcoComponentTestContext;

      

       
        [SetUp]
        public void Setup()
        {
           
        }

     
        [Test]
        public void GetSignalInfo()
        {
            tc.Run(1, (int)eTcoComponentTests.GetSignalInfo);

            Assert.IsNotNull(tc._sut._signalInfo.Signal);
            Assert.AreEqual("--Some IO signal", tc._sut._signalInfo.Signal.AttributeName);
            Assert.AreEqual("Test base component class.--Some IO signal", tc._sut._signalInfo.Signal.HumanReadable);
            Assert.AreEqual("MAIN._tcoComponentTestContext._signal", tc._sut._signalInfo.Signal.Symbol);
        }     
    }
}
