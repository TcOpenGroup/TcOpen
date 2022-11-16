using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Raven.Embedded;
using TcoInspectors;
using TcOpen.Inxton.Data;
using TcOpen.Inxton.Data.Merge;
using TcOpen.Inxton.RavenDb;

namespace TcoDataMergeTests
{
   
        public class MergeEntitiesDataTest
        {
#if DEBUG
            private const int timeOut = 70000;
#else
    private const int timeOut = 70000;
#endif

            private IRepository<TestData> repositorySource;
            private IRepository<TestData> repositoryTarget;
            List<Type> reqTypes = new List<Type>();
            List<string> reqProperties = new List<string>();

            [OneTimeSetUp]
            public void OneTimeSetUp()
            {

                EmbeddedServer.Instance.StartServer(new ServerOptions
                {
                    DataDirectory = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName, "tmp", "data"),
                    AcceptEula = true,
                    ServerUrl = "http://127.0.0.1:8080",
                });
            }
            [SetUp]
            public void SetUp()
            {

               repositoryTarget = new RavenDbRepository<TestData>(new RavenDbRepositorySettings<TestData>(new string[] { @"http://localhost:8080" }, "TargetData", "", ""));

                repositoryTarget.OnCreate = (id, data) => { data._Created = DateTime.Now; data._Modified = DateTime.Now; };
                repositoryTarget.OnUpdate = (id, data) => { data._Modified = DateTime.Now; };


                //Entry.Plc.MAIN._technology._processSettings.InitializeRepository(repositoryTarget);




                repositorySource = new RavenDbRepository<TestData>(new RavenDbRepositorySettings<TestData>(new string[] { @"http://localhost:8080" }, "SourceData", "", ""));

                repositorySource.OnCreate = (id, data) => { data._Created = DateTime.Now; data._Modified = DateTime.Now; };
                repositorySource.OnUpdate = (id, data) => { data._Modified = DateTime.Now; };

                //Entry.Plc.MAIN._technology._reworkSettings.InitializeRepository(repositorySource);


                var recordsSource = repositorySource.Queryable.Where(p => true).Select(p => p._EntityId).ToList();
                recordsSource.ForEach(p => repositorySource.Delete(p));

                var recordsTarget = repositoryTarget.Queryable.Where(p => true).Select(p => p._EntityId).ToList();
                recordsTarget.ForEach(p => repositoryTarget.Delete(p));

            }




            private void SetupSourceEntity(TestData testDataSource)
            {
                var random = new Random();
                testDataSource.customChecker1.IsByPassed = true;
                testDataSource.customChecker2.IsByPassed = true;
                testDataSource.customChecker3.IsByPassed = true;
                testDataSource.customChecker4.IsByPassed = true;

                testDataSource.customChecker1.IsExcluded = true;
                testDataSource.customChecker2.IsExcluded = true;
                testDataSource.customChecker3.IsExcluded = true;
                testDataSource.customChecker4.IsExcluded = true;

                testDataSource.customChecker1.Result = (short)TcoInspectors.eInspectorResult.NoAction;
                testDataSource.customChecker2.Result = (short)TcoInspectors.eInspectorResult.NoAction;
                testDataSource.customChecker3.Result = (short)TcoInspectors.eInspectorResult.NoAction;
                testDataSource.customChecker4.Result = (short)TcoInspectors.eInspectorResult.NoAction;

                testDataSource.customChecker1.DetectedStatus = 0;
                testDataSource.customChecker2.DetectedStatus = 0;
                testDataSource.customChecker3.DetectedStatus = false;
                testDataSource.customChecker4.DetectedStatus = false;

                testDataSource.customChecker1.NumberOfAllowedRetries = 1;
                testDataSource.customChecker2.NumberOfAllowedRetries = 2;
                testDataSource.customChecker3.NumberOfAllowedRetries = 3;
                testDataSource.customChecker4.NumberOfAllowedRetries = 4;


                testDataSource.customChecker1.RequiredMin = (float)random.NextDouble();
                testDataSource.customChecker2.RequiredMin = (float)random.NextDouble();
                //testDataSource.customChecker3.RequiredMin = (float)random.NextDouble();
                //testDataSource.customChecker4.RequiredMin = (float)random.NextDouble();

                testDataSource.customChecker1.RequiredMax = (float)random.NextDouble();
                testDataSource.customChecker2.RequiredMax = (float)random.NextDouble();
                //testDataSource.customChecker3.RequiredMax = (float)random.NextDouble();
                //testDataSource.customChecker4.RequiredMax = (float)random.NextDouble();

            }

