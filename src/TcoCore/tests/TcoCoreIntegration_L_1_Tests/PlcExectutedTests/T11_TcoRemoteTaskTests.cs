using NUnit.Framework;
using TcoCoreTests;
using Vortex.Connector.ValueTypes;

namespace TcoCoreUnitTests.PlcExecutedTests
{
    [Timeout(5000)]
    public class T11_TcoRemoteTaskTests
    {
        TcoRemoteTaskTestContext tc = ConnectorFixture.Connector.MAIN._tcoRemoteTaskTestContext;
        OnlinerInt sharedVariable;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            sharedVariable = ConnectorFixture.Connector.MAIN._tcoRemoteTaskTestContext._sharedVariable;
            tc.ExecuteProbeRun(10, (int)eTcoRemoteTaskTests.RestoreTasks);
        }

        [SetUp]
        public void RunTwoCycles() => tc.ExecuteProbeRun(2, 0);

        public bool IncrementSharedVariable()
        {
            var currentValue = sharedVariable.Synchron;
            sharedVariable.Synchron = (short)(currentValue + 1);
            return true;
        }

        [Test, Order((int)eTcoRemoteTaskTests.InvokeInitializedCSharpMethod)]
        public void InvokeInitializedCSharpMethod()
        {
            //Arrange
            tc._sut._invokeInitializedCSharpMethod.Initialize(IncrementSharedVariable);
            sharedVariable.Synchron = 0;
            //Act
            tc.ExecuteProbeRun(
                (int)eTcoRemoteTaskTests.InvokeInitializedCSharpMethod,
                () => tc._invokeInitializedCSharpMethodDone.Synchron);
            //Assert
            Assert.AreEqual(1, sharedVariable.Synchron);
        }


        [Test, Order((int)eTcoRemoteTaskTests.ExceptionInMethodWillResultInException)]
        public void ExceptionInMethodWillResultInException()
        {
            //Arrange
            tc._sut._exceptionInMethodWillResultInException.Initialize(ExceptionMethod);
            sharedVariable.Synchron = 0;
            //Act
            tc.ExecuteProbeRun(
                (int)eTcoRemoteTaskTests.ExceptionInMethodWillResultInException,
                () => tc._exceptionInMethodWillResultInExceptionDone.Synchron);
            //Assert
            Assert.AreEqual(0, sharedVariable.Synchron);
        }

        public void ExceptionMethod() => throw new System.Exception("An exception thrown from tests");

    }
}
