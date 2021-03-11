using NUnit.Framework;
using System;
using System.Text;
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

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string localAmsId = TwinCAT.Ads.AmsNetId.Local.ToString();
            tc_A.SetSynchAmsId(localAmsId);
            tc_B.SetSynchAmsId(localAmsId);

            tc_A._CallMyPlcInstance.Synchron = true;            //Switch on the cyclical execution of the _TcoContextTest_A instance 
            tc_B._CallMyPlcInstance.Synchron = true;            //Switch on the cyclical execution of the _TcoContextTest_B instance 

            while(!tc_A.IsRtcValid() |!tc_B.IsRtcValid())
            {

            }


            tc_A._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 
            tc_B._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_B instance 

            tc_A.RunUntilEndConditionIsMet(() =>                //Calling of the Open() method before the code inside the parentheses is ensured be the TestRunner RunUntilEndConditionIsMet            
            {
                tc_A.CallMainFromUnitTest();                    //Calling only the Main() method
            }, () => tc_A.IsRtcValid());                        //Calling of the Close() method after the code inside the parentheses is ensured be the TestRunner RunUntilEndConditionIsMet    

            tc_B.RunUntilEndConditionIsMet(() =>                //Calling of the Open() method before the code inside the parentheses is ensured be the TestRunner RunUntilEndConditionIsMet            
            {
                tc_B.CallMainFromUnitTest();                    //Calling only the Main() method
            }, () => tc_B.IsRtcValid());                        //Calling of the Close() method after the code inside the parentheses is ensured be the TestRunner RunUntilEndConditionIsMet    
        }


        [Test, Order(100)]
        public void T100_Plc_ContextADoesNotAffectContextB()
        {
            tc_A._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 
            tc_B._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_B instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance
            tc_B.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            ulong BstrtCycles_0 = tc_B._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong BmainCycles_0 = tc_B._mainCycles.Synchron;
            ulong BendCycles__0 = tc_B._endCycles.Synchron;

            tc_A._CallMyPlcInstance.Synchron = true;            //Switch on the cyclical execution of the _TcoContextTest_A instance             

            Thread.Sleep(Delay);                                //Time of the cyclical execution of the _TcoContextTest_A instance

            tc_A._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance
            tc_B.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_diff = tc_A._startCycles.Synchron - AstrtCycles_0;//Calculate differencies between initial and actual counters values
            ulong AmainCycles_diff = tc_A._mainCycles.Synchron  - AmainCycles_0;
            ulong AendCycles__diff = tc_A._endCycles.Synchron   - AendCycles__0;

            Assert.Greater(tc_A._startCycles.Synchron , AstrtCycles_0);//All the actual values of the instance A must be greather than the initial values 
            Assert.Greater(tc_A._mainCycles.Synchron  , AmainCycles_0);
            Assert.Greater(tc_A._endCycles.Synchron   , AendCycles__0);

            Assert.AreEqual(AstrtCycles_diff, AmainCycles_diff);//All the values should have the same increment
            Assert.AreEqual(AmainCycles_diff, AendCycles__diff);


            Assert.AreEqual(BstrtCycles_0, tc_B._startCycles.Synchron); //All the actual values of the instance B must be the same than the initial values
            Assert.AreEqual(BmainCycles_0, tc_B._mainCycles.Synchron);
            Assert.AreEqual(BendCycles__0, tc_B._endCycles.Synchron);
        }

        [Test, Order(101)]
        public void T101_Plc_SoAsContextBDoesNotAffectContextA()
        {
            tc_A._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 
            tc_B._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_B instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance
            tc_B.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            ulong BstrtCycles_0 = tc_B._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong BmainCycles_0 = tc_B._mainCycles.Synchron;
            ulong BendCycles__0 = tc_B._endCycles.Synchron;

            tc_B._CallMyPlcInstance.Synchron = true;            //Switch on the cyclical execution of the _TcoContextTest_B instance             

            Thread.Sleep(3 * Delay);                            //Time of the cyclical execution of the _TcoContextTest_B instance

            tc_B._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_B instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance
            tc_B.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong BstrtCycles_diff = tc_B._startCycles.Synchron - BstrtCycles_0;//Calculate differencies between initial and actual counters values
            ulong BmainCycles_diff = tc_B._mainCycles.Synchron - BmainCycles_0;
            ulong BendCycles__diff = tc_B._endCycles.Synchron - BendCycles__0;

            Assert.Greater(tc_B._startCycles.Synchron, BstrtCycles_0);//All the actual values of the instance B must be greather than the initial values 
            Assert.Greater(tc_B._mainCycles.Synchron, BmainCycles_0);
            Assert.Greater(tc_B._endCycles.Synchron, BendCycles__0);

            Assert.AreEqual(BstrtCycles_diff, BmainCycles_diff);//All the values should have the same increment
            Assert.AreEqual(BmainCycles_diff, BendCycles__diff);


            Assert.AreEqual(AstrtCycles_0, tc_A._startCycles.Synchron);//All the actual values of the instance A must be the same than the initial values
            Assert.AreEqual(AmainCycles_0, tc_A._mainCycles.Synchron);
            Assert.AreEqual(AendCycles__0, tc_A._endCycles.Synchron);
        }

        [Test, Order(102)]
        public void T102_NoPlcLogicIsRunning()
        {
            tc_A._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 
            tc_B._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_B instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance
            tc_B.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            ulong BstrtCycles_0 = tc_B._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong BmainCycles_0 = tc_B._mainCycles.Synchron;
            ulong BendCycles__0 = tc_B._endCycles.Synchron;


            Thread.Sleep(Delay);                                //During this time, no PLC logic should be executed

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance
            tc_B.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            Assert.AreEqual(AstrtCycles_0, tc_A._startCycles.Synchron);//All the actual values of the instance A must be the same than the initial values
            Assert.AreEqual(AmainCycles_0, tc_A._mainCycles.Synchron);
            Assert.AreEqual(AendCycles__0, tc_A._endCycles.Synchron);

            Assert.AreEqual(BstrtCycles_0, tc_B._startCycles.Synchron);//All the actual values of the instance B must be the same than the initial values
            Assert.AreEqual(BmainCycles_0, tc_B._mainCycles.Synchron);
            Assert.AreEqual(BendCycles__0, tc_B._endCycles.Synchron);
        }

        [Test, Order(103)]
        public void T103_ContextDotMainCall()
        {
            tc_A._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            tc_A.CallMainFromUnitTest();                        //Calling only the Main() method of the TcoContext test instance

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            Assert.AreEqual(AstrtCycles_0, tc_A._startCycles.Synchron);//Should be the same as before, as only Main() method is called. As neither the Open() nor the Close() methods are called, neither the _startCycleCount, not the _endCycleCount are incremented
            Assert.AreEqual(AmainCycles_0 + 1, tc_A._mainCycles.Synchron);//Only cycle counter of the Main() method should be incremented
            Assert.AreEqual(AendCycles__0 , tc_A._endCycles.Synchron);//Should be the same as before, as only Main() method is called. As neither the Open() nor the Close() methods are called, neither the _startCycleCount, not the _endCycleCount are incremented
        }

        [Test, Order(104)]
        public void T104_ContextDotRunCall()
        {
            tc_A._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            tc_A.CallRunFromUnitTest();                         //Calling the Run() method of the TcoContext test instance. This method could call internaly Open(), Main() and Close() in the exact order as they are written.

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            Assert.AreEqual(AstrtCycles_0 + 1, tc_A._startCycles.Synchron);//_startCycleCount should be also incremented, as by calling the Run() method, also the Open() method should be called internally
            Assert.AreEqual(AmainCycles_0 + 1, tc_A._mainCycles.Synchron);//cycle counter of the Main() method should be also incremented, as by calling the Run() method, also the Main() method should be called internally
            Assert.AreEqual(AendCycles__0 + 1, tc_A._endCycles.Synchron);//_endCycleCount should be also incremented, as by calling the Run() method, also the Close() method should be called internally
        }

        [Test, Order(105)]
        public void T105_MultipleContextDotMainCallUsingTestRunner()
        {
            ushort cycles = 10;
            ushort i = 0;

            tc_A._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            ulong AstrtCycles_0 = tc_A._startCycles.Synchron;   //Store the actual cycle counters values as the initial values
            ulong AmainCycles_0 = tc_A._mainCycles.Synchron;
            ulong AendCycles__0 = tc_A._endCycles.Synchron;

            tc_A.RunUntilEndConditionIsMet(() =>                //Calling of the Open() method before the code inside the parentheses is ensured be the TestRunner RunUntilEndConditionIsMet            
            {
                tc_A.CallMainFromUnitTest();                    //Calling only the Main() method
                i++;
            },() => i >= cycles);                               //Calling of the Close() method after the code inside the parentheses is ensured be the TestRunner RunUntilEndConditionIsMet    

            tc_A.ReadOutCycleCounters();                        //Read out actual cycle counters values into the test instance

            Assert.AreEqual(AstrtCycles_0 + cycles, tc_A._startCycles.Synchron);//_startCycleCount should be also incremented, as by calling the TestRunner RunUntilEndConditionIsMet, also the Open() method should be called explicitly
            Assert.AreEqual(AmainCycles_0 + cycles, tc_A._mainCycles.Synchron);//cycle counter of the Main() method should be also incremented, as the Main() method is called explixitly.
            Assert.AreEqual(AendCycles__0 + cycles, tc_A._endCycles.Synchron);//_endCycleCount should be also incremented, as by calling the TestRunner RunUntilEndConditionIsMet, also the Close() method should be called explicitly
        }

        [Test, Order(106)]
        public void T106_RtcValid()
        {
            tc_A._CallMyPlcInstance.Synchron = true;            //Switch on the cyclical execution of the _TcoContextTest_A instance             

            Thread.Sleep(Delay);                                //Time of the cyclical execution of the _TcoContextTest_A instance

            tc_A._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 

            Assert.IsTrue(tc_A.IsRtcValid());                   //System time should already been read out and valid, as PLC instance was running at least Delay time.
        }

        [Test, Order(107)]
        public void T107_RtcNowChanging()
        {

            Delay = 5000;                                       //Set delay to 5000ms
            tc_A._CallMyPlcInstance.Synchron = true;            //Switch on the cyclical execution of the _TcoContextTest_A instance       
            tc_A.Now();
            DateTime _startPLCTime = tc_A._rtcNow.Synchron;     //Readout actual time of the instance (number of seconds from 1.1.1970).
            DateTime _startDotNotTime = DateTime.Now;
            Thread.Sleep(Delay);                                //Time of the cyclical execution of the task instance

            tc_A._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 

            tc_A.Now();
            DateTime _endPLCTime = tc_A._rtcNow.Synchron;                     //Readout actual time of the instance (number of seconds from 1.1.1970). Does not matter that .Net System.DateTime starts in 1.1.0001
            DateTime _endDotNotTime = DateTime.Now;

            TimeSpan _diffPLC = _endPLCTime - _startPLCTime;
            Assert.AreEqual(Delay, _diffPLC.TotalMilliseconds);            //Check if time difference is equal. As Thread.Sleep() method's input parameter is in miliseconds and tc_A.Now() is in seconds, _diff must be multiplied by 1000.

            TimeSpan _startTimeDiff = _startDotNotTime - _startPLCTime;
            TimeSpan _endTimeDiff = _endDotNotTime - _endPLCTime;

            TimeSpan _tolerance = new TimeSpan(0, 0, 0, 1);     //Tollerance of the difference is 1s, as PLC time has resolution in seconds.
            Assert.LessOrEqual(_startTimeDiff, _tolerance);
            Assert.LessOrEqual(_endTimeDiff, _tolerance);
        }


        [Test, Order(109)]
        public void T109_RtcNowHiPrecisionChanging()
        {
            string _sStartTime = "";
            Delay = 5000;                                        //Set delay to 500ms
            tc_A._CallMyPlcInstance.Synchron = true;            //Switch on the cyclical execution of the _TcoContextTest_A instance             
                if (_sStartTime == "")
            {
                //Readout actual time of the instance (number of hundreds of nanoseconds from 1.1.1970).
                _sStartTime = new StringBuilder(tc_A.NowHiPrecision()) { [10] = 'T' }.ToString();             
            }

            Thread.Sleep(Delay);                                //Time of the cyclical execution of the task instance

            tc_A._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 

            //Readout actual time of the instance (number of hundreds of nanoseconds from 1.1.1970).
            string _sEndTime = new StringBuilder(tc_A.NowHiPrecision()) { [10] = 'T'}.ToString();

            DateTime _dtStartTime;
            DateTime _dtEndTime;
            DateTime.TryParse(_sStartTime, out _dtStartTime);
            DateTime.TryParse(_sEndTime, out _dtEndTime);
            TimeSpan _diff = _dtEndTime - _dtStartTime;
            Assert.GreaterOrEqual(_diff.TotalMilliseconds , Delay * 0.9);
            Assert.LessOrEqual(_diff.TotalMilliseconds , Delay * 1.1);
        }

        [Test, Order(110)]
        public void T110_RtcNowHiPrecision()
        {

            tc_A._CallMyPlcInstance.Synchron = true;            //Switch on the cyclical execution of the _TcoContextTest_A instance             
            //Readout actual time of the instance (number of hundreds of nanoseconds from 1.1.1970).
            string _splcTime = new StringBuilder(tc_A.NowHiPrecision()) { [10] = 'T' }.ToString();
            tc_A._CallMyPlcInstance.Synchron = false;           //Switch off the cyclical execution of the _TcoContextTest_A instance 
            DateTime _dtplcTime;
            DateTime.TryParse(_splcTime, out _dtplcTime);
            DateTime _dotNetTime = DateTime.Now;
            TimeSpan _diff = _dotNetTime - _dtplcTime;
            Assert.GreaterOrEqual(_diff.TotalMilliseconds, Delay * 0.9);
            Assert.LessOrEqual(_diff.TotalMilliseconds, Delay * 1.1);
        }


    }
}