            private void CreateEntities(out TestData testDataSource, out TestData testDataTarget)
            {
                testDataSource = new TestData() { _recordId = default(dynamic), _Created = new DateTime(1904522374), _EntityId = "TestValue1447482512", _Modified = new DateTime(1997083921) };
                repositorySource.Create(testDataSource._EntityId, testDataSource);

                testDataTarget = new TestData() { _recordId = default(dynamic), _Created = new DateTime(1904522374), _EntityId = "TestValue1447482513", _Modified = new DateTime(1997083921) };
                repositoryTarget.Create(testDataTarget._EntityId, testDataTarget);
            }



            [Test]
            public void compare_req_prop_not_in_list()
            {


                reqTypes.Clear();
                reqTypes.Add(typeof(PlainTcoAnalogueInspectorData));

                reqProperties = PropertyHelper.GetPropertiesNames(new PlainTcoAnalogueInspectorData(), p => p.IsByPassed, p => p.IsExcluded, p => p.RequiredMin, p => p.RequiredMax, p => p.NumberOfAllowedRetries);

                TestData testDataSource, testDataTarget;
                CreateEntities(out testDataSource, out testDataTarget);

                SetupSourceEntity(testDataSource);

                var random = new Random();
                testDataTarget.customChecker1.DetectedStatus = (float)random.NextDouble()+1; //plus one means - avoid to zero
                testDataTarget.customChecker2.DetectedStatus = (float)random.NextDouble()+1;
                testDataTarget.customChecker3.DetectedStatus = true;
                testDataTarget.customChecker4.DetectedStatus = true;


                repositorySource.Update(testDataSource._EntityId, testDataSource);

                repositoryTarget.Update(testDataTarget._EntityId, testDataTarget);



                // Act


                var merge = new MergeEntitiesData<TestData>(
                          repositorySource
                        , repositoryTarget
                        , reqTypes
                        , reqProperties
                        , ExclusionNotUsed
                        , InclusionNotUsed
                        );

                merge.Merge(testDataSource._EntityId, testDataTarget._EntityId);

                var mergedTarget = repositoryTarget.Read(testDataTarget._EntityId);



                // Assert
                Assert.AreNotEqual(testDataSource.customChecker1.DetectedStatus.ToString(), mergedTarget.customChecker1.DetectedStatus.ToString());
                Assert.AreNotEqual(testDataSource.customChecker2.DetectedStatus.ToString(), mergedTarget.customChecker2.DetectedStatus.ToString());
                Assert.AreNotEqual(testDataSource.customChecker3.DetectedStatus.ToString(), mergedTarget.customChecker3.DetectedStatus.ToString());
                Assert.AreNotEqual(testDataSource.customChecker4.DetectedStatus.ToString(), mergedTarget.customChecker4.DetectedStatus.ToString());


            }

