using System.Linq;

namespace TcOpen.Inxton
{
    class MqttData
    {
        public int Int { get; set; }
        public double Double { get; set; }
        public string String { get; set; }
        public long Long { get; set; }
        public ulong ULong { get; set; }
        public bool Bool { get; set; }
        public int[] IntArray { get; set; }
        public string[] StringArray { get; set; }

        public MqttDataNested Nested { get; set; }

        public MqttData()
        {
            Int = int.MaxValue;
            Double = double.MaxValue;
            String = "Some string is habla habla";
            Long = long.MaxValue;
            ULong = ulong.MaxValue;
            Bool = true;
            IntArray = new int[] { int.MaxValue, int.MinValue, 10, 0, 12, 31 };
            StringArray = new string[] { "this", "is", "string", "array" };
            Nested = new MqttDataNested();
        }

        public MqttData(bool empty)
        {

        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var other = (MqttData)obj;
            return Int == other.Int &&
                   Double == other.Double &&
                   String == other.String &&
                   Long == other.Long &&
                   ULong == other.ULong &&
                   Bool == other.Bool &&
                   Enumerable.SequenceEqual(IntArray, other.IntArray) &&
                   Enumerable.SequenceEqual(StringArray, other.StringArray) &&
                   Nested.Equals(other.Nested);
        }

        public override int GetHashCode() => (
                    Int.GetHashCode() +
                    Double.GetHashCode() +
                    String.GetHashCode() +
                    Long.GetHashCode() +
                    ULong.GetHashCode() +
                    Bool.GetHashCode() +
                    IntArray.GetHashCode() +
                    StringArray.GetHashCode() +
                    Nested.GetHashCode()
                    )
                    .GetHashCode();
    }

    class MqttDataNested
    {
        public int Int { get; set; }
        public double Double { get; set; }
        public string String { get; set; }
        public long Long { get; set; }
        public ulong ULong { get; set; }
        public bool Bool { get; set; }
        public int[] IntArray { get; set; }
        public string[] StringArray { get; set; }

        public MqttDataNested()
        {
            Int = int.MinValue;
            Double = double.MinValue;
            String = "Some string is habababa";
            Long = long.MinValue;
            ULong = ulong.MinValue;
            Bool = false;
            IntArray = new int[] { int.MaxValue, int.MinValue, 10, 0, 12, 31 };
            StringArray = new string[] { "this", "is", "string", "array" };
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var other = (MqttDataNested)obj;
            return Int == other.Int &&
                   Double == other.Double &&
                   String == other.String &&
                   Long == other.Long &&
                   ULong == other.ULong &&
                   Bool == other.Bool &&
                   Enumerable.SequenceEqual(IntArray, other.IntArray) &&
                   Enumerable.SequenceEqual(StringArray, other.StringArray);
        }

        public override int GetHashCode() => (
                    Int.GetHashCode() +
                    Double.GetHashCode() +
                    String.GetHashCode() +
                    Long.GetHashCode() +
                    ULong.GetHashCode() +
                    Bool.GetHashCode() +
                    IntArray.GetHashCode() +
                    StringArray.GetHashCode())
                    .GetHashCode();
    }



    class MqttOtherData
    {
        public short Short { get; set; }
        public string Long { get; set; }
        public char Character{ get; set; }
        public string Int { get; set; }
        public ulong ULong2 { get; set; }
        public bool Bool2 { get; set; }


        public MqttOtherData()
        {
            Short = short.MinValue;
            Long = "abc";
            Character = 'j';
            Int = "asdas";
            ULong2 = ulong.MaxValue;
            Bool2 = true;
        }


    }
}
