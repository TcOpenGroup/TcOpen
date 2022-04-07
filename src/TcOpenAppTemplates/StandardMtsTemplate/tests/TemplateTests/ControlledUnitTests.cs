using MainPlc;
using MainPlcConnector;
using NUnit.Framework;
using System;
using System.Linq;
using TcoCore;
using TcOpen.Inxton.RavenDb;

namespace TemplateTests
{
    public class ControlledUnitTests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Entry.Plc.Connector.BuildAndStart();

            var ProcessSettningsRepoSettings = new RavenDbRepositorySettings<PlainProcessData>(new string[] { @"http://localhost:8080" }, "ProcessSettings", "", "");
            var ProcessSettingsRepository = new RavenDbRepository<PlainProcessData>(ProcessSettningsRepoSettings);
            ProcessSettingsRepository.OnCreate = (id, data) => { data._Created = DateTime.Now; data._Modified = DateTime.Now; data.qlikId = id; };
            ProcessSettingsRepository.OnUpdate = (id, data) => { data._Modified = DateTime.Now; };
            Entry.Plc.MAIN._technology._processSettings.InitializeRepository(ProcessSettingsRepository);
            //Entry.Plc.MAIN._technology._processSettings.InitializeRemoteDataExchange(ProcessSettingsRepository);