            [Test]
            public void compare_if_req_prop_is_in_list()
            {

                reqTypes.Clear();
                reqTypes.Add(typeof(PlainTcoAnalogueInspectorData));


                reqProperties = PropertyHelper.GetPropertiesNames(new PlainTcoAnalogueInspectorData(), p => p.IsByPassed, p => p.IsExcluded, p => p.RequiredMin, p => p.RequiredMax, p => p.NumberOfAllowedRetries);


                TestData testDataSource, testDataTarget;
                CreateEntities(out testDataSource, out testDataTarget);

                SetupSourceEntity(testDataSource);

                var random = new Random();
                testDataTarget.customChecker1.IsByPassed = !testDataSource.customChecker1.IsByPassed;
                testDataTarget.customChecker2.IsByPassed = !testDataSource.customChecker2.IsByPassed;
                testDataTarget.customChecker3.IsByPassed = !testDataSource.customChecker3.IsByPassed;
                testDataTarget.customChecker4.IsByPassed = !testDataSource.customChecker4.IsByPassed;



                repositorySource.Update(testDataSource._EntityId, testDataSource);

                repositoryTarget.Update(testDataTarget._EntityId, testDataTarget);



                // Act


                var merge = new MergeEntitiesData<TestData>(
                          repositorySource
                        , repositoryTarget
                        , reqTypes
                        , reqProperties
                        , ExclusionNotUsed
                        , InclusionNotUsed
                        );

                merge.Merge(testDataSource._EntityId, testDataTarget._EntityId);

                var mergedTarget = repositoryTarget.Read(testDataTarget._EntityId);



                // Assert
                Assert.AreEqual(testDataSource.customChecker1.IsByPassed, mergedTarget.customChecker1.IsByPassed);
                Assert.AreEqual(testDataSource.customChecker2.IsByPassed, mergedTarget.customChecker2.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker3.IsByPassed, mergedTarget.customChecker3.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker4.IsByPassed, mergedTarget.customChecker4.IsByPassed);


            }



            [Test]
            public void compare_req_prop_is_in_list_with_exclusion_result_zero()
            {
    
                reqTypes.Add(typeof(PlainTcoAnalogueInspectorData));
                reqTypes.Add(typeof(PlainTcoDigitalInspectorData));



                reqProperties = PropertyHelper.GetPropertiesNames(new PlainTcoAnalogueInspectorData(), p => p.IsByPassed, p => p.IsExcluded, p => p.RequiredMin, p => p.RequiredMax, p => p.NumberOfAllowedRetries);


                TestData testDataSource, testDataTarget;
                CreateEntities(out testDataSource, out testDataTarget);

                SetupSourceEntity(testDataSource);

                var random = new Random();
                testDataTarget.customChecker1.IsByPassed = !testDataSource.customChecker1.IsByPassed;
                testDataTarget.customChecker2.IsByPassed = !testDataSource.customChecker2.IsByPassed;
                testDataTarget.customChecker3.IsByPassed = !testDataSource.customChecker3.IsByPassed;
                testDataTarget.customChecker4.IsByPassed = !testDataSource.customChecker4.IsByPassed;

                testDataTarget.customChecker1.Result = (short)TcoInspectors.eInspectorResult.Excluded;
                testDataTarget.customChecker2.Result = (short)TcoInspectors.eInspectorResult.Excluded;
                testDataTarget.customChecker3.Result = (short)TcoInspectors.eInspectorResult.NoAction;
                testDataTarget.customChecker4.Result = (short)TcoInspectors.eInspectorResult.NoAction;

                repositorySource.Update(testDataSource._EntityId, testDataSource);

                repositoryTarget.Update(testDataTarget._EntityId, testDataTarget);



                // Act


                var merge = new MergeEntitiesData<TestData>(
                          repositorySource
                        , repositoryTarget
                        , reqTypes
                        , reqProperties
                        , ExclusionActiveIfResultIsZero
                        , InclusionNotUsed
                        );

                merge.Merge(testDataSource._EntityId, testDataTarget._EntityId);

                var mergedTarget = repositoryTarget.Read(testDataTarget._EntityId);



                // Assert
                Assert.AreEqual(testDataSource.customChecker1.IsByPassed, mergedTarget.customChecker1.IsByPassed);
                Assert.AreEqual(testDataSource.customChecker1.NumberOfAllowedRetries, mergedTarget.customChecker1.NumberOfAllowedRetries);

                Assert.AreEqual(testDataSource.customChecker2.IsByPassed, mergedTarget.customChecker2.IsByPassed);
                Assert.AreEqual(testDataSource.customChecker2.NumberOfAllowedRetries, mergedTarget.customChecker2.NumberOfAllowedRetries);

                Assert.AreNotEqual(testDataSource.customChecker3.IsByPassed, mergedTarget.customChecker3.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker3.NumberOfAllowedRetries, mergedTarget.customChecker3.NumberOfAllowedRetries);

                Assert.AreNotEqual(testDataSource.customChecker4.IsByPassed, mergedTarget.customChecker4.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker4.NumberOfAllowedRetries, mergedTarget.customChecker4.NumberOfAllowedRetries);


            }

