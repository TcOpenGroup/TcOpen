using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoCoreUnitTests
{
    class TestHelpers
    {
        private static readonly Random _random = new Random();

        public static ushort RandomNumber(ushort min, ushort max)
        {
            if (max > 0 & max >= min)
            {
                return (ushort)_random.Next(min, max);
            }
            else
                return 0;
        }

        public static short RandomNumber(short min, short max)
        {
            if (max > 0 & max >= min)
            {
                return (short)_random.Next(min, max);
            }
            else
                return 0;
        }
        public static string RandomString(ushort length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }


    }
}
