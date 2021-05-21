namespace Tc.Prober.RecorderTests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using PlcTcProberTests;
    using System.IO;
    using Vortex.Connector;
    using System.Reflection;
    using Tc.Prober.Recorder;
    using Vortex.Adapters.Connector.Tc3.Adapter;

    [TestFixture()]
    public class RunnerTests
    {
        private PlcTcProberTestsTwinController  connector = null;
        private string _runner_recording_file;
        private string _runner_with_test_method_file;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {            
            Runner.RecordingsShell = Path.GetFullPath(Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, @"..\..\..\output\shell\"));            
            connector = new PlcTcProberTestsTwinController(Tc3ConnectorAdapter.Create(null, 851, false));
            connector.Connector.BuildAndStart().ReadWriteCycleDelay = 10;
        }

        [Test()]
        public void RunTest()
        {           
            //-- Act
            int iterationCount = 0;
            int openCalsCount = 0;
            int closeCallCount = 0;
            connector.MAIN.Run(() => true, 
                               () => { iterationCount++; return iterationCount >= 10; }, 
                               () => openCalsCount++, 
                               () => closeCallCount++);

            //-- Assert
            Assert.AreEqual(10, iterationCount);           
            Assert.AreEqual(9, openCalsCount);
            Assert.AreEqual(9, closeCallCount);

        }

        [Test()]
        [Order(100)]
        public void RunWithRecordingTest()
        {
           
            //-- Act
            int iterationCount = 0;
            int openCalsCount = 0;
            int closeCallCount = 0;
            connector.MAIN.Run(() => true,
                             () => { iterationCount++; return iterationCount >= 10; },                          
                             () => openCalsCount++, 
                             () => closeCallCount++,                            
                             new Recorder<fbInheritanceLevel_5, PlainfbInheritanceLevel_5>(connector.MAIN.InheritanceRw, RecorderModeEnum.Graver, 0).Actor,
                             Recorder.Runner.GetRecordingFilePathWithMethodName(2));

            //-- Assert
            Assert.AreEqual(10, iterationCount);
            Assert.AreEqual(9, openCalsCount);
            Assert.AreEqual(9, closeCallCount);

        }


        [Test()]
        [Order(200)]
        public void RecordTestStructureTest()
        {            
            var sut = connector.Tests._recorderRunnerTests;
            // Recorder in 'recording' mode == RecorderModeEnum.Graver
            var recorder = new Recorder.Recorder<stRecorder, PlainstRecorder>(sut._recorder, RecorderModeEnum.Graver);
            sut._recorder.counter.Synchron = 0;

            var count = 0;

            sut.Run(() => sut.RunWithRecorder(), // Actual testing method.
                    () => 
                    {
                        Assert.AreEqual(count++, sut._recorder.counter.Synchron);
                        sut._recorder.counter.Synchron++;   // this line changes the state of plc variable for simulation                       
                        return sut._recorder.counter.Synchron > 100; 
                    },
                    null, 
                    null,
                    recorder.Actor,
                    Path.Combine(Runner.RecordingsShell, Runner.GetRecordingFilePathWithMethodName())
                    );

        }

        [Test()]
        [Order(300)]
        public void PlayTestStructureTest()
        {
            var sut = connector.Tests._recorderRunnerTests;
            var recorder = new Recorder.Recorder<stRecorder, PlainstRecorder>(sut._recorder, RecorderModeEnum.Player);
            sut._recorder.counter.Synchron = 0;

            var count = 0;

            sut.Run(() => sut.RunWithRecorder(),
                    () =>
                    {
                        Assert.AreEqual(count++, sut._recorder.counter.Synchron);
                        // sut._recorder.counter.Synchron++;
                        return sut._recorder.counter.Synchron > 100;
                    },                    
                    null,
                    null,
                    recorder.Actor,
                    Path.Combine(Runner.RecordingsShell, $"{nameof(RecordTestStructureTest)}.json")
                    );
        }

        [Test()]
        [Order(200)]
        public void RecordAndReplayTest()
        {
            var sut = connector.Tests._recorderRunnerTests;
            IRecorder actor;

            // We run with recording

            //-- Arrange
            sut._recorder.counter.Synchron = 0;
            var count = 0;

            // Actor is recorder-graver
            actor = new Recorder.Recorder<stRecorder, PlainstRecorder>(sut._recorder, RecorderModeEnum.Graver).Actor;

            sut.Run(() => sut.RunWithRecorder(), // Actual testing method.
                    () =>
                    {                        
                        Assert.AreEqual(count++, sut._recorder.counter.Synchron);
                        sut._recorder.counter.Synchron++;        // this line changes the state of plc variable for simulation                       
                        return sut._recorder.counter.Synchron > 100;
                    },
                    null,
                    null,
                    actor,
                    Path.Combine(Runner.RecordingsShell, $"{nameof(RecordAndReplayTest)}.json")
                    );




            // We run the same code with re-play. 

            // Actor is player
            actor = new Recorder.Recorder<stRecorder, PlainstRecorder>(sut._recorder, RecorderModeEnum.Player).Actor;

            //-- Arrange
            sut._recorder.counter.Synchron = 0;
            count = 0;


            sut.Run(() => sut.RunWithRecorder(), // Actual testing method.
                   () =>
                   {
                       Assert.AreEqual(count++, sut._recorder.counter.Synchron);
                       // sut._recorder.counter.Synchron++;        // this line changes the state of plc variable for simulation commented out in replay.                      
                        return sut._recorder.counter.Synchron > 100;
                   },
                   null,
                   null,
                   actor,
                   Path.Combine(Runner.RecordingsShell, $"{nameof(RecordAndReplayTest)}.json")
                   );

        }
    }
}