            [Test]
            public void compare_req_prop_additional_req_prop_via_inclusion_excluson_result_zero()
            {
                // Arrange  
                //SetupRepositories();
                reqTypes.Clear();
                reqTypes.Add(typeof(PlainTcoAnalogueInspectorData));



                reqProperties = PropertyHelper.GetPropertiesNames(new PlainTcoAnalogueInspectorData(), p => p.IsByPassed, p => p.IsExcluded, p => p.RequiredMin, p => p.RequiredMax, p => p.NumberOfAllowedRetries);


                TestData testDataSource, testDataTarget;
                CreateEntities(out testDataSource, out testDataTarget);

                SetupSourceEntity(testDataSource);

                testDataTarget.customChecker1.IsByPassed = !testDataSource.customChecker1.IsByPassed;
                testDataTarget.customChecker2.IsByPassed = !testDataSource.customChecker2.IsByPassed;
                testDataTarget.customChecker3.IsByPassed = !testDataSource.customChecker3.IsByPassed;
                testDataTarget.customChecker4.IsByPassed = !testDataSource.customChecker4.IsByPassed;

                testDataTarget.customChecker1.Result = (short)TcoInspectors.eInspectorResult.Excluded;
                testDataTarget.customChecker2.Result = (short)TcoInspectors.eInspectorResult.Excluded;
                testDataTarget.customChecker3.Result = (short)TcoInspectors.eInspectorResult.NoAction;
                testDataTarget.customChecker4.Result = (short)TcoInspectors.eInspectorResult.NoAction;

                repositorySource.Update(testDataSource._EntityId, testDataSource);

                repositoryTarget.Update(testDataTarget._EntityId, testDataTarget);



                // Act


                var merge = new MergeEntitiesData<TestData>(
                          repositorySource
                        , repositoryTarget
                        , reqTypes
                        , reqProperties
                        , ExclusionAdditionalReqTypeResultNotEqualZero
                        , InclusionAdditionalReqType
                        );

                merge.Merge(testDataSource._EntityId, testDataTarget._EntityId);

                var mergedTarget = repositoryTarget.Read(testDataTarget._EntityId);



                // Assert
                Assert.AreEqual(testDataSource.customChecker1.IsByPassed, mergedTarget.customChecker1.IsByPassed);
                Assert.AreEqual(testDataSource.customChecker1.NumberOfAllowedRetries, mergedTarget.customChecker1.NumberOfAllowedRetries);

                Assert.AreEqual(testDataSource.customChecker2.IsByPassed, mergedTarget.customChecker2.IsByPassed);
                Assert.AreEqual(testDataSource.customChecker2.NumberOfAllowedRetries, mergedTarget.customChecker2.NumberOfAllowedRetries);

                Assert.AreNotEqual(testDataSource.customChecker3.IsByPassed, mergedTarget.customChecker3.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker3.NumberOfAllowedRetries, mergedTarget.customChecker3.NumberOfAllowedRetries);

                Assert.AreNotEqual(testDataSource.customChecker4.IsByPassed, mergedTarget.customChecker4.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker4.NumberOfAllowedRetries, mergedTarget.customChecker4.NumberOfAllowedRetries);


            }
            [Test]
            public void compare_no_req_type_no_prop_no_inclusion_no_exclusion()
            {
                // Arrange  
                //SetupRepositories();
                reqTypes.Clear();
                reqTypes.Add(typeof(PlainTcoAnalogueInspectorData));


                reqProperties = PropertyHelper.GetPropertiesNames(new PlainTcoAnalogueInspectorData(), p => p.IsByPassed, p => p.IsExcluded, p => p.RequiredMin, p => p.RequiredMax, p => p.NumberOfAllowedRetries);


                TestData testDataSource, testDataTarget;
                CreateEntities(out testDataSource, out testDataTarget);

                SetupSourceEntity(testDataSource);

                var random = new Random();
                testDataTarget.customChecker1.IsByPassed = !testDataSource.customChecker1.IsByPassed;
                testDataTarget.customChecker2.IsByPassed = !testDataSource.customChecker2.IsByPassed;
                testDataTarget.customChecker3.IsByPassed = !testDataSource.customChecker3.IsByPassed;
                testDataTarget.customChecker4.IsByPassed = !testDataSource.customChecker4.IsByPassed;

                testDataTarget.customChecker1.Result = (short)TcoInspectors.eInspectorResult.Excluded;
                testDataTarget.customChecker2.Result = (short)TcoInspectors.eInspectorResult.Excluded;
                testDataTarget.customChecker3.Result = (short)TcoInspectors.eInspectorResult.NoAction;
                testDataTarget.customChecker4.Result = (short)TcoInspectors.eInspectorResult.NoAction;

                repositorySource.Update(testDataSource._EntityId, testDataSource);

                repositoryTarget.Update(testDataTarget._EntityId, testDataTarget);



                // Act


                var merge = new MergeEntitiesData<TestData>(
                          repositorySource
                        , repositoryTarget
                        );

                merge.Merge(testDataSource._EntityId, testDataTarget._EntityId);

                var mergedTarget = repositoryTarget.Read(testDataTarget._EntityId);



                // Assert
                Assert.AreNotEqual(testDataSource.customChecker1.IsByPassed, mergedTarget.customChecker1.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker1.NumberOfAllowedRetries, mergedTarget.customChecker1.NumberOfAllowedRetries);

                Assert.AreNotEqual(testDataSource.customChecker2.IsByPassed, mergedTarget.customChecker2.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker2.NumberOfAllowedRetries, mergedTarget.customChecker2.NumberOfAllowedRetries);

                Assert.AreNotEqual(testDataSource.customChecker3.IsByPassed, mergedTarget.customChecker3.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker3.NumberOfAllowedRetries, mergedTarget.customChecker3.NumberOfAllowedRetries);

                Assert.AreNotEqual(testDataSource.customChecker4.IsByPassed, mergedTarget.customChecker4.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker4.NumberOfAllowedRetries, mergedTarget.customChecker4.NumberOfAllowedRetries);


            }

