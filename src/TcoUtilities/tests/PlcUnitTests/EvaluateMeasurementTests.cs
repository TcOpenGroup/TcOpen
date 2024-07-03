using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TcoUtilitiesTests;

namespace TcoEvaluateMeasurement
{
    public class Units
    {
        TcoUtilitiesTests.TcoEvaluateMeasurementContext sut;

        [OneTimeSetUp]
        public void OnTimeSetup()
        {
            Entry.TcoUtilitiesTests.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
            sut = Entry.TcoUtilitiesTests.MAIN._testEvalueateMeasurementContext;
            Entry.TcoUtilitiesTests.MAIN._testEvalueateMeasurementContext._tcoMeasEvaluator.InitializeTask();
        }

        [SetUp]
        public void Setup()
        {
            sut._entityId.Synchron = "TestId";
            sut._tcoMeasEvaluatorConfig.IgnoreZeroSamplesIfDistance.Synchron = false;
            sut._tcoMeasEvaluatorConfig.IgnoreZeroSamplesIfTimeBase.Synchron = false;

            sut._tcoMeasEvaluatorConfig.FilterValue.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.SearchRange.Synchron = 20;
            sut._tcoMeasEvaluatorConfig.FilterValue.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.TriggerNoise.Synchron = 0.3f;
            sut._tcoMeasEvaluatorConfig.PeaksNoise.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.SmoothFactor.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromStart.Synchron = 0;
            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromEnd.Synchron = 0;
            sut._tcoMeasEvaluatorConfig.IgnoreZeroSamplesIfDistance.Synchron = true;
            sut._tcoMeasEvaluatorConfig.LimitIndexFoundExtrems.Synchron = 10;
            sut._tcoMeasEvaluatorConfig.LimitIndexFoundTriggers.Synchron = 10;

            //sut._tcoMeasEvaluatorConfig.ExportResultsLocation.Synchron = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            //sut._tcoMeasEvaluatorConfig.ExportRawLocation.Synchron = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var pathRaw =
                System.IO.Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().Location
                )
                + "\\"
                + sut._entityId.Synchron
                + "_RawData.csv";
            var pathResult =
                System.IO.Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().Location
                )
                + "\\"
                + sut._entityId.Synchron
                + "_Results.csv";

            if (System.IO.File.Exists(pathRaw))
                System.IO.File.Delete(pathRaw);
            if (System.IO.File.Exists(pathResult))
                System.IO.File.Delete(pathResult);

