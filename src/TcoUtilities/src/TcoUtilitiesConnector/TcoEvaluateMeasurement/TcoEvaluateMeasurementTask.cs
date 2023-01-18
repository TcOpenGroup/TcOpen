using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Vortex.Connector;
using System.Threading;
using System.Net.Sockets;

namespace TcoUtilities
{
    public partial class TcoEvaluateMeasurementTask
    {

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
           

        }
     
        public void InitializeTask()
        {
            this.InitializeExclusively(FindExtrems);
        }

        public IList<TimeSpan> TimeBaseValues { get; private set; }
        public IList<float> DistanceValues { get; private set; }

        public IList<float> ProcessValues { get; private set; }
     
        public IList<float> FindTriggersValues { get; private set; }


        public int LimitIndex { get; private set; }

        private bool FindExtrems()
        {
            
            var filterValue = _config.FilterValue.Synchron;
            var searchRange = _config.SearchRange.Synchron;
            var noisePeak = _config.PeaksNoise.Synchron;
            var noiseTrigger = _config.TriggerNoise.Synchron;
            var smootFactor = _config.SmoothFactor.Synchron;
            var ignoreSamplesFromStart = _config.IgnoreSamplesFromStart.Synchron;
            var ignoreSamplesFromEnd = _config.IgnoreSamplesFromEnd.Synchron;
            var ignoreSampleIfTimeBaseIsZero = __config.IgnoreZeroSamplesIfTimeBase.Synchron;
            var ignoreSampleIfDistanceIsZero = __config.IgnoreZeroSamplesIfDistance.Synchron;
            var limitIndexExtrems = _config.LimitIndexFoundExtrems.Synchron;
            var limitIndexTriggers = _config.LimitIndexFoundTriggers.Synchron;

            try
            {

                this.Read();
                var len = _data.Length;

                IEnumerable<TcoEvaluateMeasurementDataItem> data = ignoreSampleIfTimeBaseIsZero
                  ? _data.Skip(ignoreSamplesFromStart).Where(p => p.TimeBase.LastValue != new TimeSpan()).Take(len - ignoreSamplesFromStart - 1)
                  : _data.Skip(ignoreSamplesFromStart).Take(len - ignoreSamplesFromStart - 1);

                data = ignoreSampleIfDistanceIsZero
                    ? _data.Skip(ignoreSamplesFromStart).Where(p => p.Distance.LastValue != 0).Take(len-ignoreSamplesFromStart-1)
                    : _data.Skip(ignoreSamplesFromStart).Take(len - ignoreSamplesFromStart - 1);

                len = data.Count();
                data = data.Take(len - ignoreSamplesFromEnd);


                DistanceValues = data.Select(p => p.Distance.LastValue).Where((x, i) => i % filterValue == 0).ToList();

                ProcessValues = data.Select(p => p.ProcessValue.LastValue).Where((x, i) => i % filterValue == 0).ToList();

                FindTriggersValues = data.Select(p => p.DiscreteValue.LastValue).Where((x, i) => i % filterValue == 0).ToList();


                if (smootFactor != 1)
                {
                    if (ProcessValues.Count !=0)
                        ProcessValues = FindExtremsHelper.FilterByMovingAverage(ProcessValues,Convert.ToInt32(smootFactor));
                    DistanceValues = DistanceValues.Take(ProcessValues.Count).ToArray();
                    FindTriggersValues = FindTriggersValues.Take(ProcessValues.Count).ToArray();
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

                LimitIndex = limitIndexExtrems ;
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
                    }
                    
                    itemIndex++;
                }

                itemIndex = 0;
                LimitIndex = limitIndexTriggers;

                foreach (var item in fallingPeaksIndex)
                {
                    if (itemIndex <= LimitIndex)
                    {
                        _results.FallingPeaks[itemIndex].ProcessValue.Synchron = ProcessValues.ElementAt(item);
                        _results.FallingPeaks[itemIndex].Distance.Synchron = DistanceValues.ElementAt(item);
                    }
                    itemIndex++;
                }

                itemIndex = 0;
                foreach (var item in triggerIndex)
                {
                    if (itemIndex <= LimitIndex)
                    {
                        _results.Triggers[itemIndex].DiscreteValue.Synchron = FindTriggersValues.ElementAt(item);
                        _results.Triggers[itemIndex].Distance.Synchron = DistanceValues.ElementAt(item);
                    }
                    itemIndex++;
                }
              

                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
      

    }


}