            [Test]
            public void compare_inclusion_exclusion_from_outside()
            {
                TestData testDataSource, testDataTarget;
                CreateEntities(out testDataSource, out testDataTarget);

                SetupSourceEntity(testDataSource);

                var random = new Random();
                testDataTarget.customChecker1.IsByPassed = !testDataSource.customChecker1.IsByPassed;
                testDataTarget.customChecker2.IsByPassed = !testDataSource.customChecker2.IsByPassed;
                testDataTarget.customChecker3.IsByPassed = !testDataSource.customChecker3.IsByPassed;
                testDataTarget.customChecker4.IsByPassed = !testDataSource.customChecker4.IsByPassed;

                testDataTarget.customChecker1.Result = (short)TcoInspectors.eInspectorResult.Excluded;
                testDataTarget.customChecker2.Result = (short)TcoInspectors.eInspectorResult.NoAction;
                testDataTarget.customChecker3.Result = (short)TcoInspectors.eInspectorResult.NoAction;
                testDataTarget.customChecker4.Result = (short)TcoInspectors.eInspectorResult.NoAction;

                repositorySource.Update(testDataSource._EntityId, testDataSource);

                repositoryTarget.Update(testDataTarget._EntityId, testDataTarget);



                // Act


                var merge = new MergeEntitiesData<TestData>(
                          repositorySource
                        , repositoryTarget
                        , new List<Type>()
                        , new List<string>()
                        , ExclusionNotUsed
                        , InclusionNotUsed
                        );

                merge.Merge(testDataSource._EntityId, testDataTarget._EntityId, ExcludeFromOutside, IncludeFromOutside, ReqPropertyFromOutisde);

                var mergedTarget = repositoryTarget.Read(testDataTarget._EntityId);



                // Assert
                Assert.AreEqual(testDataSource.customChecker1.IsByPassed, mergedTarget.customChecker1.IsByPassed);
                Assert.AreEqual(testDataSource.customChecker1.NumberOfAllowedRetries, mergedTarget.customChecker1.NumberOfAllowedRetries);

                Assert.AreNotEqual(testDataSource.customChecker2.IsByPassed, mergedTarget.customChecker2.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker2.NumberOfAllowedRetries, mergedTarget.customChecker2.NumberOfAllowedRetries);

                Assert.AreNotEqual(testDataSource.customChecker3.IsByPassed, mergedTarget.customChecker3.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker3.NumberOfAllowedRetries, mergedTarget.customChecker3.NumberOfAllowedRetries);

                Assert.AreNotEqual(testDataSource.customChecker4.IsByPassed, mergedTarget.customChecker4.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker4.NumberOfAllowedRetries, mergedTarget.customChecker4.NumberOfAllowedRetries);


            }


