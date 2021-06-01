using System;

namespace TcOpen.Inxton.Data
{
    internal static class DataHelpers
    {
        private static byte count;
        public static Guid CreateUid()
        {
            var dt = DateTime.Now;
            return new Guid((uint)dt.Year,
                            (ushort)dt.DayOfYear,
                            (ushort)(dt.DayOfWeek),
                            (byte)dt.Day,
                            (byte)dt.Month,
                            (byte)dt.Hour,
                            (byte)dt.Minute,
                            (byte)dt.Second,
                            (byte)dt.Millisecond, count++, 0);
        }
    }
}
