using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TcoCore;
using TcoCoreTests;
using TcOpen.Inxton.Logging;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

/// <summary>
/// NOTE! we use here the logger from the context instance
/// </summary>

namespace TcoCoreUnitTests.PlcExecutedTests
{
    [Timeout(1500000)]
    public class T14_TcoDaqPlcTests
    {
        TcoDaqTestContext tc = ConnectorFixture.Connector.MAIN._tcoDaqTestContext;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            tc.GetConnector().SuspendWriteProtection("Hoj morho vetvo mojho rodu, kto kramou rukou siahne na tvoju slobodu a co i dusu das v tom boji divokom vol nebyt ako byt otrokom!");
            tc._logger._plcCarret.Synchron = 0;
            var emptyMessage = new PlainTcoDaqDataItemImplementation();
            foreach (var item in tc._sut.Buffer.Select(p => p as TcoDaqDataItemImplementation))
            {
                item.FlushPlainToOnline(emptyMessage);
            }

        }

        [SetUp]
        public void SetUp()
        {
           
        }

        [Test]
        public void PushTest()
        {
            //Arrange
            tc._msg._s.Synchron = "hello i am buffered message";            
            tc._sut._plcCarret.Synchron = 0;            
            var index = tc._logger._plcCarret.Synchron;

            //Act
            tc.ExecuteProbeRun(1, (int)eTcoDaqTests.Push);
            
            var buffer = tc._sut.Buffer.Select(p => p as TcoDaqDataItemImplementation).ToArray();

            //Assert

            Assert.AreEqual(tc._msg._s .Synchron, buffer[index].Data._s.Synchron);
            Assert.AreEqual(tc._msg._i.Synchron, buffer[index].Data._i.Synchron);                        
        }


        [Test]
        public void PopTest()
        {
            //Arrange
            tc._msg._s.Synchron = "hello i am buffered message";
            tc._sut._plcCarret.Synchron = 0;
            var index = tc._logger._plcCarret.Synchron;

            //Act
            tc.ExecuteProbeRun(10, (int)eTcoDaqTests.Push);

            var buffer = tc._sut.Pop<PlainTcoDaqDataItemImplementation>().ToList();

            //Assert            
            Assert.AreEqual(10, buffer.Count());
            
        }
    }
}