            [Test]
            public void compare_inclusion_exclusion_from_outside_diff_ctor()
            {
                // Arrange  
                //SetupRepositories();




                TestData testDataSource, testDataTarget;
                CreateEntities(out testDataSource, out testDataTarget);

                SetupSourceEntity(testDataSource);

                var random = new Random();
                testDataTarget.customChecker1.IsByPassed = !testDataSource.customChecker1.IsByPassed;
                testDataTarget.customChecker2.IsByPassed = !testDataSource.customChecker2.IsByPassed;
                testDataTarget.customChecker3.IsByPassed = !testDataSource.customChecker3.IsByPassed;
                testDataTarget.customChecker4.IsByPassed = !testDataSource.customChecker4.IsByPassed;

                testDataTarget.customChecker1.Result = (short)TcoInspectors.eInspectorResult.Excluded;
                testDataTarget.customChecker2.Result = (short)TcoInspectors.eInspectorResult.NoAction;
                testDataTarget.customChecker3.Result = (short)TcoInspectors.eInspectorResult.NoAction;
                testDataTarget.customChecker4.Result = (short)TcoInspectors.eInspectorResult.NoAction;

                repositorySource.Update(testDataSource._EntityId, testDataSource);

                repositoryTarget.Update(testDataTarget._EntityId, testDataTarget);



                // Act


                var merge = new MergeEntitiesData<TestData>(
                          repositorySource
                        , repositoryTarget
                        );

                merge.Merge(testDataSource._EntityId, testDataTarget._EntityId, ExcludeFromOutside, IncludeFromOutside, ReqPropertyFromOutisde);

                var mergedTarget = repositoryTarget.Read(testDataTarget._EntityId);



                // Assert
                Assert.AreEqual(testDataSource.customChecker1.IsByPassed, mergedTarget.customChecker1.IsByPassed);
                Assert.AreEqual(testDataSource.customChecker1.NumberOfAllowedRetries, mergedTarget.customChecker1.NumberOfAllowedRetries);

                Assert.AreNotEqual(testDataSource.customChecker2.IsByPassed, mergedTarget.customChecker2.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker2.NumberOfAllowedRetries, mergedTarget.customChecker2.NumberOfAllowedRetries);

                Assert.AreNotEqual(testDataSource.customChecker3.IsByPassed, mergedTarget.customChecker3.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker3.NumberOfAllowedRetries, mergedTarget.customChecker3.NumberOfAllowedRetries);

                Assert.AreNotEqual(testDataSource.customChecker4.IsByPassed, mergedTarget.customChecker4.IsByPassed);
                Assert.AreNotEqual(testDataSource.customChecker4.NumberOfAllowedRetries, mergedTarget.customChecker4.NumberOfAllowedRetries);


            }