            sut._tcoMeasEvaluatorConfig.ExportResultsLocation.Synchron = string.Empty;
            sut._tcoMeasEvaluatorConfig.ExportRawLocation.Synchron = string.Empty;
        }

        //[Test]

        ////[TestCase ("RawData1.csv")]
        //[TestCase("RawData2.csv")]

        //public void check_found__peaks_triggers_from_sources(string fileName)
        //{
        //    //-- Arrange
        //    var pathRaw = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + fileName;



        //    sut._tcoMeasEvaluatorConfig.SearchRange.Synchron = 20;
        //    sut._tcoMeasEvaluatorConfig.FilterValue.Synchron = 1;
        //    sut._tcoMeasEvaluatorConfig.TriggerNoise.Synchron = 0.2f;
        //    sut._tcoMeasEvaluatorConfig.PeaksNoise.Synchron = 1;
        //    sut._tcoMeasEvaluatorConfig.SmoothFactor.Synchron = 4;
        //    sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromStart.Synchron = 10;
        //    sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromEnd.Synchron = 100;
        //    sut._tcoMeasEvaluatorConfig.IgnoreZeroSamplesIfDistance.Synchron = true;
        //    sut._tcoMeasEvaluatorConfig.LimitIndexFoundExtrems.Synchron = 100;
        //    sut._tcoMeasEvaluatorConfig.LimitIndexFoundTriggers.Synchron = 100;
        //    sut._tcoMeasEvaluatorConfig.ExportResultsLocation.Synchron = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        //    sut._tcoMeasEvaluatorConfig.ExportRawLocation.Synchron = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);



        //    sut.ExecuteProbeRun(1, (int)eTcoEvaluateMeasurementTestList.Init);


        //    int i = 0;
        //    foreach (string line in System.IO.File.ReadLines(pathRaw))
        //    {
        //        if (!string.IsNullOrEmpty(line))
        //        {
        //            var cleanString = Regex.Replace(line, @"[^0-9a-zA-Z\.;_]", string.Empty);
        //            var splited = cleanString.Split(';');
        //            sut._measData[i].Distance.Synchron = Single.Parse(splited[1], CultureInfo.InvariantCulture);
        //            sut._measData[i].ProcessValue.Synchron = Single.Parse(splited[2], CultureInfo.InvariantCulture);
        //            sut._measData[i].DiscreteValue.Synchron = Single.Parse(splited[3], CultureInfo.InvariantCulture);

        //        }
        //        i++;
        //    }

        //    //-- Act
        //    sut.ExecuteProbeRun((int)eTcoEvaluateMeasurementTestList.Evaluate, () => sut._tcoEvaluateTestDone.Synchron);

        //    //-- Assert
        //    //Assert.AreEqual(3, sut._tcoMeasEvaluator._results.RisingPeaksFound.Synchron);
        //    //Assert.AreEqual(3, sut._tcoMeasEvaluator._results.FallingPeaksFound.Synchron);
        //    Assert.AreEqual(2, sut._tcoMeasEvaluator._results.TriggersFound.Synchron);

        //}


        [Test]
        [Repeat(4)]
        public void check_found_number_of_peaks_triggers()
        {
            //-- Arrange


            sut._tcoMeasEvaluatorConfig.SearchRange.Synchron = 20;
            sut._tcoMeasEvaluatorConfig.FilterValue.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.TriggerNoise.Synchron = 0.2f;
            sut._tcoMeasEvaluatorConfig.PeaksNoise.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.SmoothFactor.Synchron = 4;
            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromStart.Synchron = 0;
            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromEnd.Synchron = 0;
            sut._tcoMeasEvaluatorConfig.IgnoreZeroSamplesIfDistance.Synchron = true;
            sut._tcoMeasEvaluatorConfig.LimitIndexFoundExtrems.Synchron = 10;
            sut._tcoMeasEvaluatorConfig.LimitIndexFoundTriggers.Synchron = 10;

            sut.ExecuteProbeRun(1, (int)eTcoEvaluateMeasurementTestList.Init);

            PrepareDummyData();

            //-- Act
            sut.ExecuteProbeRun(
                (int)eTcoEvaluateMeasurementTestList.Evaluate,
                () => sut._tcoEvaluateTestDone.Synchron
            );

            //-- Assert
            Assert.AreEqual(3, sut._tcoMeasEvaluator._results.RisingPeaksFound.Synchron);
            Assert.AreEqual(3, sut._tcoMeasEvaluator._results.FallingPeaksFound.Synchron);
            Assert.AreEqual(2, sut._tcoMeasEvaluator._results.TriggersFound.Synchron);
        }

        private void PrepareDummyData()
        {
            for (int i = 0; i < dummyDistanceData.Count; i++)
            {
                sut._measData[i].Distance.Synchron = dummyDistanceData[i];
                sut._measData[i].ProcessValue.Synchron = dummyProcessValueData[i];
                sut._measData[i].DiscreteValue.Synchron = dummyDiscreteValueData[i];
            }
        }

        [Test]
        public void find_global_maximum()
        {
            sut._tcoMeasEvaluatorConfig.SearchRange.Synchron = 20;
            sut._tcoMeasEvaluatorConfig.FilterValue.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.TriggerNoise.Synchron = 0.2f;
            sut._tcoMeasEvaluatorConfig.PeaksNoise.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.SmoothFactor.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromStart.Synchron = 0;
            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromEnd.Synchron = 0;
            sut._tcoMeasEvaluatorConfig.IgnoreZeroSamplesIfDistance.Synchron = false;
            sut._tcoMeasEvaluatorConfig.LimitIndexFoundExtrems.Synchron = 10;
            sut._tcoMeasEvaluatorConfig.LimitIndexFoundTriggers.Synchron = 10;

            sut.ExecuteProbeRun(1, (int)eTcoEvaluateMeasurementTestList.Init);

            PrepareDummyData();

            //-- Arrange
            for (int i = 0; i < dummyDistanceData.Count; i++)
            {
                sut._measData[i].Distance.Synchron = dummyDistanceData[i];
                sut._measData[i].ProcessValue.Synchron = dummyProcessValueData[i];
                sut._measData[i].DiscreteValue.Synchron = dummyDiscreteValueData[i];
            }

            var (maxValue, maxIndex) = dummyProcessValueData.Select((x, i) => (x, i)).Max();

            //-- Act
            sut.ExecuteProbeRun(
                (int)eTcoEvaluateMeasurementTestList.Evaluate,
                () => sut._tcoEvaluateTestDone.Synchron
            );

            //-- Assert
            Assert.AreEqual(
                maxValue,
                sut._tcoMeasEvaluator
                    ._results
                    .RisingPeaks[sut._tcoMeasEvaluator._results.RisingPeaksFound.Synchron - 1]
                    .ProcessValue
                    .Synchron
            );
        }

        [Test]
        public void find_local_max()
        {
            //-- Arrange
            sut._tcoMeasEvaluatorConfig.SmoothFactor.Synchron = 4;
            sut._tcoMeasEvaluatorConfig.SearchRange.Synchron = 10;
            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromStart.Synchron = 25;
            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromEnd.Synchron = (short)(
                sut._measData.Length - 75
            );
            sut._tcoMeasEvaluatorConfig.IgnoreZeroSamplesIfDistance.Synchron = false;

            sut.ExecuteProbeRun(1, (int)eTcoEvaluateMeasurementTestList.Init);
            PrepareDummyData();

            //-- Act
            sut.ExecuteProbeRun(
                (int)eTcoEvaluateMeasurementTestList.Evaluate,
                () => sut._tcoEvaluateTestDone.Synchron
            );

            //-- Assert
            Assert.AreEqual(1, sut._tcoMeasEvaluator._results.RisingPeaksFound.Synchron);
        }

        [Test]
        public void find_local_min()
        {
            //-- Arrange


            sut._tcoMeasEvaluatorConfig.SearchRange.Synchron = 10;

            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromStart.Synchron = 60;
            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromEnd.Synchron = (short)(
                sut._measData.Length - 85
            );
            sut._tcoMeasEvaluatorConfig.IgnoreZeroSamplesIfDistance.Synchron = false;

            sut.ExecuteProbeRun(1, (int)eTcoEvaluateMeasurementTestList.Init);

            PrepareDummyData();

            //-- Act
            sut.ExecuteProbeRun(
                (int)eTcoEvaluateMeasurementTestList.Evaluate,
                () => sut._tcoEvaluateTestDone.Synchron
            );

            //-- Assert
            Assert.AreEqual(1, sut._tcoMeasEvaluator._results.FallingPeaksFound.Synchron);
        }

        [Test]
        public void find_global_minimum()
        {
            //-- Arrange

            sut._tcoMeasEvaluatorConfig.SearchRange.Synchron = 20;
            sut._tcoMeasEvaluatorConfig.FilterValue.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.TriggerNoise.Synchron = 0.2f;
            sut._tcoMeasEvaluatorConfig.PeaksNoise.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.SmoothFactor.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromStart.Synchron = 0;
            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromEnd.Synchron = 0;
            sut._tcoMeasEvaluatorConfig.IgnoreZeroSamplesIfDistance.Synchron = true;
            sut._tcoMeasEvaluatorConfig.LimitIndexFoundExtrems.Synchron = 10;
            sut._tcoMeasEvaluatorConfig.LimitIndexFoundTriggers.Synchron = 10;

            sut.ExecuteProbeRun(1, (int)eTcoEvaluateMeasurementTestList.Init);
            PrepareDummyData();

            var minNonZeroValue = dummyProcessValueData.Where(x => x != 0).Min();

            //-- Act
            sut.ExecuteProbeRun(
                (int)eTcoEvaluateMeasurementTestList.Evaluate,
                () => sut._tcoEvaluateTestDone.Synchron
            );

            //-- Assert
            Assert.AreEqual(
                minNonZeroValue,
                sut._tcoMeasEvaluator._results.FallingPeaks[0].ProcessValue.Synchron
            );
        }

        [Test]
        public void check_if_raw_data_export_success()
        {
            //-- Arrange



            sut._tcoMeasEvaluatorConfig.SearchRange.Synchron = 20;
            sut._tcoMeasEvaluatorConfig.FilterValue.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.TriggerNoise.Synchron = 0.2f;
            sut._tcoMeasEvaluatorConfig.PeaksNoise.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.SmoothFactor.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromStart.Synchron = 0;
            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromEnd.Synchron = 0;
            sut._tcoMeasEvaluatorConfig.IgnoreZeroSamplesIfDistance.Synchron = true;
            sut._tcoMeasEvaluatorConfig.LimitIndexFoundExtrems.Synchron = 10;
            sut._tcoMeasEvaluatorConfig.LimitIndexFoundTriggers.Synchron = 10;
            sut._tcoMeasEvaluatorConfig.ExportResultsLocation.Synchron =
                System.IO.Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().Location
                );
            sut._tcoMeasEvaluatorConfig.ExportRawLocation.Synchron =
                System.IO.Path.GetDirectoryName(
                    System.Reflection.Assembly.GetExecutingAssembly().Location
                );

            var pathRaw =
                sut._tcoMeasEvaluatorConfig.ExportRawLocation.Synchron
                + "\\"
                + sut._entityId.Synchron
                + "_RawData.csv";
            var pathResult =
                sut._tcoMeasEvaluatorConfig.ExportResultsLocation.Synchron
                + "\\"
                + sut._entityId.Synchron
                + "_Results.csv";

            sut.ExecuteProbeRun(1, (int)eTcoEvaluateMeasurementTestList.Init);

            PrepareDummyData();

            var minNonZeroValue = dummyProcessValueData.Where(x => x != 0).Min();

            //-- Act
            sut.ExecuteProbeRun(
                (int)eTcoEvaluateMeasurementTestList.Evaluate,
                () => sut._tcoEvaluateTestDone.Synchron
            );

            //-- Assert
            Assert.AreEqual(true, System.IO.File.Exists(pathRaw));
            Assert.AreEqual(true, System.IO.File.Exists(pathResult));
        }

        [Test]
        public void check_if_raw_data_export_not_provided()
        {
            //-- Arrange


            sut._tcoMeasEvaluatorConfig.SearchRange.Synchron = 20;
            sut._tcoMeasEvaluatorConfig.FilterValue.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.TriggerNoise.Synchron = 0.2f;
            sut._tcoMeasEvaluatorConfig.PeaksNoise.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.SmoothFactor.Synchron = 1;
            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromStart.Synchron = 0;
            sut._tcoMeasEvaluatorConfig.IgnoreSamplesFromEnd.Synchron = 0;
            sut._tcoMeasEvaluatorConfig.IgnoreZeroSamplesIfDistance.Synchron = true;
            sut._tcoMeasEvaluatorConfig.LimitIndexFoundExtrems.Synchron = 10;
            sut._tcoMeasEvaluatorConfig.LimitIndexFoundTriggers.Synchron = 10;

            var pathRaw =
                sut._tcoMeasEvaluatorConfig.ExportRawLocation.Synchron
                + "\\"
                + sut._entityId.Synchron
                + "_RawData.csv";
            var pathResult =
                sut._tcoMeasEvaluatorConfig.ExportResultsLocation.Synchron
                + "\\"
                + sut._entityId.Synchron
                + "_Results.csv";

            sut.ExecuteProbeRun(1, (int)eTcoEvaluateMeasurementTestList.Init);

            PrepareDummyData();

            var minNonZeroValue = dummyProcessValueData.Where(x => x != 0).Min();

            //-- Act
            sut.ExecuteProbeRun(
                (int)eTcoEvaluateMeasurementTestList.Evaluate,
                () => sut._tcoEvaluateTestDone.Synchron
            );

            //-- Assert
            Assert.AreEqual(false, System.IO.File.Exists(pathRaw));
            Assert.AreEqual(false, System.IO.File.Exists(pathResult));
        }

        private static readonly List<float> dummyDistanceData = new List<float>()
        {
            0.0175f,
            0.035f,
            0.0525f,
            0.07f,
            0.0875f,
            0.105f,
            0.1225f,
            0.14f,
            0.1575f,
            0.175f,
            0.1925f,
            0.21f,
            0.2275f,
            0.245f,
            0.2625f,
            0.28f,
            0.2975f,
            0.315f,
            0.3325f,
            0.3500001f,
            0.3675001f,
            0.3850001f,
            0.4025001f,
            0.4200001f,
            0.4375001f,
            0.4550001f,
            0.4725001f,
            0.4900002f,
            0.5075002f,
            0.5250002f,
            0.5425001f,
            0.5600001f,
            0.5775001f,
            0.5950001f,
            0.6125001f,
            0.6300001f,
            0.6475f,
            0.665f,
            0.6825f,
            0.7f,
            0.7175f,
            0.735f,
            0.7524999f,
            0.7699999f,
            0.7874999f,
            0.8049999f,
            0.8224999f,
            0.8399999f,
            0.8574998f,
            0.8749998f,
            0.8924998f,
            0.9099998f,
            0.9274998f,
            0.9449998f,
            0.9624997f,
            0.9799997f,
            0.9974997f,
            1.015f,
            1.0325f,
            1.0425f,
            1.0675f,
            1.085f,
            1.1025f,
            1.1345261f,
            1.1375f,
            1.155f,
            1.1725f,
            1.1825f,
            1.1925f,
            1.225f,
            1.235f,
            1.245f,
            1.265f,
            1.295f,
            1.131f,
            1.330001f,
            1.347501f,
            1.365001f,
            1.382501f,
            1.400001f,
            1.417501f,
            1.435001f,
            1.452501f,
            1.470001f,
            1.487501f,
            1.505001f,
            1.522501f,
            1.540001f,
            1.557501f,
            1.575001f,
            1.592501f,
            1.610001f,
            1.627501f,
            1.645001f,
            1.662501f,
            1.680001f,
            1.697501f,
            1.715001f,
            1.732502f,
            1.750002f,
            1.767502f,
            1.785002f,
            1.802502f,
            1.820002f,
            1.837502f,
            1.855002f,
            1.872502f,
            1.890002f,
            1.907502f,
            1.925002f,
            1.942502f,
            1.960002f,
            1.977502f,
            1.995002f,
            2.012502f,
            2.030002f,
            2.047502f,
            2.065002f,
            2.082502f,
            2.100002f,
            2.117502f,
            2.135002f,
            2.152502f,
            2.170002f,
            2.187501f,
            2.205001f,
            2.222501f,
            2.240001f,
            2.257501f,
            2.275001f,
            2.292501f,
            2.310001f,
            2.327501f,
            2.345001f,
            2.362501f,
            2.380001f,
            2.397501f,
            2.415f,
            2.42885746f,
            2.4416469f,
            2.461013581f,
            2.485f,
            2.491141416f,
            2.509025f,
            2.51269251f,
            2.555f,
            2.56397086f,
            2.5721582f,
            2.591524921f,
            2.625f,
            2.642499f,
            2.659999f,
            2.677499f,
            2.694999f,
            2.712499f,
            2.729999f,
            2.747499f,
            2.764999f,
            2.782499f,
            2.799999f,
            2.817499f,
            2.834999f,
            2.852499f,
            2.869998f,
            2.887498f,
            2.904998f,
            2.922498f,
            2.939998f,
            2.957498f,
            2.974998f,
            2.992498f,
            3.009998f,
            3.027498f,
            3.044998f,
            3.062498f,
            3.079998f,
            3.097497f,
            3.114997f,
            3.132497f,
            3.149997f,
            3.167497f,
            3.184997f,
            3.202497f,
            3.219997f,
            3.237497f,
            3.254997f,
            3.272497f,
            3.289997f,
            3.307497f,
            3.324996f,
            3.342496f,
            3.359996f,
            3.377496f,
            3.394996f,
            3.412496f,
            3.429996f,
            3.447496f,
            3.464996f,
            3.482496f,
            3.499996f,
            3.517496f,
            3.534996f,
            3.552495f,
            3.569995f,
            3.587495f,
            3.604995f,
            3.622495f,
            3.639995f,
            3.657495f,
            3.674995f,
            3.692495f,
            3.709995f,
            3.727495f,
            3.744995f,
            3.762495f,
            3.779994f,
            3.797494f,
            3.814994f,
            3.832494f,
            3.849994f,
            3.867494f,
            3.884994f,
            3.902494f,
            3.919994f,
            3.937494f,
            3.954994f,
            3.972494f,
            3.989994f,
            4.007493f,
            4.024993f,
            4.042493f,
            4.059993f,
            4.077493f,
            4.094993f,
            4.112493f,
            4.129993f,
            4.147493f,
            4.164993f,
            4.182493f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f
        };
        private static readonly List<float> dummyProcessValueData = new List<float>()
        {
            1.400277f,
            1.689494f,
            1.689523f,
            1.958728f,
            1.958683f,
            2.181783f,
            2.353907f,
            2.353832f,
            2.605796f,
            2.944484f,
            2.944633f,
            3.17435f,
            3.321245f,
            3.321305f,
            3.49468f,
            3.494546f,
            3.730565f,
            3.979921f,
            3.979832f,
            4.206836f,
            4.312575f,
            4.312322f,
            4.421353f,
            4.421696f,
            4.593119f,
            4.805714f,
            4.805714f,
            4.952133f,
            5.059347f,
            5.05957f,
            5.20891f,
            5.310029f,
            5.310371f,
            5.442664f,
            5.442426f,
            5.520925f,
            5.589932f,
            5.589902f,
            5.648046f,
            5.691335f,
            5.691782f,
            5.761579f,
            5.760565f,
            5.782008f,
            5.776778f,
            5.776286f,
            5.772173f,
            5.773947f,
            5.774036f,
            5.755037f,
            5.754516f,
            5.739167f,
            5.700379f,
            5.700245f,
            5.65514f,
            5.571217f,
            5.571813f,
            5.478129f,
            5.427227f,
            5.427271f,
            5.314618f,
            5.314633f,
            5.19523f,
            5.050704f,
            5.050883f,
            4.950821f,
            4.848287f,
            4.848585f,
            4.655719f,
            4.65551f,
            4.508913f,
            4.388958f,
            4.389122f,
            4.201799f,
            4.074648f,
            4.074916f,
            3.921717f,
            4.085645f,
            4.0856f,
            4.255801f,
            4.255757f,
            4.42335f,
            4.590959f,
            4.590929f,
            4.772141f,
            4.955754f,
            4.954905f,
            5.064816f,
            5.065486f,
            5.214482f,
            5.340472f,
            5.340278f,
            5.487934f,
            5.605414f,
            5.655414f,
            5.734131f,
            5.734071f,
            5.846247f,
            5.973145f,
            5.973101f,
            6.105751f,
            6.24147f,
            6.241173f,
            6.34262f,
            6.504238f,
            6.504282f,
            6.638676f,
            6.638378f,
            6.78955f,
            6.903365f,
            6.902978f,
            7.032201f,
            7.170811f,
            7.170394f,
            7.304832f,
            7.304952f,
            7.409409f,
            7.53653f,
            7.53653f,
            7.610291f,
            7.752657f,
            7.752151f,
            7.850185f,
            7.983491f,
            7.983178f,
            8.064583f,
            8.065298f,
            8.155704f,
            8.283883f,
            8.283318f,
            8.385405f,
            8.503318f,
            8.504123f,
            8.568987f,
            8.568287f,
            8.665859f,
            8.731619f,
            8.731991f,
            8.782119f,
            8.856148f,
            8.854776f,
            8.956104f,
            8.957535f,
            8.968994f,
            8.999258f,
            8.999557f,
            9.033069f,
            9.014786f,
            9.015337f,
            9.008124f,
            8.912489f,
            8.912206f,
            8.809134f,
            8.809507f,
            8.648783f,
            8.426383f,
            8.426115f,
            8.048505f,
            7.722154f,
            7.721901f,
            7.40546f,
            7.405281f,
            7.121682f,
            6.909981f,
            6.91016f,
            6.753072f,
            6.655291f,
            6.655619f,
            6.600171f,
            6.593421f,
            6.593079f,
            6.628424f,
            6.628111f,
            6.681383f,
            6.707907f,
            6.708175f,
            6.808175f,
            6.93658f,
            6.937117f,
            7.045582f,
            7.045731f,
            7.145464f,
            7.308543f,
            7.308364f,
            7.371754f,
            7.544398f,
            7.545739f,
            7.770702f,
            7.768795f,
            7.976904f,
            8.188352f,
            8.188054f,
            8.409694f,
            8.576557f,
            8.576542f,
            8.747906f,
            8.979917f,
            8.979321f,
            9.193972f,
            9.194747f,
            9.428829f,
            9.67522f,
            9.7522f,
            9.859741f,
            10.11585f,
            10.11536f,
            10.32878f,
            10.32875f,
            10.57747f,
            10.79318f,
            10.89318f,
            11.03826f,
            11.28845f,
            11.28905f,
            11.59605f,
            11.59625f,
            11.78765f,
            12.04643f,
            12.04681f,
            12.27611f,
            12.48373f,
            12.48382f,
            12.74955f,
            13.01718f,
            13.01491f,
            13.27938f,
            13.28105f,
            13.43022f,
            13.80415f,
            13.80424f,
            13.95986f,
            14.19134f,
            14.19269f,
            14.49324f,
            14.49305f,
            14.7623f,
            14.98823f,
            14.98908f,
            15.18738f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f
        };
        private static readonly List<float> dummyDiscreteValueData = new List<float>()
        {
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            4f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            6f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f,
            0f
        };
    }
}
