using NUnit.Framework;
using TcoCore;
using TcoCoreTests;
using TcOpen.Inxton.Logging;
using Vortex.Connector.ValueTypes;

/// <summary>
/// NOTE! we use here the logger from the context instance
/// </summary>

namespace TcoCoreUnitTests.PlcExecutedTests
{
    [Timeout(5000)]
    public class TcoLoggerPlcTests
    {
        TcoLoggerTestContext tc = ConnectorFixture.Connector.MAIN._tcoLoggerTestContext;
       
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            tc.GetConnector().SuspendWriteProtection("Hoj morho vetvo mojho rodu, kto kramou rukou siahne na tvoju slobodu a co i dusu das v tom boji divokom vol nebyt ako byt otrokom!");
            tc._logger._index.Synchron = 0;
            var emptyMessage = new PlainTcoMessage();
            foreach (var item in tc._logger._buffer.Buffer)
            {
                item.FlushPlainToOnline(emptyMessage);
            }


            tc._logger.StartLoggingMessages();
     
        }

        [Test, Order((int)eTcoRemoteTaskTests.InvokeInitializedCSharpMethod)]
        [TestCase("This is info message", TcoCore.eMessageCategory.Info)]
        [TestCase("This is error message", TcoCore.eMessageCategory.Error)]
        public void PushTest(string message, eMessageCategory category)
        {
            //Arrange
            tc._msg.Text.Synchron = message;
            tc._msg.Category.Synchron = (short)category;
            var index = tc._logger._index.Synchron;
            //Act
            tc.ExecuteProbeRun(1, (int)eTcoLoggerTests.Push);

            //Assert
            Assert.AreEqual(tc._msg.Text.Synchron, tc._logger._buffer.Buffer[index].Text.Synchron);
            Assert.AreEqual(tc._msg.Category.Synchron, tc._logger._buffer.Buffer[index].Category.Synchron);

            var logger = TcOpen.Inxton.TcoAppDomain.Current.Logger as DummyLoggerAdapter;

            System.Threading.Thread.Sleep(tc.GetConnector().ReadWriteCycleDelay * 2);

            Assert.AreEqual(("This is error message {@sender}"), logger.LastMessage.message);
            Assert.AreEqual(("Error"), logger.LastMessage.serverity);
          
        }
    }
}
