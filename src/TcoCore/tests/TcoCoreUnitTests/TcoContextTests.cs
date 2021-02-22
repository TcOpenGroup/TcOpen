using NUnit.Framework;
using System.Threading;

namespace TcoCoreUnitTests
{

    public class T01_TcoContextTests
    {

        TcoCoreTests.TcoContextTest tc_A = TcoCoreUnitTests.ConnectorFixture.Connector.MAIN._TcoContextTest_A;
        TcoCoreTests.TcoContextTest tc_B = TcoCoreUnitTests.ConnectorFixture.Connector.MAIN._TcoContextTest_B;
        int Delay = 500;

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(100)]
        public void T100_Plc_ContextADoesNotAffectContextB()
        {
            tc_A._CallMyPlcInstance.Synchron = false;
            tc_B._CallMyPlcInstance.Synchron = false;

            tc_A.ReadOutCycleCounters();
            tc_B.ReadOutCycleCounters();

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            ulong BstrtCycles_0 = tc_B._startCycles.Synchron;
            ulong BmainCycles_0 = tc_B._mainCycles.Synchron;
            ulong BendCycles__0 = tc_B._endCycles.Synchron;

            tc_A._CallMyPlcInstance.Synchron = true;

            Thread.Sleep(Delay);

            tc_A._CallMyPlcInstance.Synchron = false;

            tc_A.ReadOutCycleCounters();
            tc_B.ReadOutCycleCounters();

            ulong AstrtCycles_diff = tc_A._startCycles.Synchron - AstrtCycles_0;
            ulong AmainCycles_diff = tc_A._mainCycles.Synchron  - AmainCycles_0;
            ulong AendCycles__diff = tc_A._endCycles.Synchron   - AendCycles__0;

            Assert.Greater(tc_A._startCycles.Synchron , AstrtCycles_0);
            Assert.Greater(tc_A._mainCycles.Synchron  , AmainCycles_0);
            Assert.Greater(tc_A._endCycles.Synchron   , AendCycles__0);

            Assert.AreEqual(AstrtCycles_diff, AmainCycles_diff);
            Assert.AreEqual(AmainCycles_diff, AendCycles__diff);


            Assert.AreEqual(BstrtCycles_0, tc_B._startCycles.Synchron);
            Assert.AreEqual(BmainCycles_0, tc_B._mainCycles.Synchron);
            Assert.AreEqual(BendCycles__0, tc_B._endCycles.Synchron);
        }

        [Test, Order(101)]
        public void T101_Plc_SoAsContextBDoesNotAffectContextA()
        {
            tc_A._CallMyPlcInstance.Synchron = false;
            tc_B._CallMyPlcInstance.Synchron = false;

            tc_A.ReadOutCycleCounters();
            tc_B.ReadOutCycleCounters();

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            ulong BstrtCycles_0 = tc_B._startCycles.Synchron;
            ulong BmainCycles_0 = tc_B._mainCycles.Synchron;
            ulong BendCycles__0 = tc_B._endCycles.Synchron;

            tc_B._CallMyPlcInstance.Synchron = true;

            Thread.Sleep(3 * Delay);

            tc_B._CallMyPlcInstance.Synchron = false;

            tc_A.ReadOutCycleCounters();
            tc_B.ReadOutCycleCounters();

            ulong BstrtCycles_diff = tc_B._startCycles.Synchron - BstrtCycles_0;
            ulong BmainCycles_diff = tc_B._mainCycles.Synchron - BmainCycles_0;
            ulong BendCycles__diff = tc_B._endCycles.Synchron - BendCycles__0;

            Assert.Greater(tc_B._startCycles.Synchron, BstrtCycles_0);
            Assert.Greater(tc_B._mainCycles.Synchron, BmainCycles_0);
            Assert.Greater(tc_B._endCycles.Synchron, BendCycles__0);

            Assert.AreEqual(BstrtCycles_diff, BmainCycles_diff);
            Assert.AreEqual(BmainCycles_diff, BendCycles__diff);


            Assert.AreEqual(AstrtCycles_0, tc_A._startCycles.Synchron);
            Assert.AreEqual(AmainCycles_0, tc_A._mainCycles.Synchron);
            Assert.AreEqual(AendCycles__0, tc_A._endCycles.Synchron);
        }

        [Test, Order(102)]
        public void T102_NoPlcLogicIsRunning()
        {
            tc_A._CallMyPlcInstance.Synchron = false;
            tc_B._CallMyPlcInstance.Synchron = false;

            tc_A.ReadOutCycleCounters();
            tc_B.ReadOutCycleCounters();

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            ulong BstrtCycles_0 = tc_B._startCycles.Synchron;
            ulong BmainCycles_0 = tc_B._mainCycles.Synchron;
            ulong BendCycles__0 = tc_B._endCycles.Synchron;

            Thread.Sleep(Delay);

            tc_A.ReadOutCycleCounters();
            tc_B.ReadOutCycleCounters();

            Assert.AreEqual(AstrtCycles_0, tc_A._startCycles.Synchron);
            Assert.AreEqual(AmainCycles_0, tc_A._mainCycles.Synchron);
            Assert.AreEqual(AendCycles__0, tc_A._endCycles.Synchron);

            Assert.AreEqual(BstrtCycles_0, tc_B._startCycles.Synchron);
            Assert.AreEqual(BmainCycles_0, tc_B._mainCycles.Synchron);
            Assert.AreEqual(BendCycles__0, tc_B._endCycles.Synchron);
        }

        [Test, Order(103)]
        public void T103_ContextDotMainCall()
        {
            tc_A._CallMyPlcInstance.Synchron = false;

            tc_A.ReadOutCycleCounters();

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            tc_A.CallMainFromUnitTest();

            tc_A.ReadOutCycleCounters();

            Assert.AreEqual(AstrtCycles_0, tc_A._startCycles.Synchron);        
            Assert.AreEqual(AmainCycles_0 + 1, tc_A._mainCycles.Synchron);
            Assert.AreEqual(AendCycles__0 , tc_A._endCycles.Synchron);
        }

        [Test, Order(104)]
        public void T104_ContextDotRunCall()
        {
            tc_A._CallMyPlcInstance.Synchron = false;

            tc_A.ReadOutCycleCounters();

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            tc_A.CallRunFromUnitTest();

            tc_A.ReadOutCycleCounters();

            Assert.AreEqual(AstrtCycles_0 + 1, tc_A._startCycles.Synchron);
            Assert.AreEqual(AmainCycles_0 + 1, tc_A._mainCycles.Synchron);
            Assert.AreEqual(AendCycles__0 + 1, tc_A._endCycles.Synchron);
        }

        [Test, Order(105)]
        public void T105_MultipleContextDotMainCallUsingTestRunner()
        {
            ushort cycles = 10;
            ushort i = 0;

            tc_A._CallMyPlcInstance.Synchron = false;

            tc_A.ReadOutCycleCounters();

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            tc_A.RunUntilEndConditionIsMet(() =>
            {
                tc_A.CallMainFromUnitTest();
                i++;
            },() => i >= cycles);

            tc_A.ReadOutCycleCounters();

            Assert.AreEqual(AstrtCycles_0 + 10, tc_A._startCycles.Synchron);
            Assert.AreEqual(AmainCycles_0 + 10, tc_A._mainCycles.Synchron);
            Assert.AreEqual(AendCycles__0 + 10, tc_A._endCycles.Synchron);
        }

    }
}