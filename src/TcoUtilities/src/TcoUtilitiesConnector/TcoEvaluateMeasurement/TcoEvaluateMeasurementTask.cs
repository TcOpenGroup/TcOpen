using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Vortex.Connector;
using System.Threading;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace TcoUtilities
{
    public partial class TcoEvaluateMeasurementTask
    {
        private PlainTcoEvaluateMeasurementTask plainData; 

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
           

        }
     
        public void InitializeTask()
        {
            this.InitializeExclusively(FindExtrems);
        }

     

        public string RemoveUnnecessary(string source)
        {
            string result = string.Empty;
            string regex = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars());
            Regex reg = new Regex(string.Format("[{0}]", Regex.Escape(regex)));
            result = reg.Replace(source, "");
            return result;
        }

        public IList<TimeSpan> TimeBaseValues { get; private set; }
        public IList<float> DistanceValues { get; private set; }

        public IList<float> ProcessValues { get; private set; }
     
        public IList<float> FindTriggersValues { get; private set; }


        public int LimitIndex { get; private set; }

        private bool FindExtrems()
        {
          

            try
            {
                plainData = new PlainTcoEvaluateMeasurementTask();
                this.Read();
                this.FlushOnlineToPlain(plainData);

                var filterValue = plainData._config.FilterValue;
                var searchRange = plainData._config.SearchRange;
                var noisePeak = plainData._config.PeaksNoise;
                var noiseTrigger = plainData._config.TriggerNoise;
                var smootFactor = plainData._config.SmoothFactor;
                var ignoreSamplesFromStart = plainData._config.IgnoreSamplesFromStart;
                var ignoreSamplesFromEnd = plainData._config.IgnoreSamplesFromEnd;
                var ignoreSampleIfTimeBaseIsZero = plainData._config.IgnoreZeroSamplesIfTimeBase;
                var ignoreSampleIfDistanceIsZero = plainData._config.IgnoreZeroSamplesIfDistance;
                var limitIndexExtrems = plainData._config.LimitIndexFoundExtrems;
                var limitIndexTriggers = plainData._config.LimitIndexFoundTriggers;


                var len = plainData._data.Length;

                IEnumerable<PlainTcoEvaluateMeasurementDataItem> data = plainData._data;

                if (ignoreSampleIfTimeBaseIsZero)
                    data = data.Where(p => p.TimeBase != new TimeSpan());


                if (ignoreSampleIfDistanceIsZero)
                    data = data.Where(p => p.Distance != 0);



                data = data.Skip(ignoreSamplesFromStart);
             
                data = data.Take(data.Count() - ignoreSamplesFromEnd);


                DistanceValues = data.Select(p => p.Distance).Where((x, i) => i % filterValue == 0).ToList();

                ProcessValues = data.Select(p => p.ProcessValue).Where((x, i) => i % filterValue == 0).ToList();

                FindTriggersValues = data.Select(p => p.DiscreteValue).Where((x, i) => i % filterValue == 0).ToList();


                if (smootFactor != 1)
                {
                    if (ProcessValues.Count != 0)
                    {
                        ProcessValues = FindExtremsHelper.FilterByMovingAverage(ProcessValues, Convert.ToInt32(smootFactor));
                        DistanceValues = DistanceValues.Take(ProcessValues.Count).ToArray();
                        FindTriggersValues = FindTriggersValues.Take(ProcessValues.Count).ToArray();
                    }
                }


               var risingPeaksIndex = FindExtremsHelper.FindPeaks(ProcessValues, false, searchRange, noisePeak).ToArray(); 
               var fallingPeaksIndex = FindExtremsHelper.FindPeaks(ProcessValues, true, searchRange, noisePeak).ToArray(); 
               var triggerIndex = FindExtremsHelper.FindTrigger(FindTriggersValues, noiseTrigger).ToArray();


                var risingPeaks = FindExtremsHelper.ElementsAt(ProcessValues, risingPeaksIndex);
                var fallingPeaks = FindExtremsHelper.ElementsAt(ProcessValues, fallingPeaksIndex);
                var triggerLin = FindExtremsHelper.ElementsAt(FindTriggersValues, triggerIndex);

                var risingPeaksDistance = FindExtremsHelper.ElementsAt(DistanceValues, risingPeaksIndex);
                var fallingPeaksDistance = FindExtremsHelper.ElementsAt(DistanceValues, fallingPeaksIndex);
                var triggerLinDistance = FindExtremsHelper.ElementsAt(DistanceValues, triggerIndex);

                LimitIndex = limitIndexExtrems == 0 ? 100 : limitIndexExtrems;

                int itemIndex = 0;

                _results.RisingPeaksFound.Synchron = (short)risingPeaks.Count();
                _results.FallingPeaksFound.Synchron = (short)fallingPeaks.Count();
                _results.TriggersFound.Synchron = (short)triggerLin.Count();
                
                foreach (var item in risingPeaksIndex)
                {
                    if (itemIndex<=LimitIndex)
                    {
                        _results.RisingPeaks[itemIndex].ProcessValue.Synchron = ProcessValues.ElementAt(item);
                        _results.RisingPeaks[itemIndex].Distance.Synchron = DistanceValues.ElementAt(item);
                        _results.RisingPeaks[itemIndex].DiscreteValue.Synchron = FindTriggersValues.ElementAt(item);

                    }

                    itemIndex++;
                }

                itemIndex = 0;
     

                foreach (var item in fallingPeaksIndex)
                {
                    if (itemIndex <= LimitIndex)
                    {
                        
                        _results.FallingPeaks[itemIndex].ProcessValue.Synchron = ProcessValues.ElementAt(item);
                        _results.FallingPeaks[itemIndex].Distance.Synchron = DistanceValues.ElementAt(item);
                        _results.FallingPeaks[itemIndex].DiscreteValue.Synchron = FindTriggersValues.ElementAt(item);

                    }
                    itemIndex++;
                }

                itemIndex = 0;
                LimitIndex = limitIndexTriggers == 0 ? 100 : limitIndexTriggers;
                foreach (var item in triggerIndex)
                {
                    if (itemIndex <= LimitIndex)
                    {
                        _results.Triggers[itemIndex].ProcessValue.Synchron = ProcessValues.ElementAt(item);
                        _results.Triggers[itemIndex].Distance.Synchron = DistanceValues.ElementAt(item);
                        _results.Triggers[itemIndex].DiscreteValue.Synchron = FindTriggersValues.ElementAt(item);

                    }
                    itemIndex++;
                }

                plainData = new PlainTcoEvaluateMeasurementTask();
                this.Read();
                this.FlushOnlineToPlain(plainData);

                ExportRawData(plainData);
                ExportResultsData(plainData);

                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void ExportRawData(PlainTcoEvaluateMeasurementTask plainData)
        {
            var config = plainData._config;
            var exportLocation = config.ExportRawLocation;

            var entityId = _entityId.Synchron + "_RawData";

            if (exportLocation == string.Empty || !System.IO.Directory.Exists(exportLocation))
                return;



            var measData = plainData._data;

            StringBuilder data = new StringBuilder();

            // If you want headers for your file
            var header = string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\"",
                                       "Time",
                                       "Distance",
                                       config.ProcessValueRowName,
                                       config.DiscreteValueRowName
                                      ); 
            data.AppendLine(header);

            foreach (var item in measData)
            {
                var listResults = string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\"",
                                                  item.TimeBase,
                                                  item.Distance,
                                                  item.ProcessValue,
                                                  item.DiscreteValue
                                                 );
                data.AppendLine(listResults);
            }

            var fileNamePath = string.Format("{0}{1}{2}.csv", exportLocation, "\\", RemoveUnnecessary(entityId));


            try
            {
                using (var sw = new System.IO.StreamWriter(fileNamePath, true))
                {
                    sw.Write(data.ToString());
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }


        }

        public void ExportResultsData(PlainTcoEvaluateMeasurementTask plainData)
        {
            var config = plainData._config;
            var exportLocation = config.ExportResultsLocation;

            var id = plainData._entityId + "_Results";


            if (exportLocation == string.Empty || !System.IO.Directory.Exists(exportLocation))
                return;

         

            StringBuilder data = new StringBuilder();

            data.AppendLine("CONFIG");

            data.AppendLine(string.Format("{0}={1}",nameof(config.SearchRange), config.SearchRange));
            data.AppendLine(string.Format("{0}={1}", nameof(config.FilterValue), config.FilterValue));
            data.AppendLine(string.Format("{0}={1}", nameof(config.PeaksNoise), config.PeaksNoise));
            data.AppendLine(string.Format("{0}={1}", nameof(config.TriggerNoise), config.TriggerNoise));
            data.AppendLine(string.Format("{0}={1}", nameof(config.SmoothFactor), config.SmoothFactor));
            data.AppendLine(string.Format("{0}={1}", nameof(config.IgnoreSamplesFromStart), config.IgnoreSamplesFromStart));
            data.AppendLine(string.Format("{0}={1}", nameof(config.IgnoreSamplesFromEnd), config.IgnoreSamplesFromEnd));
            data.AppendLine(string.Format("{0}={1}", nameof(config.IgnoreZeroSamplesIfTimeBase), config.IgnoreZeroSamplesIfTimeBase));
            data.AppendLine(string.Format("{0}={1}", nameof(config.IgnoreZeroSamplesIfDistance), config.IgnoreZeroSamplesIfDistance));
            data.AppendLine(string.Format("{0}={1}", nameof(config.LimitIndexFoundExtrems), config.LimitIndexFoundExtrems));
            data.AppendLine(string.Format("{0}={1}", nameof(config.LimitIndexFoundTriggers), config.LimitIndexFoundTriggers));


            data.AppendLine("RESULTS");
            data.AppendLine(string.Format("{0} = {1}", "Founded Rising Peaks", plainData._results.RisingPeaksFound));
            data.AppendLine(string.Format("{0} = {1}", "Founded Falling Peaks", plainData._results.FallingPeaksFound));
            data.AppendLine(string.Format("{0} = {1}", "Founded Trigges", plainData._results.TriggersFound));




            // If you want headers for your file
            var header = string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\"",
                                       "Time",
                                       "Distance",
                                       config.ProcessValueRowName,
                                       config.DiscreteValueRowName
                                      ); ;
            data.AppendLine(header);

            data.AppendLine("<<<<<<Rising Peaks>>>>>>");
            foreach (var item in plainData._results.RisingPeaks.Where(x => x.ProcessValue != 0))
            {
                var listResults = string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\"",
                                                  item.TimeBase,
                                                  item.Distance,
                                                  item.ProcessValue,
                                                  item.DiscreteValue
                                                 );
                data.AppendLine(listResults);
            }

            //falling peaks
            data.AppendLine("<<<<<<Falling Peaks>>>>>>");
            foreach (var item in plainData._results.FallingPeaks.Where(x => x.Distance != 0))
            {
                var listResults = string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\"",
                                                  item.TimeBase,
                                                  item.Distance,
                                                  item.ProcessValue,
                                                  item.DiscreteValue
                                                 );
                data.AppendLine(listResults);
            }
            data.AppendLine("<<<<<<Triggers>>>>>>");
            foreach (var item in plainData._results.Triggers.Where(x => x.Distance != 0))
            {
                var listResults = string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\"",
                                                  item.TimeBase,
                                                  item.Distance,
                                                  item.ProcessValue,
                                                  item.DiscreteValue
                                                 );
                data.AppendLine(listResults);
            }


            var fileNamePath = string.Format("{0}{1}{2}.csv",  exportLocation, "\\", RemoveUnnecessary(id));


            try
            {
                using (var sw = new System.IO.StreamWriter(fileNamePath, true))
                {
                    sw.Write(data.ToString());
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }


        }


    }


}

