
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace TcoUtilities
{
    public static class FindExtremsHelper 
    {
        

        public static IEnumerable<float> ElementsAt(IEnumerable<float> source, params int[] indices)
        {
            return indices.Join(source.Select((Item, Index) => (Item, Index)),
                i => i, p => p.Index, (_, p) => p.Item);
        }



        public static IList<int> FindPeaks(IList<float> values, bool negative, int rangeOfPeaks, float noise)
        {
            List<int> peaks = new List<int>();
            double current;
            IEnumerable<float> range;

            int checksOnEachSide = rangeOfPeaks / 2;
            for (int i = 0; i < values.Count; i++)
            {
                current = values[i];
                range = values;

                if (i > checksOnEachSide)
                {
                    range = range.Skip(i - checksOnEachSide);
                }

                range = range.Take(rangeOfPeaks);
                if (!negative)
                    if ((range.Count() > 0) && (current > System.Math.Abs(noise)) && (current == range.Max()))
                    {
                        peaks.Add(i);
                    }
                if (negative)
                    if ((range.Count() > 0) && (current > System.Math.Abs(noise)) && (current == range.Min()))
                    {
                        peaks.Add(i);
                    }
            }

            return peaks;
        }

        public static IList<int> FindTrigger(IList<float> values, float noise)
        {
            List<int> triggers = new List<int>();
            float current;

            float lastValue = 0;

            for (int i = 0; i < values.Count; i++)
            {
                current = values[i];

                if (current != lastValue)
                {
                    lastValue = current;
                    triggers.Add(i);
                }
            }

            return triggers;
        }


        public static IList<float> FilterByMovingAverage(IList<float> list, int smoothFactor)
        {

          return Enumerable
               .Range(0, list.Count - smoothFactor)
               .Select(n => list.Skip(n).Take(smoothFactor).Average())
               .ToList();
           
        }
    }

}