            var TraceabilityRepoSettings = new RavenDbRepositorySettings<PlainProcessData>(new string[] { @"http://localhost:8080" }, "Traceability", "", "");
            var TraceabilityRepository = new RavenDbRepository<PlainProcessData>(TraceabilityRepoSettings);
            TraceabilityRepository.OnCreate = (id, data) => { data._Created = DateTime.Now; data._Modified = DateTime.Now; data.qlikId = id; };
            TraceabilityRepository.OnUpdate = (id, data) => { data._Modified = DateTime.Now; };
            Entry.Plc.MAIN._technology._cu00x._processData.InitializeRepository(TraceabilityRepository);
            //Entry.Plc.MAIN._technology._cu00x._processData.InitializeRemoteDataExchange(TraceabilityRepository);
        }

        [SetUp]
        public void SetUp()
        {
            Entry.Plc.MAIN._technology._cu00x._automatTask._dataLoadProcessSettings.Synchron = false;
            Entry.Plc.MAIN._technology._cu00x._automatTask._dataCreateNew.Synchron = false;
            Entry.Plc.MAIN._technology._cu00x._automatTask._dataOpen.Synchron = false;
            Entry.Plc.MAIN._technology._cu00x._automatTask._dataClose.Synchron = false;
            Entry.Plc.MAIN._technology._cu00x._automatTask._dataFinalize.Synchron = false;
            Entry.Plc.MAIN._technology._cu00x._automatTask._continueRestore.Synchron = false;
            Entry.Plc.MAIN._technology._cu00x._automatTask._loop.Synchron = false;
        }

        [Test]
        [Timeout(5000)]
        public void run_manual_mode()
        {                        
            var cu = Entry.Plc.MAIN._technology._cu00x;
            cu._manualTask.Execute();

            Assert.AreEqual(eTaskState.Ready, (eTaskState)cu._groundTask._task._taskState.Synchron);
            Assert.AreEqual(eTaskState.Ready, (eTaskState)cu._automatTask._task._taskState.Synchron);
        }

        [Test]
        [Timeout(5000)]
        public void run_ground_mode()
        {
            var cu = Entry.Plc.MAIN._technology._cu00x;
            cu._manualTask.Execute(); // Reset other tasks
            cu._groundTask._task.Execute();

           

            Assert.AreEqual(eTaskState.Ready, (eTaskState)cu._manualTask._taskState.Synchron);
            Assert.AreEqual(eTaskState.Ready, (eTaskState)cu._automatTask._task._taskState.Synchron);
            Assert.AreEqual(eTaskState.Busy, (eTaskState)cu._groundTask._task._taskState.Synchron);
            while ((eTaskState)cu._groundTask._task._taskState.Synchron == eTaskState.Busy) ;
            while ((eTaskState)cu._groundTask._task._taskState.Synchron != eTaskState.Done) ;
            Assert.AreEqual(eTaskState.Done, (eTaskState)cu._groundTask._task._taskState.Synchron);

        }

        [Test]
        [Timeout(5000)]
        public void run_automat_mode_ground_not_done()
        {
            var cu = Entry.Plc.MAIN._technology._cu00x;
            cu._manualTask.Execute(); // Reset other tasks

            cu._automatTask._task.Execute();

            System.Threading.Thread.Sleep(250);
            
            Assert.AreEqual(eTaskState.Ready, (eTaskState)cu._automatTask._task._taskState.Synchron);
        }

        [Test]
        [Timeout(5000)]
        public void run_automat_mode_ground_done()
        {
            var cu = Entry.Plc.MAIN._technology._cu00x;
            cu._manualTask.Execute(); // Reset other tasks

            while ((eTaskState)cu._manualTask._taskState.Synchron != eTaskState.Busy);

            cu._groundTask._task.Execute();

            while ((eTaskState)cu._groundTask._task._taskState.Synchron != eTaskState.Done) ;

            cu._automatTask._task.Execute();
           
            Assert.AreEqual(eTaskState.Busy, (eTaskState)cu._automatTask._task._taskState.Synchron);
        }

        [Test]
        [Timeout(5000)]
        public void run_automat_mode_load_process_data()
        {
            var rec = Entry.Plc.MAIN._technology._processSettings.GetRepository<PlainProcessData>().Read("default");
            var data = Entry.Plc.MAIN._technology._processSettings._data;
            data._EntityId.Synchron = "default";
            var cu = Entry.Plc.MAIN._technology._cu00x;
            var automat = cu._automatTask;
            var cuData = cu._processData._data;
            cu._manualTask.Execute(); // Reset other tasks

            while ((eTaskState)cu._manualTask._taskState.Synchron != eTaskState.Busy) ;

            cu._groundTask._task.Execute();

            while ((eTaskState)cu._groundTask._task._taskState.Synchron != eTaskState.Done) ;

            automat._dataLoadProcessSettings.Synchron = true;

            automat._task.Execute();

            while (cu._automatTask._currentStep.ID.Synchron != 32766) ;

            Assert.AreEqual(rec._EntityId, cuData.EntityHeader.Reciepe.Synchron);
            Assert.AreEqual(TcoInspectors.eOverallResult.NoAction, (TcoInspectors.eOverallResult)cuData.EntityHeader.Results.Result.Synchron);
#if NET5_0_OR_GREATER
            Assert.AreEqual(rec._Created.ToString().Substring(0, 19), cuData.EntityHeader.ReciepeCreated.Synchron.AddHours(-1).ToString().Substring(0, 19));
            Assert.AreEqual(rec._Modified.ToString().Substring(0, 19), cuData.EntityHeader.ReciepeLastModified.Synchron.AddHours(-1).ToString().Substring(0, 19));
#else
            Assert.AreEqual(rec._Created.ToString().Substring(0, 19), cuData.EntityHeader.ReciepeCreated.Synchron.ToString().Substring(0, 19));
            Assert.AreEqual(rec._Modified.ToString().Substring(0, 19), cuData.EntityHeader.ReciepeLastModified.Synchron.ToString().Substring(0, 19));
#endif
        }

        [Test]
        [Timeout(5000)]
        public void run_automat_mode_load_create_new_entity()
        {            
            var rec = Entry.Plc.MAIN._technology._processSettings.GetRepository<PlainProcessData>().Read("default");
            var data = Entry.Plc.MAIN._technology._processSettings._data;
            data._EntityId.Synchron = "default";
            var cu = Entry.Plc.MAIN._technology._cu00x;
            var automat = cu._automatTask;
            var cuData = cu._processData._data;

            automat._dataLoadProcessSettings.Synchron = true;
            automat._dataCreateNew.Synchron = true;

            cu._manualTask.Execute(); // Reset other tasks

            while ((eTaskState)cu._manualTask._taskState.Synchron != eTaskState.Busy);

            cu._groundTask._task.Execute();

            while ((eTaskState)cu._groundTask._task._taskState.Synchron != eTaskState.Done);

            cu._automatTask._task.Execute();

            while (cu._automatTask._currentStep.ID.Synchron != 32766);

            Assert.AreEqual(rec._EntityId, cuData.EntityHeader.Reciepe.Synchron);
            Console.WriteLine(rec._Created.ToString().Substring(0, 19));
            Console.WriteLine(cuData.EntityHeader.ReciepeCreated.Synchron.ToString().Substring(0, 19));
#if NET5_0_OR_GREATER
            Assert.AreEqual(rec._Created.ToString().Substring(0, 19), cuData.EntityHeader.ReciepeCreated.Synchron.AddHours(-1).ToString().Substring(0, 19));
            Assert.AreEqual(rec._Modified.ToString().Substring(0, 19), cuData.EntityHeader.ReciepeLastModified.Synchron.AddHours(-1).ToString().Substring(0, 19));
#else
            Assert.AreEqual(rec._Created.ToString().Substring(0, 19), cuData.EntityHeader.ReciepeCreated.Synchron.ToString().Substring(0, 19));
            Assert.AreEqual(rec._Modified.ToString().Substring(0, 19), cuData.EntityHeader.ReciepeLastModified.Synchron.ToString().Substring(0, 19));
#endif
        }


        [Test]
        [Timeout(5000)]
        public void run_automat_mode_load_open_entity()
        {
            var rec = Entry.Plc.MAIN._technology._processSettings.GetRepository<PlainProcessData>().Read("default");
            var data = Entry.Plc.MAIN._technology._processSettings._data;
            data._EntityId.Synchron = "default";
            var cu = Entry.Plc.MAIN._technology._cu00x;
            var automat = cu._automatTask;
            var cuData = cu._processData._data;

            automat._dataLoadProcessSettings.Synchron = true;
            automat._dataCreateNew.Synchron = true;
            automat._dataOpen.Synchron = true;

            cu._manualTask.Execute(); // Reset other tasks

            while ((eTaskState)cu._manualTask._taskState.Synchron != eTaskState.Busy) ;

            cu._groundTask._task.Execute();

            while ((eTaskState)cu._groundTask._task._taskState.Synchron != eTaskState.Done) ;

            cu._automatTask._task.Execute();

            while (cu._automatTask._currentStep.ID.Synchron != 32766) ;

            Assert.AreEqual(rec._EntityId, cuData.EntityHeader.Reciepe.Synchron);
            Assert.AreEqual(TcoInspectors.eOverallResult.InProgress, (TcoInspectors.eOverallResult)cuData.EntityHeader.Results.Result.Synchron);
            Assert.AreEqual(rec.CU00x.Header.NextOnFailed, cuData.EntityHeader.NextStation.Synchron);
            Assert.AreEqual(TcoInspectors.eOverallResult.InProgress, (TcoInspectors.eOverallResult)cuData.EntityHeader.Results.Result.Synchron);
            Assert.AreEqual(MainPlc.eStations.CU00x, (MainPlc.eStations)cuData.EntityHeader.OpenOn.Synchron);
            Assert.AreEqual(false, cuData.EntityHeader.WasReset.Synchron);
        }

        [Test]
        [Timeout(5000)]
        public void run_automat_mode_load_open_close_entity()
        {
            var rec = Entry.Plc.MAIN._technology._processSettings.GetRepository<PlainProcessData>().Read("default");
            var data = Entry.Plc.MAIN._technology._processSettings._data;
            data._EntityId.Synchron = "default";
            var cu = Entry.Plc.MAIN._technology._cu00x;
            var automat = cu._automatTask;
            var cuData = cu._processData._data;

            automat._dataLoadProcessSettings.Synchron = true;
            automat._dataCreateNew.Synchron = true;
            automat._dataOpen.Synchron = true;
            automat._dataClose.Synchron = true;
            automat._continueRestore.Synchron = true;

            cu._manualTask.Execute(); // Reset other tasks

            while ((eTaskState)cu._manualTask._taskState.Synchron != eTaskState.Busy) ;

            cu._groundTask._task.Execute();

            while ((eTaskState)cu._groundTask._task._taskState.Synchron != eTaskState.Done) ;

            cu._automatTask._task.Execute();

            while (cu._automatTask._currentStep.ID.Synchron != 10000) ;

            Assert.AreEqual(rec._EntityId, cuData.EntityHeader.Reciepe.Synchron);
            Assert.AreEqual(TcoInspectors.eOverallResult.InProgress, (TcoInspectors.eOverallResult)cuData.EntityHeader.Results.Result.Synchron);
            Assert.AreEqual(rec.CU00x.Header.NextOnFailed, cuData.EntityHeader.NextStation.Synchron);
            Assert.AreEqual(TcoInspectors.eOverallResult.InProgress, (TcoInspectors.eOverallResult)cuData.EntityHeader.Results.Result.Synchron);
            Assert.AreEqual(MainPlc.eStations.NONE, (MainPlc.eStations)cuData.EntityHeader.OpenOn.Synchron);
            Assert.AreEqual(false, cuData.EntityHeader.WasReset.Synchron);
        }


        [Test]
        [Timeout(5000)]
        public void run_automat_mode_load_open_finalize_entity()
        {
            var rec = Entry.Plc.MAIN._technology._processSettings.GetRepository<PlainProcessData>().Read("default");
            var data = Entry.Plc.MAIN._technology._processSettings._data;
            data._EntityId.Synchron = "default";
            var cu = Entry.Plc.MAIN._technology._cu00x;
            var automat = cu._automatTask;
            var cuData = cu._processData._data;

            automat._dataLoadProcessSettings.Synchron = true;
            automat._dataCreateNew.Synchron = true;
            automat._dataOpen.Synchron = true;
            automat._dataFinalize.Synchron = true;
            automat._continueRestore.Synchron = true;

            cu._manualTask.Execute(); // Reset other tasks

            while ((eTaskState)cu._manualTask._taskState.Synchron != eTaskState.Busy) ;

            cu._groundTask._task.Execute();

            while ((eTaskState)cu._groundTask._task._taskState.Synchron != eTaskState.Done) ;

            cu._automatTask._task.Execute();

            while (cu._automatTask._currentStep.ID.Synchron != 10000) ;

            Assert.AreEqual(rec._EntityId, cuData.EntityHeader.Reciepe.Synchron);
            Assert.AreEqual(TcoInspectors.eOverallResult.Passed, (TcoInspectors.eOverallResult)cuData.EntityHeader.Results.Result.Synchron);
            Assert.AreEqual(rec.CU00x.Header.NextOnFailed, cuData.EntityHeader.NextStation.Synchron);
            Assert.AreEqual(TcoInspectors.eOverallResult.Passed, (TcoInspectors.eOverallResult)cuData.EntityHeader.Results.Result.Synchron);
            Assert.AreEqual(MainPlc.eStations.NONE, (MainPlc.eStations)cuData.EntityHeader.OpenOn.Synchron);
            Assert.AreEqual(false, cuData.EntityHeader.WasReset.Synchron);
        }
    }
}