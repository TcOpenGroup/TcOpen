using Grafana.Backend.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TcOpenHammer.Grafana.API.Transformation;

namespace Grafana.Backend.Queries
{
    public class StationModesTimeline : IQuery<enumModesObservedValue>
    {
        public ITable Query(IQueryable<enumModesObservedValue> production, DateTime from, DateTime to)
        {
            // Group events by time.
            var query = production
                 .AsQueryable()
                 .Select(x => new { Time = x.Timestamp, Name = x.Name, Mode = x.ValueDescription })
                 .ToList()
                 .GroupBy(x => x.Time)
                 .Select(x => new { Time = x.Key, StationStates = x })
                 .AsEnumerable();

            // find station names
            var stationCount = query.Max(max => max.StationStates.Count());
            var resultWithAllStations = query.First(x => x.StationStates.Count() == stationCount);
            var stationNames = resultWithAllStations.StationStates.Select(x => x.Name).OrderBy(x => x);
            
            //table with time column and adding a column for every station name
            var table = new Table();
            table.AddColumn(new Column { Text = "Time", Type = "time" });
            stationNames.ForEach(name => table.AddColumn(new Column { Text = name, Type = "text" }));
            // dictionary with every station and it's last state
            var lastStationState = resultWithAllStations.StationStates.ToDictionary(keySelector => keySelector.Name, elementSelector => "Unknown");
            foreach (var record in query)
            {
                var timeColumn = record.Time;
                //update the last known state of the station
                record.StationStates.ForEach(x => lastStationState[x.Name] = x.Mode);
                //add new row with updated states and prepended time.
                table.AddRow(
                    lastStationState.OrderBy(x => x.Key).Select(x => x.Value).Cast<object>().Prepend(timeColumn).ToList()
                );
            }
            return table;
        }
        public string QueryName() => GetType().Name;

    }

}
