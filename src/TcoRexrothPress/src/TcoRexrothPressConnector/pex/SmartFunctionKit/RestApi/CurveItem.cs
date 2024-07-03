using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoRexrothPressConnector.SmartfunctionKit.RestApi
{
    public class Function
    {
        public List<Parameter> parameters { get; set; }
        public string name { get; set; }
        public bool trendingEnabled { get; set; }
        public bool visible { get; set; }
        public int position { get; set; }
    }

    public class Offset
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class Parameter
    {
        public string type { get; set; }
        public string name { get; set; }
        public object value { get; set; }
    }

    public class Point
    {
        public double x { get; set; }
        public double y { get; set; }
    }

    public class Program
    {
        public string id { get; set; }
        public int fieldbusId { get; set; }
        public string name { get; set; }
        public string driveId { get; set; }
        public List<Function> functions { get; set; }
        public Triggers triggers { get; set; }
        public DateTime uploaded { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime lastModifiedDate { get; set; }
        public DateTime resetTimestamp { get; set; }
        public bool fieldbusRequired { get; set; }
        public List<object> variablesUsed { get; set; }
        public string _id { get; set; }
        public int __v { get; set; }
    }

    public class CurveItem
    {
        public string id { get; set; }
        public Program program { get; set; }
        public List<int> steps { get; set; }
        public List<Point> points { get; set; }
        public double maxForce { get; set; }
        public double maxPosition { get; set; }
        public bool valid { get; set; }
        public List<object> validations { get; set; }
        public DateTime createdDate { get; set; }
        public double cycleTime { get; set; }
        public int validationTime { get; set; }
        public string status { get; set; }
        public string customId { get; set; }
        public List<object> variableValues { get; set; }
        public bool dataRecordingDisabled { get; set; }
        public int samplingInterval { get; set; }
        public Offset offset { get; set; }
        public string _id { get; set; }
        public int __v { get; set; }
    }

    public class Triggers
    {
        public X x { get; set; }
        public Y y { get; set; }
    }

    public class X
    {
        public string type { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }

    public class Y
    {
        public string type { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
}
