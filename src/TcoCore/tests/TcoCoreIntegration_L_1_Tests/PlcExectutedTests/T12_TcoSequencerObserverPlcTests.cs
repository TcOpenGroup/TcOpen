using System;
using System.Threading;
using NUnit.Framework;
using TcoCore;
using TcoCoreTests;

namespace TcoCoreUnitTests.PlcExecutedTests
{
    public class T12_TcoSequencerObserverPlcTests
    {
        TcoSequenceObserverTests tc = ConnectorFixture.Connector.MAIN._tcoSequenceObserverTests;

        [OneTimeSetUp]
        public void OneTimeSetUp() { }

        [OneTimeTearDown]
        public void OneTimeTearDown() { }

        [SetUp]
        public void Setup() { }

        [TearDown]
        public void TearDown() { }

        [Test]
        [Order((int)eTcoSequenceObserverTests.RunSequenceWithObserverTest)]
        public void RunSequenceWithObserverTest()
        {
            tc.ExecuteProbeRun(1, (int)eTcoSequenceObserverTests.RestoreSequencer);

            tc.ExecuteProbeRun((int)eTcoSequenceObserverTests.RunSequenceWithObserverTest);

            var observer = tc._sObserver.GetPlainFromOnline<PlainTcoSequencerObserver>();

            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(((i + 1) * 100).ToString(), observer._steps[i].Description);
                Assert.AreEqual(((i + 1) * 100), observer._steps[i].ID);
                if (i < 4)
                {
                    Assert.AreEqual(
                        new TimeSpan(0, 0, 0, 0, ((i + 1) * 10)),
                        observer._steps[i].Duration
                    );
                }

                //Console.WriteLine(observer._steps[i].Duration);
            }
        }

        [Test]
        [Order((int)eTcoSequenceObserverTests.RunSequenceWithoutObserverTest)]
        public void RunSequenceWithoutObserverTest()
        {
            tc.ExecuteProbeRun(1, (int)eTcoSequenceObserverTests.RestoreSequencer);

            tc.ExecuteProbeRun((int)eTcoSequenceObserverTests.RunSequenceWithoutObserverTest);

            var observer = tc._sObserver.GetPlainFromOnline<PlainTcoSequencerObserver>();

            for (int i = 1; i < 5; i++)
            {
                Assert.AreEqual(string.Empty, observer._steps[i].Description);
                Assert.AreEqual(0, observer._steps[i].ID);
                Assert.AreEqual(new TimeSpan(), observer._steps[i].Duration);
            }
        }
    }
}