            private IEnumerable<string> ReqPropertyFromOutisde(object obj)
            {
                var retVal = new List<string>();
                switch (obj)
                {

                    case PlainTcoAnalogueInspectorData
                        c:
                        return PropertyHelper.GetPropertiesNames(c, p => p.IsByPassed, p => p.IsExcluded, p => p.RequiredMin, p => p.RequiredMax, p => p.NumberOfAllowedRetries);



                    default:
                        break;
                }

                return new List<string>();
            }

            private bool ExcludeFromOutside(object obj)
            {

                return false;
            }

            private bool IncludeFromOutside(object obj)
            {
                switch (obj)
                {
                    case PlainTcoAnalogueInspectorData c:
                        return c is PlainTcoAnalogueInspectorData && c.Result != 0;




                    default:
                        break;
                }

                return false;
            }


            private bool ExclusionActiveIfResultIsZero(object obj)
            {
                switch (obj)
                {
                    case PlainTcoAnalogueInspectorData c:
                        return c.Result == 0; // not change settings for checkers whitch are not checked yet
                    case PlainTcoDigitalInspectorData c:
                        return c.Result == 0; // not change settings for checkers whitch are not checked yet

                    default:
                        break;
                }

                return false;
            }

            private bool ExclusionAdditionalReqTypeResultNotEqualZero(object obj)
            {
                switch (obj)
                {

                    case PlainTcoDigitalInspectorData c:
                        return c.Result == 0; // not change settings for checkers whitch are not checked yet
                    default:
                        break;
                }

                return false;
            }

            private bool InclusionAdditionalReqType(object obj)
            {
                switch (obj)
                {


                    case PlainTcoDigitalInspectorData c:
                        return c is PlainTcoDigitalInspectorData;


                    default:
                        break;
                }

                return false;
            }
            private bool InclusionNotUsed(object obj)
            {
                //switch (obj)
                //{

                //    case PlainstAnalogueCheckerData c:
                //        return c is PlainstAnalogueCheckerData;

                //    case PlainstCu_Header c:
                //        return c is PlainstCu_Header;

                //    case PlainstCu_1_ProcessData c:
                //        return c is PlainstCu_1_ProcessData;
                //    default:
                //        break;
                //}

                return false;
            }

            private bool ExclusionNotUsed(object obj)
            {
                switch (obj)
                {

                    default:
                        break;
                }

                return false;
            }

            private IEnumerable<string> PropertiesInclusion(object obj)
            {
                var retVal = new List<string>();
                //switch (obj)
                //{

                //    case PlainstAnalogueCheckerData c:

                //        return DataMerge.PropertyHelper.GetPropertiesNames(c, p => p.IsByPassed, p => p.IsExcluded, p => p.RequiredMin);


                //    case PlainstCu_1_ProcessData c:
                //        return DataMerge.PropertyHelper.GetPropertiesNames(c, p => p.Check, p => p.LogicCheck, p => p.Header);

                //    default:
                //        break;
                //}

                return new List<string>();
            }
        }


    public class TestData : TcoData.PlainTcoEntity
    {

        public DateTime _Created { get; set; }

        public DateTime _Modified { get; set; }

        public PlainTcoAnalogueInspectorData customChecker1 { get; set; } = new PlainTcoAnalogueInspectorData();
        public PlainTcoAnalogueInspectorData customChecker2 { get; set; } = new PlainTcoAnalogueInspectorData();

        public PlainTcoDigitalInspectorData customChecker3 { get; set; } = new PlainTcoDigitalInspectorData();
        public PlainTcoDigitalInspectorData customChecker4 { get; set; } = new PlainTcoDigitalInspectorData();

    }
}
